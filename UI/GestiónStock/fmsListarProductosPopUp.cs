using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using System.Data;
using System.Globalization;
using System.Text;
using System.Linq;


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

        #region Metodos Privados
        private void ConfigurarFiltrosIniciales()
        {
            cboBuscarPor.Items.Clear();
            cboBuscarPor.Items.Add("Nombre");
            cboBuscarPor.Items.Add("SKU");
            cboBuscarPor.SelectedIndex = 0;
        }
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
        private void FiltrarProductos()
        {
            try
            {

                string busqueda = NormalizarTexto(txtBusqueda.Text.ToLower());
                string criterio = cboBuscarPor.Text;

                IEnumerable<ProductoDTO> resultados;

                if (string.IsNullOrWhiteSpace(busqueda))
                {
                    resultados = _productosDto;
                }
                else
                {


                    switch (criterio)
                    {
                        case "Nombre":
                            resultados = _productosDto.Where(p => p.Nombre.ToLower().Contains(txtBusqueda.Text.ToLower()));
                            break;
                        case "SKU":
                            resultados = _productosDto.Where(p => p.CodigoSku.ToString().Contains(txtBusqueda.Text));
                            break;

                        default:
                            resultados = _productosDto;
                            break;
                    }
                }

                dgvProductos.DataSource = resultados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }
        private string NormalizarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;

            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);

            // Filtra y elimina todos los caracteres que son marcas de tilde/acento
            var chars = textoNormalizado.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);

            return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
        }
        #endregion

        #region Eventos de Controles
        private void cboBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            txtBusqueda.Focus();

            if (cboBuscarPor.Text == "Nombre")
                txtBusqueda.PlaceholderText = "Escriba el nombre del proucto...";
            else
                txtBusqueda.PlaceholderText = "Escriba el código SKU...";
        }
        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.CurrentRow?.DataBoundItem is ProductoDTO prod)
            {
                ProductoSeleccionado = prod;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        #endregion
    }
}
