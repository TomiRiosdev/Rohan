using BLL.DomainDtos;
using BLL.GestiónCompra.Facade;
using BLL.GestiónProveedor.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.Facade;
using System;

namespace UI.GestionCompra
{
    public partial class fmsCargarOrdenCompra : Form
    {
        private readonly ProductoProveedorFacade _prodProvFacade;
        private readonly OrdenCompraFacade _ordenCompraFacade;
        private readonly ProveedorFacade _proveedorFacade;

        private readonly OrdenCompraFacade _comprasFacade;
        private readonly IServiceProvider _serviceProvider;
        private List<OrdenCompraDTO> _preOrdenesLocales;


        public fmsCargarOrdenCompra
        (
            OrdenCompraFacade comprasFacade,
            ProductoProveedorFacade prodProvFacade,
            ProveedorFacade proveedorFacade,
            IServiceProvider serviceProvider
        )
        {
            InitializeComponent();
            _ordenCompraFacade = comprasFacade ?? throw new ArgumentNullException(nameof(comprasFacade));
            _prodProvFacade = prodProvFacade ?? throw new ArgumentNullException(nameof(prodProvFacade));
            _proveedorFacade = proveedorFacade ?? throw new ArgumentNullException(nameof(proveedorFacade));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        }

        private void fmsCargarOrdenCompra_Load(object sender, EventArgs e)
        {
            ConfigurarVistasDeGrillas();
            ActualizarPantallaCompleta();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            using (var formManual = _serviceProvider.GetRequiredService<fmsCrearManualOC>())
            {
                if (formManual.ShowDialog() == DialogResult.OK)
                {
                    formManual.OnOrdenCreada += (s, args) =>
                    {
                        // Reemplaza esto con el nombre de tu método que consulta a la BLL y llena el dgvPreOrden
                        ActualizarPantallaCompleta(); ;
                    };
                    // Si el usuario guardó con éxito la pre-orden manual, refrescamos la grilla reactivamente

                }
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvPreOrdenCompra.CurrentRow == null) return;

            try
            {
                var solicitud = (OrdenCompraDTO)dgvPreOrdenCompra.CurrentRow.DataBoundItem;

                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de rechazar y archivar la Orden de Compra N° {solicitud.NroOrdenCompra}?",
                    "Alerta de Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Cambia el estado a Rechazada 
                    _ordenCompraFacade.CambiarEstado(solicitud.IdOrdenCompra, 3);

                    MessageBox.Show("La solicitud fue desestimada y removida de la mesa de entradas.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarPantallaCompleta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Mutación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPantallaCompleta()
        {
            try
            {
                // 1. Desconectamos temporalmente el evento para evitar el bug de índices visuales
                dgvPreOrdenCompra.SelectionChanged -= dgvPreOrdenCompra_SelectionChanged!;

                // 2. CAPTURAMOS EL CONTEXTO REGIONAL DE LA SESIÓN ACTIVA
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión del usuario.");

                // 3. Pasamos (IdSucursal, IdProveedor = null, IdEstado = 1, fechaDesde, fechaHasta)
                DateTime fechaDesde = DateTime.Now.AddMonths(-1); // Ejemplo: último mes
                DateTime fechaHasta = DateTime.Now;
                var todasLasOcs = _ordenCompraFacade.ConsultarHistorial(idSucursalActual, null, 1, fechaDesde, fechaHasta);

                _preOrdenesLocales = new List<OrdenCompraDTO>(todasLasOcs);

                // 4. Volcamos reactivamente en la grilla maestra
                dgvPreOrdenCompra.DataSource = null;
                dgvPreOrdenCompra.DataSource = _preOrdenesLocales;

                // 5. Sincronizamos el detalle derecho según si hay filas o no
                if (_preOrdenesLocales.Count > 0 && dgvPreOrdenCompra.CurrentRow != null)
                {
                    if (dgvPreOrdenCompra.CurrentRow.DataBoundItem is OrdenCompraDTO ocSeleccionada)
                    {
                        dgvDetalleOrdenCompra.DataSource = null;
                        dgvDetalleOrdenCompra.DataSource = ocSeleccionada.Detalles;
                    }
                }
                else
                {
                    dgvDetalleOrdenCompra.DataSource = null;
                }

                // Habilitamos o deshabilitamos el botón de generación según el estado de la grilla
                btnGenerarOrdenCompra.Enabled = _preOrdenesLocales.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga Regional", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 6. Volvemos a conectar el escuchador de clics del usuario
                dgvPreOrdenCompra.SelectionChanged += dgvPreOrdenCompra_SelectionChanged!;
            }
        }

        private void btnGenerarOrdenCompra_Click(object sender, EventArgs e)
        {
            if (dgvPreOrdenCompra.CurrentRow == null) return;

            try
            {
                var oc = (OrdenCompraDTO)dgvPreOrdenCompra.CurrentRow.DataBoundItem;

                DialogResult result = MessageBox.Show(
                    $"¿Desea confirmar y emitir definitivamente la Orden de Compra N° {oc.NroSolicitudReferencia}? " +
                    "Esto bloqueará el documento y generará el archivo físico para el proveedor.",
                    "Emisión Comercial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 1. Pasamos el estado de 1 (Pendiente) a 2 (Emitida)
                    _ordenCompraFacade.CambiarEstado(oc.IdOrdenCompra, 2);

                    // 2. Generamos el archivo Bloc de Notas en la carpeta del sistema
                    string rutaDestino = AppDomain.CurrentDomain.BaseDirectory + "OrdenesEmitidas";
                    Directory.CreateDirectory(rutaDestino);
                    _ordenCompraFacade.GenerarDocumentoTexto(oc.IdOrdenCompra, rutaDestino);

                    MessageBox.Show("Orden de Compra emitida y exportada a TXT con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarPantallaCompleta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al emitir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrillaMaestro()
        {
            dgvPreOrdenCompra.AutoGenerateColumns = false;
            dgvPreOrdenCompra.AllowUserToAddRows = false;
            dgvPreOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreOrdenCompra.RowHeadersVisible = false;
            dgvPreOrdenCompra.BackgroundColor = Color.White;
            dgvPreOrdenCompra.BorderStyle = BorderStyle.None;
            dgvPreOrdenCompra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvPreOrdenCompra.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
            dgvPreOrdenCompra.Columns.Clear();

            dgvPreOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroOrdenCompra",
                DataPropertyName = "NroOrdenCompra", // Sincroniza con tu propiedad del DTO
                HeaderText = "N° Orden",
                Width = 85,
                ReadOnly = true
            });

            dgvPreOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RazonSocialProveedor", // Asegurate que tu DTO rico traiga resuelta esta propiedad string
                DataPropertyName = "RazonSocialProveedor",
                HeaderText = "Proveedor",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            var colFecha = new DataGridViewTextBoxColumn
            {
                Name = "FechaOc",
                DataPropertyName = "FechaOc",
                HeaderText = "Fecha",
                Width = 110,
                ReadOnly = true
            };
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPreOrdenCompra.Columns.Add(colFecha);

            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "CostoTotal",
                DataPropertyName = "CostoTotal",
                HeaderText = "Total",
                Width = 95,
                ReadOnly = true
            };
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Format = "C2"; // Formato moneda local (Ej: $150.00)
            dgvPreOrdenCompra.Columns.Add(colTotal);
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvDetalleOrdenCompra.AutoGenerateColumns = false;
            dgvDetalleOrdenCompra.AllowUserToAddRows = false;
            dgvDetalleOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleOrdenCompra.RowHeadersVisible = false;
            dgvDetalleOrdenCompra.BackgroundColor = Color.White;
            dgvDetalleOrdenCompra.BorderStyle = BorderStyle.None;
            dgvDetalleOrdenCompra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvDetalleOrdenCompra.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);

            dgvDetalleOrdenCompra.Columns.Clear();

            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Renglon",
                DataPropertyName = "Renglon",
                HeaderText = "Rng",
                Width = 45,
                ReadOnly = true
            });

            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku", // Propiedad extendida de tu OrdenCompraDetalleDTO
                HeaderText = "SKU",
                Width = 85,
                ReadOnly = true
            });

            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Descripción del Artículo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            var colCant = new DataGridViewTextBoxColumn
            {
                Name = "CantidadPedida",
                DataPropertyName = "CantidadPedida",
                HeaderText = "Cant. Bultos",
                Width = 90,
                ReadOnly = true
            };
            colCant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleOrdenCompra.Columns.Add(colCant);

            // Precio Unitario Pactado inicialmente (extraído de tu tabla intermedia)
            var colPrecio = new DataGridViewTextBoxColumn
            {
                Name = "PrecioPactado",
                DataPropertyName = "PrecioPactado",
                HeaderText = "P. Pactado",
                Width = 95,
                ReadOnly = true
            };
            colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrecio.DefaultCellStyle.Format = "C2"; // Formato Moneda ($)
            dgvDetalleOrdenCompra.Columns.Add(colPrecio);


            var colSubtotal = new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                DataPropertyName = "Subtotal", // Propiedad que podés calcular mediante un 'get' en tu DTO detalle
                HeaderText = "Subtotal",
                Width = 100,
                ReadOnly = true
            };
            colSubtotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSubtotal.DefaultCellStyle.Format = "C2";
            dgvDetalleOrdenCompra.Columns.Add(colSubtotal);
        }

        private void ConfigurarVistasDeGrillas()
        {
            ConfigurarGrillaMaestro();
            ConfigurarGrillaDetalle();
        }

        private void dgvPreOrdenCompra_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPreOrdenCompra.Rows.Count == 0 || dgvPreOrdenCompra.CurrentRow == null || dgvPreOrdenCompra.CurrentRow.Index < 0)
            {
                dgvDetalleOrdenCompra.DataSource = null;
                return;
            }

            try
            {
                if (dgvPreOrdenCompra.CurrentRow.DataBoundItem is OrdenCompraDTO ocSeleccionada)
                {
                    // Cargamos el detalle derecho de forma aislada
                    dgvDetalleOrdenCompra.DataSource = null;
                    dgvDetalleOrdenCompra.DataSource = ocSeleccionada.Detalles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvPreOrdenCompra.CurrentRow?.DataBoundItem is OrdenCompraDTO ocSeleccionada)
            {
                // Validamos estado visualmente 
                if (ocSeleccionada.EstadoDescripcion != "Pendiente")
                {
                    MessageBox.Show("Solo puede modificar órdenes pendientes."); return;
                }

                var formModificar = new fmsModificarOrdenCompra(
                    ocSeleccionada.IdOrdenCompra,
                    _prodProvFacade,
                    _ordenCompraFacade
                );

                formModificar.OnOrdenModificada += (s, args) => ActualizarPantallaCompleta();
                formModificar.ShowDialog();
            }
        }
    }
}
