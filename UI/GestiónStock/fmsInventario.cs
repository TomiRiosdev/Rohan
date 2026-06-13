using BLL.DomainDtos;
using BLL.GestiónStock.Interface;
using Service.Facade;


namespace UI.GestiónStock
{
    public partial class fmsInventario : Form
    {
        private readonly IFacade _stockFacade;
        private List<StockPorSucursalDTO> _inventarioCompleto = new();
        public event EventHandler<StockPorSucursalDTO> OnSolicitarConfiguracionMermas;
        public event EventHandler<StockPorSucursalDTO> OnSolicitarVerVencimientos;
     

        public fmsInventario
        (
            IFacade stockFacade
         
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
            dgvInventario.AutoGenerateColumns = false;
            dgvInventario.AllowUserToAddRows = false;
            dgvInventario.AllowUserToDeleteRows = false;
            dgvInventario.ReadOnly = true;
            dgvInventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventario.MultiSelect = false;
            dgvInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventario.RowHeadersVisible = false;
            dgvInventario.BackgroundColor = Color.White;
            dgvInventario.BorderStyle = BorderStyle.None;

            dgvInventario.Columns.Clear();

            // Claves de Identificación
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku",
                HeaderText = "Código SKU",
                FillWeight = 70
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                FillWeight = 150
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CategoriaNombre",
                DataPropertyName = "CategoriaNombre",
                HeaderText = "Categoría",
                FillWeight = 120
            });

            // El paréntesis puro de remanentes sueltos
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BultosVisual",
                DataPropertyName = "BultosVisual",
                HeaderText = "Bultos Cerrados",
                FillWeight = 85
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RemanenteSueltoVisual",
                DataPropertyName = "RemanenteSueltoVisual",
                HeaderText = "Remanente Suelto",
                FillWeight = 90
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalStockUnidadesVisual",
                DataPropertyName = "TotalStockUnidadesVisual",
                HeaderText = "Total Stock Und",
                FillWeight = 85
            });

            // Cantidad Total con el multiplicador analítico
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadTotalVisual",
                DataPropertyName = "CantidadTotalVisual",
                HeaderText = "Cantidad Total",
                FillWeight = 110
            });

            // Parámetros operativos
            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMinimo",
                DataPropertyName = "StockMinimo",
                HeaderText = "Límite Mínimo (Und)",
                FillWeight = 70
            });

            dgvInventario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMaximo",
                DataPropertyName = "StockMaximo",
                HeaderText = "Techo Máximo (Und)",
                FillWeight = 70

            });

            // Alineaciones visuales
            dgvInventario.Columns["StockMinimo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInventario.Columns["StockMaximo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInventario.Columns["BultosVisual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInventario.Columns["RemanenteSueltoVisual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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

        private void dgvInventario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificamos que sea una fila válida y que el objeto sea el DTO correcto
            if (e.RowIndex >= 0 && dgvInventario.Rows[e.RowIndex].DataBoundItem is StockPorSucursalDTO dto)
            {
                //  PRIORIDAD ABSOLUTA: RIESGO DE VENCIMIENTO CRÍTICO / MERMA (Coral Pastel)
                if (dto.TieneLotesVencidos)
                {
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 214, 207); // Coral pastel
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvInventario.Font, FontStyle.Regular);
                }

                // ALERTA INTERMEDIA: BAJO STOCK / REPOSICIÓN REQUERIDA (Amarillo Pastel)
                else if (dto.CantidadTotal <= dto.StockMinimo)
                {
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(236, 245, 159); // Amarillo pastel
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(40, 43, 3); 
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvInventario.Font, FontStyle.Regular);
                }

                //  ALERTA DE EXCESO: SOBRE STOCK (Verde Pastel)
                else if (dto.StockMaximo > 0 && dto.CantidadTotal >= dto.StockMaximo)
                {
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(207, 255, 209); // Verde pastel
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(3, 43, 16);
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvInventario.Font, FontStyle.Regular);
                }

                // ESTADO NORMAL: El stock está equilibrado en la franja correcta
                else
                {
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvInventario.DefaultCellStyle.BackColor;
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.ForeColor = dgvInventario.DefaultCellStyle.ForeColor;
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvInventario.Font, FontStyle.Regular);
                }
            }
        }

        private void dgvInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvInventario.Rows[e.RowIndex].DataBoundItem is StockPorSucursalDTO dto)
            {
                // Le disparamos el objeto completo al padre
                OnSolicitarVerVencimientos?.Invoke(this, dto);
            }

        }

        #endregion

        /// <summary>
        /// Propiedad pública que le permite al padre saber qué fila está seleccionada,
        /// </summary>
        public StockPorSucursalDTO ProductoSeleccionadoActual
        {
            get
            {
                 if (dgvInventario.CurrentRow != null && dgvInventario.CurrentRow.DataBoundItem is StockPorSucursalDTO dto)
                 {
                    return dto;
                 }
                 return null;
            }
        }
        /// <summary>
        /// Método público que actúa como puente para que el padre pueda ordenar 
        /// </summary>
        public void ForzarRefrescoInventario()
        {
            // Llama internamente a tu método privado original
            CargarInventarioCompleto();
        }
    }
}

