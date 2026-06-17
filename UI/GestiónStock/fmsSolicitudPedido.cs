using BLL.DomainDtos;
using BLL.GestiónCompra.Facade;
using BLL.GestiónCompra.Interface;
using Microsoft.Extensions.DependencyInjection;
using Service.Facade;
using System;
using System.ComponentModel;


namespace UI.GestiónStock
{
    public partial class fmsSolicitudPedido : Form
    {
        private readonly SolicitudPedidoFacade _solicitudPedido;
        private readonly IServiceProvider _serviceProvider;
        private BindingList<SolicitudPedidoDetalleDTO> _carritoDetalles = new();
        private ProductoDTO? _productoSeleccionado;

        public fmsSolicitudPedido
        (
            SolicitudPedidoFacade solicitudPedido,
            IServiceProvider serviceProvider
        )
        {
            InitializeComponent();
            _solicitudPedido = solicitudPedido ?? throw new ArgumentNullException(nameof(solicitudPedido));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        private void fmsSolicitudPedido_Load_1(object sender, EventArgs e)
        {
            ConfigurarGrillaDetalle();
            dgvProductosSolicitud.DataSource = _carritoDetalles;
            txtProductoNombre.Enabled = false; 
            txtSKU.Enabled = false;
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvProductosSolicitud.AutoGenerateColumns = false;
            dgvProductosSolicitud.AllowUserToAddRows = false;
            dgvProductosSolicitud.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductosSolicitud.RowHeadersVisible = false;
            dgvProductosSolicitud.BackgroundColor = Color.White;
            dgvProductosSolicitud.BorderStyle = BorderStyle.None;
            dgvProductosSolicitud.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvProductosSolicitud.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);

            dgvProductosSolicitud.Columns.Clear();
            dgvProductosSolicitud.Columns.Add(new DataGridViewTextBoxColumn { Name = "Renglon", DataPropertyName = "Renglon", HeaderText = "N°", Width = 40, ReadOnly = true });
            dgvProductosSolicitud.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoSku", DataPropertyName = "CodigoSku", HeaderText = "SKU", Width = 80, ReadOnly = true });
            dgvProductosSolicitud.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductoNombre", DataPropertyName = "ProductoNombre", HeaderText = "Producto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            // CANTIDAD EDITABLE DIRECTAMENTE EN LA CELDA
            dgvProductosSolicitud.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadBultosSolicitada",
                DataPropertyName = "CantidadBultosSolicitada",
                HeaderText = "Cant. Bultos",
                Width = 95,
                ReadOnly = false 
            });

            dgvProductosSolicitud.Columns.Add(new DataGridViewTextBoxColumn { Name = "PresentacionTipo", DataPropertyName = "PresentacionTipo", HeaderText = "Tipo", Width = 80, ReadOnly = true });
        }



        #region Carga Manual (Buscador, Selección y Agregar)
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var frmBuscador = _serviceProvider.GetRequiredService<fmsListarProductosPopUp>())
            {
                frmBuscador.StartPosition = FormStartPosition.CenterParent;
                if (frmBuscador.ShowDialog() == DialogResult.OK)
                {
                    _productoSeleccionado = frmBuscador.ProductoSeleccionado;

                    if (_productoSeleccionado != null)
                    {
                        txtSKU.Text = _productoSeleccionado.CodigoSku.ToString();
                        txtProductoNombre.Text = _productoSeleccionado.Nombre;

                        ActualizarLabelUnidadesSueltas();

                        numCantidadBultos.Focus();
                    }
                }
            }
        }

        private void LimpiarCamposCargaManual()
        {
            _productoSeleccionado = null;
            txtSKU.Clear();
            txtProductoNombre.Clear();
            numCantidadBultos.Value = 1;
            ActualizarLabelUnidadesSueltas();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un producto utilizando el buscador antes de agregarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidadBultos = (int)numCantidadBultos.Value;

            //REGLA DE NEGOCIO: Si el producto ya existe en el carrito, sumamos las cantidades en vez de duplicar la fila
            var renglonExistente = _carritoDetalles.FirstOrDefault(d => d.IdProducto == _productoSeleccionado.Id);
            if (renglonExistente != null)
            {
                renglonExistente.CantidadBultosSolicitada += cantidadBultos;
                dgvProductosSolicitud.Refresh(); // Fuerza el redibujado de la celda de cantidad
                LimpiarCamposCargaManual();
                return;
            }

            // Si es un producto nuevo, lo agregamos a la BindingList
            _carritoDetalles.Add(new SolicitudPedidoDetalleDTO
            {
                IdProducto = _productoSeleccionado.Id,
                CodigoSku = (int)_productoSeleccionado.CodigoSku,
                ProductoNombre = _productoSeleccionado.Nombre,
                CantidadBultosSolicitada = cantidadBultos,
                UnidadesPorBulto = (_productoSeleccionado.CantidadPorBulto),
                PresentacionTipo = "Caja" // O el tipo de envase mapeado
            });

            ReordenarRenglonesMatematicos();
            LimpiarCamposCargaManual();
        }

        #endregion

        #region Automatización y Modificaciones del Carrito
        private void btnAgregarAutomatico_Click_1(object sender, EventArgs e)
        {
            try
            {
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión.");

                // Traemos la propuesta de stock crítico
                var sugeridos = _solicitudPedido.GenerarDetallesSugeridosBajoMinimo(idSucursalActual);

                if (!sugeridos.Any())
                {
                    MessageBox.Show("El stock se encuentra equilibrado. Ningún producto está por debajo del mínimo operativo.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var sug in sugeridos)
                {
                    // Buscamos si el artículo sugerido ya estaba metido en el carrito actual
                    var itemExistente = _carritoDetalles.FirstOrDefault(x => x.IdProducto == sug.IdProducto);

                    if (itemExistente != null)
                    {
                        itemExistente.CantidadBultosSolicitada += sug.CantidadBultosSolicitada;
                    }
                    else
                    {
                        _carritoDetalles.Add(sug);
                    }
                }
                
                ReordenarRenglonesMatematicos();
                RefrescarGrillaCarrito();

                MessageBox.Show("Productos de bajo stock cargado con éxito",
                                "Propuesta Combinada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Logístico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefrescarGrillaCarrito()
        {
            dgvProductosSolicitud.DataSource = null;
            dgvProductosSolicitud.DataSource = _carritoDetalles;
        }
        private void btnEliminarRenglon_Click_1(object sender, EventArgs e)
        {
            if (dgvProductosSolicitud.CurrentRow != null && dgvProductosSolicitud.CurrentRow.DataBoundItem is SolicitudPedidoDetalleDTO seleccionado)
            {
                _carritoDetalles.Remove(seleccionado);
                ReordenarRenglonesMatematicos();
            }
        }
        private void ReordenarRenglonesMatematicos()
        {
            int contador = 1;
            foreach (var item in _carritoDetalles)
            {
                item.Renglon = contador++;
            }
            dgvProductosSolicitud.Refresh();
        }

        #endregion

        #region Guardado Final (Persistencia)
        private void btnEnviarSolicitud_Click(object sender, EventArgs e)
        {
            if (!_carritoDetalles.Any())
            {
                MessageBox.Show("No se puede registrar el documento: La solicitud debe contener al menos un renglón.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var SolicitudDto = new SolicitudPedidoDTO
                {
                    IdSucursal = SessionManager.Current.IdSucursalActual,
                    IdUsuario = SessionManager.Current.UsuarioLogueado?.IdUsuario,
                    // Pasamos los detalles del carrito tal cual los armó la grilla
                    Detalles = _carritoDetalles.ToList()
                };

                // servicio de la BLL
                _solicitudPedido.CrearSolicitud(SolicitudDto);

                MessageBox.Show("La Solicitud de Pedido fue registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                LimpiarFormularioCompleto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Persistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        private void LimpiarFormularioCompleto()
        {
            _carritoDetalles.Clear();
            _productoSeleccionado = null;
            txtSKU.Clear();
            txtProductoNombre.Clear();
            numCantidadBultos.Value = 1;
            ActualizarLabelUnidadesSueltas();
        }

        private void lblUnidadesSueltas_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numCantidadBultos_ValueChanged(object sender, EventArgs e)
        {
            ActualizarLabelUnidadesSueltas();
        }
        private void ActualizarLabelUnidadesSueltas()
        {
            if (_productoSeleccionado != null)
            {
                int unidadesPorBulto = _productoSeleccionado.CantidadPorBulto ;
                int totalSueltas = (int)numCantidadBultos.Value * unidadesPorBulto;

                lblCantBulto.Text = $"Equivale a: {totalSueltas} unidades sueltas.";
            }
            else
            {
                lblCantBulto.Text = "Equivale a: 0 unidades sueltas.";
            }
        }
    }
}

