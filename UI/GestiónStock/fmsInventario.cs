using BLL.DomainDtos;
using BLL.GestiónStock.Interface;
using Service.Facade;


namespace UI.GestiónStock
{
    public partial class fmsInventario : Form
    {
        private readonly IStockFacade _stockFacade;
        private List<StockPorSucursalDTO> _inventarioCompleto = new();

        public fmsInventario
        (
            IStockFacade stockFacade
        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            ConfigurarFiltrosIniciales();
        }

        private void fmsInventario_Load(object sender, EventArgs e)
        {
            ConfigurarDgv();
            CargarInventarioCompleto();
        }

        #region Configuración de la UI
        private void ConfigurarFiltrosIniciales()
        {
            cboBuscarPor.Items.Clear();
            cboBuscarPor.Items.Add("Nombre");
            cboBuscarPor.Items.Add("SKU");
            cboBuscarPor.Items.Add("Stock Bajo Mínimo");
            cboBuscarPor.SelectedIndex = 0;
        }

        private void ConfigurarDgv()
        {
            // 1. Configuraciones de comportamiento profesional
            dgvInventario.AutoGenerateColumns = false; // Desactivamos el auto-generado para controlar el orden nosotros
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AllowUserToDeleteRows = false;
            dgvInventario.ReadOnly = true;
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.MultiSelect = false;
            dgvInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventario.RowHeadersVisible = false; // Vuela la columna vacía de la izquierda

            // 2. Limpieza y creación manual y simétrica de columnas mapeadas al DTO
            dgvInventario.Columns.Clear();

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoSku", DataPropertyName = "CodigoSku", HeaderText = "Código SKU", FillWeight = 80 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductoNombre", DataPropertyName = "ProductoNombre", HeaderText = "Producto", FillWeight = 150 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "CategoriaNombre", DataPropertyName = "CategoriaNombre", HeaderText = "Categoría", FillWeight = 90 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "EnvasesEnteros", DataPropertyName = "EnvasesEnteros", HeaderText = "Envases/Bultos", FillWeight = 80 });   
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockDetalladoVisual", DataPropertyName = "StockDetalladoVisual", HeaderText = "Estado de Stock", FillWeight = 140 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockMinimo", DataPropertyName = "StockMinimo", HeaderText = "Stock Mínimo", FillWeight = 70 });
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockMaximo", DataPropertyName = "StockMaximo", HeaderText = "Techo Máx.", FillWeight = 70 });

            // Alineamos las columnas numéricas a la derecha para una lectura contable limpia
          
            dgvInventario.Columns["StockMinimo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInventario.Columns["StockMaximo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        #endregion

        #region Carga de Datos
        private void CargarInventarioCompleto()
        {
            try
            {
                // Usamos el SessionManager transversal de Rohan
                Guid sucursalId = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión actual.");

                // Llamada limpia a la fachada unificada
                var datos = _stockFacade.ObtenerConsolidadoPorSucursal(sucursalId);
                _inventarioCompleto = datos.ToList();

                // Cargamos el dgv con la lista completa original
                dgvInventario.DataSource = _inventarioCompleto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtBusquedaLibre.Clear();
            cboBuscarPor.SelectedIndex = 0;
            CargarInventarioCompleto();
        }
        #endregion

        #region Evento de Filtrado
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
               string criterio = cboBuscarPor.Text;
                IEnumerable<StockPorSucursalDTO> resultados;
                switch (criterio)
                {
                    case "Nombre":
                        resultados = _inventarioCompleto.Where(p => p.ProductoNombre != null
                            && p.ProductoNombre.ToLower().Contains(txtBusquedaLibre.Text.ToLower()));
                        break;
                    case "SKU":
                        resultados = _inventarioCompleto.Where(p => p.CodigoSku != null
                            && p.CodigoSku.ToString().Contains(txtBusquedaLibre.Text.ToLower()));
                        break;
                    case "Stock Bajo Mínimo":
                        resultados = _inventarioCompleto.Where(p => p.CantidadTotal <= p.StockMinimo);
                        break;
                    default:
                        resultados = _inventarioCompleto;
                        break;
                }

                // Cargamos la grilla local
                dgvInventario.DataSource = resultados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboBuscarPor_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string criterio = cboBuscarPor.Text;

            txtBusquedaLibre.Enabled = false;
            txtBusquedaLibre.Clear();

            if (criterio == "Nombre")
            {
                txtBusquedaLibre.Enabled = true;
                txtBusquedaLibre.Clear();
                txtBusquedaLibre.Focus();
                txtBusquedaLibre.PlaceholderText = "Ingrese el nombre del Producto...";

            }
            else if (criterio == "SKU")
            {
                txtBusquedaLibre.Enabled = true;
                txtBusquedaLibre.Clear();
                txtBusquedaLibre.Focus();
                txtBusquedaLibre.PlaceholderText = "Ingrese el Codigo SKU...";
            }
            else if (criterio == "Stock Bajo Mínimo")
            {
                txtBusquedaLibre.PlaceholderText = "Filtro automático listo para Ejecutar.";
            }
        }

        private void txtBusquedaLibre_TextChanged_1(object sender, EventArgs e)
        {
            string criterio = cboBuscarPor.Text.ToString();

            if (criterio == "Nombre")
            {
                txtBusquedaLibre.PlaceholderText = "Ingrese el nombre del producto...";

            }
            else if (criterio == "SKU")
            {
                txtBusquedaLibre.PlaceholderText = "Ingrese el codigo SKU...";
            }

        }
        #endregion

        #region Formateo Visual Exigido (Semáforo de Stock)

        // IMPORTANTE: Vinculá este método al evento CellFormatting de tu dgvInventario desde el diseñador
        private void dgvInventario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvInventario.Rows[e.RowIndex].DataBoundItem is StockPorSucursalDTO dto)
            {
                // Regla analítica de negocio en UI: Stock crítico
                if (dto.CantidadTotal <= dto.StockMinimo)
                {
                    // Coral/Rojo claro pastel para el fondo, texto bordó oscuro para conservar el contraste
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;

                    // Opcional: Poner la fuente de la fila en Negrita para resaltar la urgencia
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvInventario.Font, FontStyle.Bold);
                }
            }
        }

        #endregion
    }


}

