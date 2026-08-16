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

        // Variables para mantener los datos en memoria según el modo
        private List<OrdenCompraDTO> _ordenesCompraPendientes;
        private List<OrdenTraspasoDTO> _traspasosPendientes;

        public fmsAgregarStockPorOC(StockFacade stockFacade, OrdenCompraFacade comprasFacade)
        {
            InitializeComponent();
            _stockFacade = stockFacade ?? throw new ArgumentNullException(nameof(stockFacade));
            _comprasFacade = comprasFacade ?? throw new ArgumentNullException(nameof(comprasFacade));
        }

        private void fmsAgregarStockPorOC_Load(object sender, EventArgs e)
        {
            // Inicializamos el ComboBox
            cmbTipoRecepcion.Items.Add("Órdenes de Compra (Proveedores Externos)");
            cmbTipoRecepcion.Items.Add("Traspasos Internos (Depósito Central)");
            cmbTipoRecepcion.SelectedIndex = 0; // Selecciona OC por defecto

            cmbTipoRecepcion.SelectedIndexChanged += CmbTipoRecepcion_SelectedIndexChanged;

            ActualizarPantallaCompleta();
        }

        private void CmbTipoRecepcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPantallaCompleta();
        }

        private void ActualizarPantallaCompleta()
        {
            try
            {
                dgvOrdenCompra.SelectionChanged -= dgvOrdenCompra_SelectionChanged!;
                dgvOrdenCompra.DataSource = null;
                dgvDetalleOrdenCompra.DataSource = null;
                dgvOrdenCompra.Columns.Clear();
                dgvDetalleOrdenCompra.Columns.Clear();

                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión.");

                // MODO 0: ÓRDENES DE COMPRA
                if (cmbTipoRecepcion.SelectedIndex == 0)
                {
                    ConfigurarGrillasModoOC();
                    DateTime fechaDesde = DateTime.Today.AddDays(-30);

                    var ocsAprobadas = _comprasFacade.ConsultarHistorial(idSucursalActual, null, 2, fechaDesde, DateTime.Today);
                    _ordenesCompraPendientes = new List<OrdenCompraDTO>(ocsAprobadas);

                    dgvOrdenCompra.DataSource = _ordenesCompraPendientes;
                }
                // MODO 1: TRASPASOS EN TRÁNSITO
                else if (cmbTipoRecepcion.SelectedIndex == 1)
                {
                    ConfigurarGrillasModoTraspaso();

                    var traspasos = _stockFacade.ObtenerTraspasosEnTransito(idSucursalActual);
                    _traspasosPendientes = new List<OrdenTraspasoDTO>(traspasos);

                    dgvOrdenCompra.DataSource = _traspasosPendientes;
                }

                btnIngresar.Enabled = dgvOrdenCompra.Rows.Count > 0;

                // Forzar selección del primer elemento si existe
                if (dgvOrdenCompra.Rows.Count > 0)
                {
                    dgvOrdenCompra.Rows[0].Selected = true;
                    dgvOrdenCompra_SelectionChanged(null, null);
                }
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

        private void dgvOrdenCompra_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow == null) return;

            dgvDetalleOrdenCompra.DataSource = null;

            if (cmbTipoRecepcion.SelectedIndex == 0)
            {
                var oc = (OrdenCompraDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;
                foreach (var det in oc.Detalles) { if (det.CantidadRecibida == 0) det.CantidadRecibida = det.CantidadPedida; }
                dgvDetalleOrdenCompra.DataSource = oc.Detalles;
            }
            else
            {
                var traspaso = (OrdenTraspasoDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;
                // Pre-cargamos lo recibido igual a lo enviado para agilizar
                foreach (var det in traspaso.Detalles) { if (det.CantidadRecibida == 0) det.CantidadRecibida = det.CantidadEnviada; }
                dgvDetalleOrdenCompra.DataSource = traspaso.Detalles;
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow == null) return;
            dgvDetalleOrdenCompra.EndEdit();

            Guid idSucursal = SessionManager.Current.IdSucursalActual.Value;
            string usuario = SessionManager.Current.UsuarioLogueado?.Nombre ?? "Sistema";

            try
            {
                // PROCESAMIENTO ORDEN DE COMPRA
                if (cmbTipoRecepcion.SelectedIndex == 0)
                {
                    var oc = (OrdenCompraDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;
                    var detalles = (IEnumerable<OrdenCompraDetalleDTO>)dgvDetalleOrdenCompra.DataSource;

                    var listaRecepcion = detalles.Select(d => new RecepcionMercaderiaDTO
                    {
                        IdOrdenCompraDetalle = d.IdOrdenCompraDetalle,
                        IdProducto = d.IdProducto,
                        CantidadRealRecibida = d.CantidadRecibida,
                        UnidadesPorBulto = d.UnidadesPorBulto,
                        Observaciones = d.Observaciones
                    }).ToList();

                    if (!listaRecepcion.Any(r => r.CantidadRealRecibida > 0)) { MessageBox.Show("No hay cantidades a ingresar."); return; }

                    if (MessageBox.Show($"¿Confirma el ingreso de la OC N° {oc.NroOrdenCompra}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _stockFacade.RegistrarIngresoPorOrdenCompra(oc.IdOrdenCompra, idSucursal, usuario, listaRecepcion);
                        MessageBox.Show("Ingreso de Proveedor procesado con éxito.");
                        ActualizarPantallaCompleta();
                    }
                }
                // PROCESAMIENTO TRASPASO
                else
                {
                    var traspaso = (OrdenTraspasoDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;
                    var detalles = ((IEnumerable<OrdenTraspasoDetalleDTO>)dgvDetalleOrdenCompra.DataSource).ToList();

                    if (!detalles.Any(r => r.CantidadRecibida > 0)) { MessageBox.Show("No hay cantidades a ingresar."); return; }

                    if (MessageBox.Show($"¿Confirma la recepción del Traspaso N° {traspaso.NroTraspaso} desde Depósito?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // AQUÍ LLAMAS A TU NUEVO MÉTODO DE RECEPCIÓN
                        _stockFacade.RecibirTraspasoEnDestino(traspaso.IdOrdenTraspaso, idSucursal, usuario);
                        MessageBox.Show("Mercadería interna ingresada al inventario del local con éxito.");
                        ActualizarPantallaCompleta();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Operativo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Configuración Dinámica de Grillas

        private void ConfigurarGrillasModoOC()
        {
            dgvOrdenCompra.AutoGenerateColumns = false;
            dgvOrdenCompra.AllowUserToAddRows = false;
            dgvOrdenCompra.RowHeadersVisible = false;
            dgvOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenCompra.MultiSelect = false;
            dgvOrdenCompra.BackgroundColor = Color.White;
            dgvOrdenCompra.BorderStyle = BorderStyle.None;
            dgvOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // COLOR DE SELECCIÓN AZUL CLARO SUAVE
            dgvOrdenCompra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 229, 255); // Azul pastel claro
            dgvOrdenCompra.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvOrdenCompra.Columns.Clear();
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NroOrdenCompra", HeaderText = "N° OC", Width = 80 });
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RazonSocialProveedor", HeaderText = "Proveedor", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            var colFechaOc = new DataGridViewTextBoxColumn { DataPropertyName = "FechaOc", HeaderText = "Fecha", Width = 90 };
            colFechaOc.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvOrdenCompra.Columns.Add(colFechaOc);


            dgvDetalleOrdenCompra.AutoGenerateColumns = false;
            dgvDetalleOrdenCompra.AllowUserToAddRows = false;
            dgvDetalleOrdenCompra.RowHeadersVisible = false;
            dgvDetalleOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleOrdenCompra.BackgroundColor = Color.White;
            dgvDetalleOrdenCompra.BorderStyle = BorderStyle.None;
            dgvDetalleOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // COLOR DE SELECCIÓN AZUL CLARO SUAVE
            dgvDetalleOrdenCompra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 229, 255);
            dgvDetalleOrdenCompra.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvDetalleOrdenCompra.Columns.Clear();
            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CodigoSku", HeaderText = "SKU", Width = 70, ReadOnly = true });
            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductoNombre", HeaderText = "Artículo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            var colPedida = new DataGridViewTextBoxColumn { DataPropertyName = "CantidadPedida", HeaderText = "Pedida", Width = 70, ReadOnly = true };
            colPedida.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleOrdenCompra.Columns.Add(colPedida);

            var colIngresa = new DataGridViewTextBoxColumn { DataPropertyName = "CantidadRecibida", HeaderText = "Ingresa", Width = 80, ReadOnly = false };
            colIngresa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colIngresa.DefaultCellStyle.BackColor = Color.LightYellow; // Destaca la celda editable
            dgvDetalleOrdenCompra.Columns.Add(colIngresa);
        }

        private void ConfigurarGrillasModoTraspaso()
        {
            dgvOrdenCompra.AutoGenerateColumns = false;
            dgvOrdenCompra.AllowUserToAddRows = false;
            dgvOrdenCompra.RowHeadersVisible = false;
            dgvOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenCompra.MultiSelect = false;
            dgvOrdenCompra.BackgroundColor = Color.White;
            dgvOrdenCompra.BorderStyle = BorderStyle.None;
            dgvOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvOrdenCompra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 229, 255);
            dgvOrdenCompra.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvOrdenCompra.Columns.Clear();
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NroTraspaso", HeaderText = "N° Remito", Width = 90 });
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SucursalOrigenNombre", HeaderText = "Origen", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            var colFechaTraspaso = new DataGridViewTextBoxColumn { DataPropertyName = "FechaEmision", HeaderText = "Despachado", Width = 90 };
            colFechaTraspaso.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvOrdenCompra.Columns.Clear(); // Limpiamos bien antes de añadir

            // Reconstrucción limpia de columnas para Traspaso Maestro
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NroTraspaso", HeaderText = "N° Remito", Width = 80 });
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SucursalOrigenNombre", HeaderText = "Depósito Origen", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaEmision", HeaderText = "Fecha Envío", Width = 90 });

            dgvDetalleOrdenCompra.AutoGenerateColumns = false;
            dgvDetalleOrdenCompra.AllowUserToAddRows = false;
            dgvDetalleOrdenCompra.RowHeadersVisible = false;
            dgvDetalleOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleOrdenCompra.BackgroundColor = Color.White;
            dgvDetalleOrdenCompra.BorderStyle = BorderStyle.None;
            dgvDetalleOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDetalleOrdenCompra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 229, 255);
            dgvDetalleOrdenCompra.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvDetalleOrdenCompra.Columns.Clear();
            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CodigoSku", HeaderText = "SKU", Width = 70, ReadOnly = true });
            dgvDetalleOrdenCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductoNombre", HeaderText = "Artículo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            var colEnviada = new DataGridViewTextBoxColumn { DataPropertyName = "CantidadEnviada", HeaderText = "Enviada", Width = 80, ReadOnly = true };
            colEnviada.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleOrdenCompra.Columns.Add(colEnviada);

            var colRecibida = new DataGridViewTextBoxColumn { DataPropertyName = "CantidadRecibida", HeaderText = "Recibe Físico", Width = 90, ReadOnly = false };
            colRecibida.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colRecibida.DefaultCellStyle.BackColor = Color.LightCyan; // Celeste claro para indicar recepción interna
            dgvDetalleOrdenCompra.Columns.Add(colRecibida);
        }

        #endregion

        private void dgvDetalleOrdenCompra_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Valor inválido. Ingrese un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarPantallaCompleta();
        }
    }
}

