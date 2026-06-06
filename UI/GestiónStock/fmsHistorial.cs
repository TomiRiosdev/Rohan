using BLL.DomainDtos;
using BLL.GestiónStock.Interface;
using Service.Facade;
using System.Data;


namespace UI.GestiónStock
{
    public partial class fmsHistorial : Form
    {
        private readonly IStockFacade _stockFacade;
        private List<MovimientoStockDTO> _historialCompleto = new();
        public fmsHistorial
        (
            IStockFacade stockFacade
        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            ConfigurarFiltrosIniciales();
        }

      

        private void fmsHistorial_Load(object sender, EventArgs e)
        {
            ConfigurarDgvHistorial();
            CargarHistorialCompleto();
        }
        #region Configuración de la UI

        private void ConfigurarFiltrosIniciales()
        {
            // Cargamos de forma limpia los tipos de movimientos para filtrar
            cbxTipoMovimiento.Items.Clear();
            cbxTipoMovimiento.Items.Add("Todos los Movimientos");
            cbxTipoMovimiento.Items.Add("Ingreso Manual");
            cbxTipoMovimiento.Items.Add("Ingreso por OC");
            cbxTipoMovimiento.Items.Add("Egreso por Venta");
            cbxTipoMovimiento.Items.Add("Egreso por Merma / Rotura");
            cbxTipoMovimiento.Items.Add("Ajuste de Inventario");

            cbxTipoMovimiento.SelectedIndex = 0; // Selecciona "Todos" por defecto
        }

        private void ConfigurarDgvHistorial()
        {
            // 1. Comportamiento Profesional de la Grilla de Auditoría
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.RowHeadersVisible = false;
            //   dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.ScrollBars = ScrollBars.Both; // Forzamos ambas barras

            // 2. Definición Asimétrica de Columnas
            dgvHistorial.Columns.Clear();

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "FechaMovimientoCorta",
                HeaderText = "Fecha",
                FillWeight = 65
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hora",
                DataPropertyName = "HoraMovimiento",
                HeaderText = "Hora",
                FillWeight = 55
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                DataPropertyName = "UsuarioNombre",
                HeaderText = "Usuario",
                FillWeight = 85
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sku",
                DataPropertyName = "CodigoSku",
                HeaderText = "SKU",
                FillWeight = 60
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Producto",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Materia Prima / Producto",
                FillWeight = 150
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                DataPropertyName = "TipoMovimientoTexto",
                HeaderText = "Movimiento",
                FillWeight = 110
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                DataPropertyName = "Cantidad",
                HeaderText = "Cant. u.",
                FillWeight = 60
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Referencia",
                DataPropertyName = "DocumentoReferencia",
                HeaderText = "Doc. Ref (OC)",
                FillWeight = 95
            });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Observaciones",
                DataPropertyName = "Observaciones",
                HeaderText = "Observaciones / Motivo",
                FillWeight = 160
            });

            // Alineaciones Logísticas Eficientes
            dgvHistorial.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHistorial.Columns["Fecha"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorial.Columns["Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvHistorial.Columns["Referencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        #endregion

        #region Carga y Persistencia de Datos

        private void CargarHistorialCompleto()
        {
            try
            {
                // Usamos la sucursal activa del SessionManager transversal de Rohan
                Guid sucursalId = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión actual.");

                DateTime hasta = DateTime.Now;
                DateTime desde = DateTime.Today.AddDays(-30);

                // Cambiamos la llamada para pasar las fechas que exige tu Facade
                var datos = _stockFacade.ObtenerHistorialKardex(sucursalId, desde, hasta);
                _historialCompleto = datos.OrderByDescending(m => m.FechaMovimiento).ToList();

                AplicarFiltrosGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de auditoría: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Centraliza la lógica de filtrado combinado (Combo de tipo + Caja de texto de producto)
        /// </summary>
        private void AplicarFiltrosGrilla()
        {
            IEnumerable<MovimientoStockDTO> resultados = _historialCompleto;

            // 1. Filtrado por Tipo de Movimiento
            string tipoSeleccionado = cbxTipoMovimiento.Text;
            if (tipoSeleccionado != "Todos los Movimientos")
            {
                resultados = resultados.Where(m => m.TipoMovimientoTexto != null &&
                                                   m.TipoMovimientoTexto.Equals(tipoSeleccionado, StringComparison.OrdinalIgnoreCase));
            }

            // 3. Asignación limpia al DataSource
            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = resultados.ToList();
        }

        #endregion

        #region Eventos de Componentes

        private void cbxTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltrosGrilla();
        }

    

        #endregion

        #region Formateo Estético de Auditoría (Opcional)

        // Vinculá este evento si querés diferenciar visualmente las entradas de las salidas de stock
        private void dgvHistorial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHistorial.Columns[e.ColumnIndex].Name == "Cantidad" && dgvHistorial.Rows[e.RowIndex].DataBoundItem is MovimientoStockDTO dto)
            {
                // Si el movimiento es un egreso (puedes identificarlo por tipo o si la cantidad es negativa)
                if (dto.TipoMovimientoTexto.ToLower().Contains("egreso") || dto.TipoMovimientoTexto.ToLower().Contains("merma"))
                {
                    e.CellStyle.ForeColor = Color.DarkRed; // Texto oscuro para conservar el contraste normativo
                }
                else if (dto.TipoMovimientoTexto.ToLower().Contains("ingreso") || dto.TipoMovimientoTexto.ToLower().Contains("oc"))
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
            }
        }

        #endregion

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
           
            cbxTipoMovimiento.SelectedIndex = 0;
            CargarHistorialCompleto();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltrosGrilla();
        }

        private void dgvHistorial_CellFormatting(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }
      
    }

}

