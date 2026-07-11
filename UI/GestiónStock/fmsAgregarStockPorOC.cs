using BLL.DomainDtos;
using BLL.GestiónCompra.Facade;
using BLL.GestiónStock.Facade;
using Service.Facade;
using System.Data;


namespace UI.GestiónStock
{
    public partial class fmsAgregarStockPorOC : Form
    {
        private readonly StockFacade _stockFacade;
        private readonly OrdenCompraFacade _comprasFacade;
        private List<OrdenCompraDTO> _ordenesAprobadasLocales;
        private readonly IServiceProvider _serviceProvider;
        public fmsAgregarStockPorOC
        (
            StockFacade stockFacade,
            OrdenCompraFacade comprasFacade
        )
        {
            InitializeComponent();
            _stockFacade = stockFacade ?? throw new ArgumentNullException(nameof(stockFacade));
            _comprasFacade = comprasFacade ?? throw new ArgumentNullException(nameof(comprasFacade));
        }

      

        private void fmsAgregarStockPorOC_Load(object sender, EventArgs e)
        {
            ConfigurarVistasDeGrillas();
            ActualizarPantallaCompleta();
        }

        private void ActualizarPantallaCompleta()
        {
            try
            {
                // 1. DESVINCULAMOS EVENTOS PARA EVITAR EJECUCIONES INDESEADAS DURANTE LA ACTUALIZACIÓN DE DATOS
                dgvOrdenCompra.SelectionChanged -= dgvOrdenCompra_SelectionChanged!;

                // 2. CAPTURAMOS EL CONTEXTO REGIONAL DE LA SESIÓN ACTIVA
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión del usuario.");

                // 3. CONSULTAMOS LAS ORDENES DE COMPRA APROBADAS PARA LA SUCURSAL ACTUAL
                DateTime fechaDesde = DateTime.Today.AddDays(-30); // Últimos 30 días
                DateTime fechaHasta = DateTime.Today;

                var ocsAprobadas = _comprasFacade.ConsultarHistorial(idSucursalActual, null, 2, fechaDesde, fechaHasta);
                _ordenesAprobadasLocales = new List<OrdenCompraDTO>(ocsAprobadas);

                // 4. Volcar datos
                dgvOrdenCompra.DataSource = null;
                dgvOrdenCompra.DataSource = _ordenesAprobadasLocales;

                // 5. Sincronizar grilla hija
                if (_ordenesAprobadasLocales.Count > 0 && dgvOrdenCompra.CurrentRow != null)
                {
                    if (dgvOrdenCompra.CurrentRow.DataBoundItem is OrdenCompraDTO ocSeleccionada)
                    {
                        PrepararDetallesParaRecepcion(ocSeleccionada.Detalles);
                        dgvDetalleOrdenCompra.DataSource = null;
                        dgvDetalleOrdenCompra.DataSource = ocSeleccionada.Detalles;
                    }
                }
                else
                {
                    dgvDetalleOrdenCompra.DataSource = null;
                }

                btnIngresar.Enabled = _ordenesAprobadasLocales.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvOrdenCompra.SelectionChanged += dgvOrdenCompra_SelectionChanged!;
            }
        }

        private void PrepararDetallesParaRecepcion(IEnumerable<OrdenCompraDetalleDTO> detalles)
        {
            // Inicializa la cantidad recibida por defecto igual a la pedida para agilizar la carga
            foreach (var det in detalles)
            {
                if (det.CantidadRecibida == 0) // Previene sobreescribir si ya tiene datos
                {
                    det.CantidadRecibida = det.CantidadPedida;
                }
            }
        }
        #region Configuración de Vistas de Grillas
        private void ConfigurarVistasDeGrillas()
        {
            ConfigurarGrillaMaestro();
            ConfigurarGrillaDetalle();
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvOrdenCompra.AutoGenerateColumns = false;
            dgvOrdenCompra.AllowUserToAddRows = false;
            dgvOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenCompra.RowHeadersVisible = false;
            dgvOrdenCompra.BackgroundColor = Color.White;
            dgvOrdenCompra.BorderStyle = BorderStyle.None;
            dgvOrdenCompra.ReadOnly = true;
            dgvOrdenCompra.MultiSelect = false;

            dgvOrdenCompra.Columns.Clear();
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroOrdenCompra",
                DataPropertyName = "NroOrdenCompra",
                HeaderText = "N° Orden",
                Width = 90
            });

            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Proveedor",
                DataPropertyName = "RazonSocialProveedor",
                HeaderText = "Proveedor",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var colFecha = new DataGridViewTextBoxColumn
            {
                Name = "FechaOc",
                DataPropertyName = "FechaOc",
                HeaderText = "Fecha",
                Width = 90
            };

            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvOrdenCompra.Columns.Add(colFecha);
        }

        private void ConfigurarGrillaMaestro()
        {
            dgvDetalleOrdenCompra.AutoGenerateColumns = false;
            dgvDetalleOrdenCompra.AllowUserToAddRows = false;
            dgvDetalleOrdenCompra.RowHeadersVisible = false;
            dgvDetalleOrdenCompra.BackgroundColor = Color.White;
            dgvDetalleOrdenCompra.BorderStyle = BorderStyle.None;

            // ATENCIÓN: La grilla no es de solo lectura, se bloquea por columna
            dgvDetalleOrdenCompra.ReadOnly = false;
            dgvDetalleOrdenCompra.SelectionMode = DataGridViewSelectionMode.CellSelect; // Permite navegar celdas para editar
            dgvDetalleOrdenCompra.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            dgvDetalleOrdenCompra.Columns.Clear();

            // 1. Columnas de solo lectura
            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoSku",
                HeaderText = "SKU",
                Width = 70,
                ReadOnly = true
            });

            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductoNombre",
                HeaderText = "Artículo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            var colCantPedida = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadPedida",
                HeaderText = "Pedida",
                Width = 70,
                ReadOnly = true
            };

            colCantPedida.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleOrdenCompra.Columns.Add(colCantPedida);

            // 2. Columnas Editables (Se les da un color de fondo sutil para guiar al usuario)
            var colCantRecibida = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadRecibida", // Tu DTO debe tener set; en esta propiedad
                HeaderText = "Ingresa",
                Width = 80,
                ReadOnly = false
            };
            colCantRecibida.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCantRecibida.DefaultCellStyle.BackColor = Color.LightYellow;
            colCantRecibida.DefaultCellStyle.Font = new Font(dgvDetalleOrdenCompra.Font, FontStyle.Bold);
            dgvDetalleOrdenCompra.Columns.Add(colCantRecibida);

            var colObs = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Observaciones", // Tu DTO debe tener una prop string Observaciones
                HeaderText = "Observación Recepción",
                Width = 150,
                ReadOnly = false
            };
            colObs.DefaultCellStyle.BackColor = Color.LightYellow;
            dgvDetalleOrdenCompra.Columns.Add(colObs);
        }

        #endregion

        private void dgvDetalleOrdenCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region Eventos

        private void dgvOrdenCompra_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow == null || dgvOrdenCompra.CurrentRow.Index < 0)
            {
                dgvDetalleOrdenCompra.DataSource = null;
                return;
            }

            if (dgvOrdenCompra.CurrentRow.DataBoundItem is OrdenCompraDTO ocSeleccionada)
            {
                PrepararDetallesParaRecepcion(ocSeleccionada.Detalles);
                dgvDetalleOrdenCompra.DataSource = null;
                dgvDetalleOrdenCompra.DataSource = ocSeleccionada.Detalles;
            }
        }
        // Maneja errores de formato en la grilla de detalle
        private void dgvDetalleOrdenCompra_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Valor ingresado inválido. Ingrese un número entero.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow == null) return;
            dgvDetalleOrdenCompra.EndEdit(); // Fuerza a consolidar la última celda editada

            var oc = (OrdenCompraDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;

            try
            {
                // 1. Armar la lista de DTOs 
                var detallesBound = (IEnumerable<OrdenCompraDetalleDTO>)dgvDetalleOrdenCompra.DataSource;

                var listaRecepcion = detallesBound.Select(d => new RecepcionMercaderiaDTO
                {
                    IdOrdenCompraDetalle = d.IdOrdenCompraDetalle,
                    IdProducto = d.IdProducto,
                    CantidadRealRecibida = d.CantidadRecibida,
                    UnidadesPorBulto = d.UnidadesPorBulto,
                    Observaciones = d.Observaciones

                }).ToList();

                if (!listaRecepcion.Any(r => r.CantidadRealRecibida > 0))
                {
                    MessageBox.Show("No se detectaron cantidades a ingresar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"¿Confirma el ingreso físico de mercadería para la OC N° {oc.NroOrdenCompra}?",
                    "Confirmación de Ingreso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Guid idSucursal = SessionManager.Current.IdSucursalActual.Value;
                    string usuario = SessionManager.Current.UsuarioLogueado?.Nombre ?? "Sistema";

                    // 2. Llamada a la Facade orquestadora
                    _stockFacade.RegistrarIngresoPorOrdenCompra(oc.IdOrdenCompra, idSucursal, usuario, listaRecepcion);

                    MessageBox.Show("Ingreso de stock procesado y auditado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarPantallaCompleta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Ingresar Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
