using BLL.DomainDtos;
using BLL.GestiónCompra.Facade;
using Service.Facade;
using System;


namespace UI.GestionCompra
{
    public partial class fmsHistorialOrdenCompra : Form
    {
        private readonly OrdenCompraFacade _comprasFacade;
        private List<OrdenCompraDTO> _historialCompleto;

        public fmsHistorialOrdenCompra
        (
            OrdenCompraFacade comprasFacade
        )
        {
            InitializeComponent();
            _comprasFacade = comprasFacade ?? throw new ArgumentNullException(nameof(comprasFacade));

        }

        private void fmsHistorialOrdenCompra_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaMaster();
            ConfigurarGrillaDetalle();
            CargarComboEstados();
            CargarHistorialFormulario();
        }
        private void CargarComboEstados()
        {
            // Cargamos las opciones del filtro sincronizadas con tu Enum
            var estados = new[]
            {
                new { Id = 0, Descripcion = "-- Todos los Estados --" },
                new { Id = 1, Descripcion = "Pendiente de Aprobación" },
                new { Id = 2, Descripcion = "Emitida / Enviada" },
                new { Id = 3, Descripcion = "Rechazada / Cancelada" },
                new { Id = 4, Descripcion = "Finalizada / Recibida" }
            };

            cbxBuscar.DataSource = estados;
            cbxBuscar.ValueMember = "Id";
            cbxBuscar.DisplayMember = "Descripcion";
            cbxBuscar.SelectedIndex = 0; // Por defecto muestra "Todos"
        }
        private void CargarHistorialFormulario()
        {
            try
            {
                dgvMasterHistorial.SelectionChanged -= dgvMasterHistorial_SelectionChanged!;

                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión del usuario actual.");

                DateTime fechaDesde = DateTime.Now.AddMonths(-1); 
                DateTime fechaHasta = DateTime.Now;
                var ocs = _comprasFacade.ConsultarHistorial(idSucursalActual, null, null, fechaDesde, fechaHasta);

                // Almacenamos el búfer completo de la sucursal en memoria para los filtros rápidos del ComboBox
                _historialCompleto = new List<OrdenCompraDTO>(ocs);

                // 4. Delegamos el renderizado y la lógica de filtrado reactivo en RAM
                FiltrarYRenderizarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial de auditoría regional: {ex.Message}", "Error de Infraestructura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvMasterHistorial.SelectionChanged += dgvMasterHistorial_SelectionChanged!;
            }
        }
        private void FiltrarYRenderizarGrilla()
        {
            if (_historialCompleto == null) return;

            int estadoSeleccionado = (int)cbxBuscar.SelectedValue!;

            // Filtrado reactivo en memoria RAM sin volver a golpear la base de datos
            List<OrdenCompraDTO> listaFiltrada;
            if (estadoSeleccionado == 0)
            {
                listaFiltrada = _historialCompleto;
            }
            else
            {
                listaFiltrada = _historialCompleto.FindAll(oc => oc.IdEstadoOc == estadoSeleccionado);
            }

            dgvMasterHistorial.DataSource = null;
            dgvMasterHistorial.DataSource = listaFiltrada;

            // Sincronización forzada del detalle
            if (listaFiltrada.Count > 0 && dgvMasterHistorial.CurrentRow != null)
            {
                var seleccion = (OrdenCompraDTO)dgvMasterHistorial.CurrentRow.DataBoundItem;
                dgvDetalleHistorial.DataSource = seleccion?.Detalles;
            }
            else
            {
                dgvDetalleHistorial.DataSource = null;
            }
        }

        private void dgvMasterHistorial_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMasterHistorial.Rows.Count == 0 || dgvMasterHistorial.CurrentRow == null || dgvMasterHistorial.CurrentRow.Index < 0)
            {
                dgvDetalleHistorial.DataSource = null;
                return;
            }

            if (dgvMasterHistorial.CurrentRow.DataBoundItem is OrdenCompraDTO ocSeleccionada)
            {
                dgvDetalleHistorial.DataSource = null;
                dgvDetalleHistorial.DataSource = ocSeleccionada.Detalles;
            }
        }

        private void ConfigurarGrillaMaster()
        {
            dgvMasterHistorial.AutoGenerateColumns = false;
            dgvMasterHistorial.AllowUserToAddRows = false;
            dgvMasterHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMasterHistorial.RowHeadersVisible = false;
            dgvMasterHistorial.BackgroundColor = Color.White;
            dgvMasterHistorial.BorderStyle = BorderStyle.None;

            dgvMasterHistorial.Columns.Clear();

            dgvMasterHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroOrdenCompra",
                DataPropertyName = "NroOrdenCompra", // Match exacto con tu DTO enriquecido
                HeaderText = "N° OC",
                Width = 75,
                ReadOnly = true
            });

            dgvMasterHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RazonSocialProveedor",
                DataPropertyName = "RazonSocialProveedor",
                HeaderText = "Proveedor Comercial",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 75,
                ReadOnly = true
            });

            dgvMasterHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioNombre",
                DataPropertyName = "UsuarioNombre",
                HeaderText = "Comprador",
                Width = 110,
                ReadOnly = true
            });

            var colFecha = new DataGridViewTextBoxColumn
            {
                Name = "FechaOc",
                DataPropertyName = "FechaOc",
                HeaderText = "Fecha",
                Width = 95,
                ReadOnly = true
            };
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvMasterHistorial.Columns.Add(colFecha);

            dgvMasterHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstadoDescripcion",
                DataPropertyName = "EstadoDescripcion",
                HeaderText = "Estado",
                Width = 130,
                ReadOnly = true
            });

            var colTotal = new DataGridViewTextBoxColumn
            {
                Name = "CostoTotal",
                DataPropertyName = "CostoTotal",
                HeaderText = "Monto Total",
                Width = 100,
                ReadOnly = true
            };
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Format = "C2";
            dgvMasterHistorial.Columns.Add(colTotal);
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvDetalleHistorial.AutoGenerateColumns = false;
            dgvDetalleHistorial.AllowUserToAddRows = false;
            dgvDetalleHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleHistorial.RowHeadersVisible = false;
            dgvDetalleHistorial.BackgroundColor = Color.White;
            dgvDetalleHistorial.BorderStyle = BorderStyle.None;

            dgvDetalleHistorial.Columns.Clear();

            dgvDetalleHistorial.Columns.Add(new DataGridViewTextBoxColumn { Name = "Renglon", DataPropertyName = "Renglon", HeaderText = "Rng", Width = 40, ReadOnly = true });
            dgvDetalleHistorial.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoSku", DataPropertyName = "CodigoSku", HeaderText = "SKU", Width = 80, ReadOnly = true });
            dgvDetalleHistorial.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductoNombre", DataPropertyName = "ProductoNombre", HeaderText = "Artículo Auditado", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            var colCant = new DataGridViewTextBoxColumn { Name = "CantidadPedida", DataPropertyName = "CantidadPedida", HeaderText = "Cant. Solicitada", Width = 110, ReadOnly = true };
            colCant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleHistorial.Columns.Add(colCant);

            var colPrecio = new DataGridViewTextBoxColumn { Name = "PrecioPactado", DataPropertyName = "PrecioPactado", HeaderText = "Precio Compra", Width = 100, ReadOnly = true };
            colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrecio.DefaultCellStyle.Format = "C2";
            dgvDetalleHistorial.Columns.Add(colPrecio);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
