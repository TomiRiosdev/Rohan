using BLL.DomainDtos;
using BLL.GestiónCompra.Facade;
using BLL.GestiónProveedor.Facade;
using Service.Facade;
using System.ComponentModel;


namespace UI.GestionCompra
{
    public partial class fmsModificarOrdenCompra : Form
    {
        private readonly ProductoProveedorFacade _prodProvFacade;
        private readonly OrdenCompraFacade _ordenCompraFacade;


        private Guid _idOrdenCompraActual;
        private Guid _idProveedorActual;
        private ProductoProveedorDTO _productoElegidoActual;
        private BindingList<OrdenCompraDetalleDTO> _detallesPreOrden = new BindingList<OrdenCompraDetalleDTO>();

        public event EventHandler OnOrdenModificada;

        public fmsModificarOrdenCompra
        (
            Guid idOrdenCompraSeleccionada,

            ProductoProveedorFacade prodProvFacade,
            OrdenCompraFacade ordenCompraFacade
         
        )
        {
            InitializeComponent();
            _idOrdenCompraActual = idOrdenCompraSeleccionada;

            _prodProvFacade = prodProvFacade ?? throw new ArgumentNullException(nameof(prodProvFacade));
            _ordenCompraFacade = ordenCompraFacade ?? throw new ArgumentNullException(nameof(ordenCompraFacade));
       
        }   

        private void fmsModificarOrdenCompra_Load(object sender, EventArgs e)
        {
            try
            {
                txtProvRazonSocial.ReadOnly = true;
                txtCuil.ReadOnly = true;
                txtNroOrden.ReadOnly = true;
                nupCantidad.Enabled = false;
                lblCantidadBulto.Visible = false;
                lblPrecio.Visible = false;

                //  Configurar Grilla
                dgvPreOrdenCompra.AutoGenerateColumns = false;
                dgvPreOrdenCompra.DataSource = _detallesPreOrden;
                ConfigurarColumnasPreOrden();

                // CARGAR DATOS EXISTENTES
                CargarDatosDeOrdenExistente();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la Orden de Compra: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Si falla la carga, cerramos por seguridad
            }

        }

        private void CargarDatosDeOrdenExistente()
        {

            var ocExistente = _ordenCompraFacade.BuscarPorId(_idOrdenCompraActual);

            if (ocExistente == null) throw new Exception("La orden de compra no existe o fue eliminada.");

            // Cargar datos del Proveedor 
            _idProveedorActual = ocExistente.IdProveedor.Value;
            txtProvRazonSocial.Text = ocExistente.RazonSocialProveedor;
            txtCuil.Text = ocExistente.CuitProveedor;
            txtNroOrden.Text = ocExistente.NroSolicitudReferencia.ToString();


            // Llenar la memoria (El carrito) con los renglones que ya tenía la OC
            foreach (var detalle in ocExistente.Detalles)
            {
                _detallesPreOrden.Add(detalle);
            }
            ActualizarTotalizadorOc();

            // Cargar el catálogo de productos de este proveedor 
            CargarProductosDelProveedor(_idProveedorActual);
        }
      
        private void CargarProductosDelProveedor(Guid idProveedor)
        {
            try
            {
                dgvProducto.SelectionChanged -= dgvProducto_SelectionChanged!;

                dgvProducto.AutoGenerateColumns = false;
                List<ProductoProveedorDTO> productos = _prodProvFacade.ListarProductosPorProveedor(idProveedor).ToList();

                dgvProducto.DataSource = null;
                dgvProducto.DataSource = productos;

                ConfigurarColumnasGrillaDerecha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos del proveedor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvProducto.ClearSelection();
                dgvProducto.SelectionChanged += dgvProducto_SelectionChanged!;
            }
        }
       
        private void ConfigurarColumnasGrillaDerecha()
        {
            dgvProducto.Columns.Clear();

            dgvProducto.AllowUserToAddRows = false;
            dgvProducto.ReadOnly = true;
            dgvProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducto.RowHeadersVisible = false;
            dgvProducto.BackgroundColor = Color.White;
            dgvProducto.BorderStyle = BorderStyle.None;
            dgvProducto.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvProducto.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku",
                HeaderText = "Código SKU",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
       
        private void dgvProducto_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducto.CurrentRow == null || dgvProducto.CurrentRow.Index < 0)
            {
                lblCantidadBulto.Visible = false;
                lblPrecio.Visible = false;
                nupCantidad.Enabled = false;
                _productoElegidoActual = null;
                return;
            }

            try
            {
                if (dgvProducto.CurrentRow.DataBoundItem is ProductoProveedorDTO prodSeleccionado)
                {
                    _productoElegidoActual = prodSeleccionado;

                    // Bulto/ Envase
                    if (prodSeleccionado.CantidadPorBulto > 1)
                        lblCantidadBulto.Text = $"{prodSeleccionado.TipoEnvaseNombre} Cerrado/a ({prodSeleccionado.CantidadPorBulto} u.)";
                    else
                        lblCantidadBulto.Text = $"{prodSeleccionado.TipoEnvaseNombre} Directo ({prodSeleccionado.ContenidoPorVenta} {prodSeleccionado.UnidadMedidaNombre})";

                    lblCantidadBulto.Visible = true;

                    // Precio
                    if (prodSeleccionado.PrecioUnitario > 0)
                    {
                        lblPrecio.Text = $"Costo Unitario: {prodSeleccionado.PrecioUnitario:C2}";
                        lblPrecio.ForeColor = Color.DarkGreen; // Color de éxito/dinero
                    }
                    else
                    {
                        lblPrecio.Text = "Costo Unitario: $0.00 (Sin Cargar)";
                        lblPrecio.ForeColor = Color.DarkRed; // Alerta visual
                    }
                    lblPrecio.Visible = true;


                    nupCantidad.Enabled = true;
                    nupCantidad.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al interpretar las propiedades del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (_detallesPreOrden.Count == 0)
            {
                MessageBox.Show("La orden debe tener al menos un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var ocModificadaDto = new OrdenCompraDTO
                {
                    IdOrdenCompra = _idOrdenCompraActual, // Le decimos a la BLL qué OC estamos pisando
                    IdProveedor = _idProveedorActual,
                    IdSucursal = SessionManager.Current.IdSucursalActual.Value,
                    IdUsuario = SessionManager.Current.UsuarioLogueado?.IdUsuario,
                    Detalles = _detallesPreOrden.ToList()
                };

                // LLAMADA A UN NUEVO MÉTODO EN LA BLL
                _ordenCompraFacade.ActualizarOrdenCompra(ocModificadaDto);

                MessageBox.Show("Orden de Compra actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnOrdenModificada?.Invoke(this, EventArgs.Empty);
                this.Close(); // Cerramos automáticamente tras editar
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (_productoElegidoActual == null)
            {
                MessageBox.Show("Debe seleccionar un producto del catálogo del proveedor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidadPedida = (int)nupCantidad.Value;
            if (cantidadPedida <= 0)
            {
                MessageBox.Show("La cantidad a pedir debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var renglonExistente = _detallesPreOrden.FirstOrDefault(d => d.IdProducto == _productoElegidoActual.IdProducto);

                if (renglonExistente != null)
                {
                    renglonExistente.CantidadPedida += cantidadPedida;
                }
                else
                {
                    _detallesPreOrden.Add(new OrdenCompraDetalleDTO
                    {
                        IdProducto = _productoElegidoActual.IdProducto,
                        CodigoSku = _productoElegidoActual.CodigoSku,
                        ProductoNombre = _productoElegidoActual.ProductoNombre,
                        CantidadPedida = cantidadPedida,
                        PrecioPactado = _productoElegidoActual.PrecioUnitario,
                        UnidadesPorBulto = _productoElegidoActual.CantidadPorBulto,
                        CantidadRecibida = 0,
                        Observaciones = string.Empty
                    });

                }

                ReordenarNumerosDeRenglon();
                ActualizarTotalizadorOc();

                nupCantidad.Value = 0;
                nupCantidad.Enabled = false;
                lblCantidadBulto.Visible = false;
                dgvProducto.ClearSelection();
                _productoElegidoActual = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar artículo a la orden: {ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro de descartar la orden de compra en curso?", "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ConfigurarColumnasPreOrden()
        {
            dgvPreOrdenCompra.Columns.Clear();
            dgvPreOrdenCompra.AutoGenerateColumns = false;
            dgvPreOrdenCompra.AllowUserToAddRows = false;
            dgvPreOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreOrdenCompra.BackgroundColor = Color.White;
            dgvPreOrdenCompra.BorderStyle = BorderStyle.None;
            dgvPreOrdenCompra.RowHeadersVisible = false;

            dgvPreOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Renglon",
                HeaderText = "N°",
                Width = 40,
                ReadOnly = true
            });

            dgvPreOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoSku",
                HeaderText = "SKU",
                Width = 80,
                ReadOnly = true
            });

            dgvPreOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            dgvPreOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadPedida",
                HeaderText = "Cant.",
                Width = 70,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            var btnEliminar = new DataGridViewButtonColumn
            {
                Name = "colEliminar",
                HeaderText = "",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Width = 40
            };
            btnEliminar.DefaultCellStyle.ForeColor = Color.Red;
            dgvPreOrdenCompra.Columns.Add(btnEliminar);
        }

        private void ReordenarNumerosDeRenglon()
        {
            for (int i = 0; i < _detallesPreOrden.Count; i++)
            {
                _detallesPreOrden[i].Renglon = i + 1;
            }
            _detallesPreOrden.ResetBindings();
        }

        private void ActualizarTotalizadorOc()
        {
            decimal total = _detallesPreOrden.Sum(d => d.SubTotal);
            lblSubtotal.Text = $"Total Estimado: {total:C2}";
        }

        private void dgvPreOrdenCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dgvPreOrdenCompra.Columns[e.ColumnIndex].Name == "colEliminar")
            {
                var detalle = (OrdenCompraDetalleDTO)dgvPreOrdenCompra.Rows[e.RowIndex].DataBoundItem;
                _detallesPreOrden.Remove(detalle);

                ReordenarNumerosDeRenglon();
                ActualizarTotalizadorOc();

            }
        }

       
    }
}
