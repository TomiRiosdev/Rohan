using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using System.Data;


namespace UI.GestiónStock
{
    public partial class fmsListarProductosPopUp : Form
    {
        private readonly ProductoFacade _productoFacade;
        private List<ProductoDTO> _productosDto = new();
        public ProductoDTO ProductoSeleccionado { get; private set; }

        public fmsListarProductosPopUp
        (
           ProductoFacade productoFacade
        )
        {
            InitializeComponent();
            _productoFacade = productoFacade ?? throw new ArgumentNullException(nameof(productoFacade));

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void fmsListarProductosPopUp_Load(object sender, EventArgs e)
        {
            ConfigurarFiltrosIniciales();
            ConfigurarDgv();
            CargarProductosOriginales();
        }
        private void ConfigurarFiltrosIniciales()
        {
            cboBuscarPor.Items.Clear();
            cboBuscarPor.Items.Add("Nombre");
            cboBuscarPor.Items.Add("SKU");
            cboBuscarPor.SelectedIndex = 0;
        }

        #region Configuración y Carga

        private void ConfigurarDgv()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoSku", DataPropertyName = "CodigoSku", HeaderText = "Código SKU", FillWeight = 70 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Producto", FillWeight = 160 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CategoriaNombre", DataPropertyName = "CategoriaNombre", HeaderText = "Categoría", FillWeight = 100 });
        }
        private void CargarProductosOriginales()
        {
            try
            {
                var lista = _productoFacade.ListarProductosActivos();
                _productosDto = lista.ToList();

                dgvProductos.DataSource = _productosDto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar productos en el catálogo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Lógica del Buscador Integrado (Doble Criterio)

        private void FiltrarProductos()
        {
            try
            {

                var todos = _productoFacade.ListarProductosActivos().ToList();
                string criterio = cboBuscarPor.Text;
                IEnumerable<ProductoDTO> resultados;


                switch (criterio)
                {
                    case "Nombre":
                        resultados = todos.Where(p => p.Nombre.ToLower().Contains(txtBusqueda.Text.ToLower()));
                        break;
                    case "SKU":
                        resultados = todos.Where(p => p.CodigoSku.ToString().Contains(txtBusqueda.Text));
                        break;

                    default:
                        resultados = _productosDto;
                        break;
                }

                dgvProductos.DataSource = resultados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }



        private void cboBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            txtBusqueda.Focus();

            if (cboBuscarPor.Text == "Nombre")
                txtBusqueda.PlaceholderText = "Escriba el nombre del panificado...";
            else
                txtBusqueda.PlaceholderText = "Escriba el código SKU...";
        }

        #endregion

        #region Evento de Confirmación y Cierre
        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.CurrentRow?.DataBoundItem is ProductoDTO prod)
            {
                ProductoSeleccionado = prod;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
