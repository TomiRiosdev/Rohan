using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using System.Data;


namespace UI.GestiónStock
{
    public partial class fmsListarProductosPopUp : Form
    {
        private readonly ProductoFacade _productoFacade;
        private List<ProductoDTO> _productosCache = new();
        public ProductoDTO ProductoSeleccionado { get; private set; }

        public fmsListarProductosPopUp
        (
           ProductoFacade productoFacade
        )
        {
            InitializeComponent();
            _productoFacade = productoFacade ?? throw new ArgumentNullException(nameof(productoFacade));

            // Forzamos propiedades de ventana emergente limpia por código
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false; // No genera un ícono extra en la barra de tareas de Windows
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
            cboBuscarPor.SelectedIndex = 0; // "Nombre" por defecto
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
            // Agregamos Name obligatoriamente para evitar errores de indexación
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoSku", DataPropertyName = "CodigoSku", HeaderText = "Código SKU", FillWeight = 70 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Producto", FillWeight = 160 });
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CategoriaNombre", DataPropertyName = "CategoriaNombre", HeaderText = "Categoría", FillWeight = 100 });
        }
        private void CargarProductosOriginales()
        {
            try
            {
                // Traemos los productos del maestro activo y los guardamos en memoria local
                var lista = _productoFacade.ListarProductosActivos();
                _productosCache = lista.ToList();

                // Mostramos todo al arrancar
                dgvProductos.DataSource = _productosCache;
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
            string criterio = cboBuscarPor.Text;
            string textoBusqueda = txtBusqueda.Text.Trim().ToLower();

            // Si el cuadro está vacío, reestablecemos la grilla completa con el caché
            if (string.IsNullOrEmpty(textoBusqueda))
            {
                dgvProductos.DataSource = _productosCache;
                return;
            }

            IEnumerable<ProductoDTO> resultados;

            // Switch simétrico al que usás en tu ERP
            switch (criterio)
            {
                case "Nombre":
                    resultados = _productosCache.Where(p => p.Nombre != null
                        && p.Nombre.ToLower().Contains(textoBusqueda));
                    break;

                case "SKU":
                    resultados = _productosCache.Where(p => p.CodigoSku != null
                        && p.CodigoSku.ToString().Contains(textoBusqueda));
                    break;

                default:
                    resultados = _productosCache;
                    break;
            }

            dgvProductos.DataSource = resultados.ToList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        #endregion

        #region Evento de Confirmación y Cierre
        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validamos que el doble clic sea sobre una fila con datos real y no sobre el encabezado
            if (e.RowIndex >= 0 && dgvProductos.CurrentRow?.DataBoundItem is ProductoDTO prod)
            {
                ProductoSeleccionado = prod;         // Guardamos el elegido en la propiedad pública
                this.DialogResult = DialogResult.OK; // Seteamos el estado de éxito para el formulario padre
                this.Close();                        // Cerramos el Pop-up automáticamente
            }
        }

        #endregion

        private void cboBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            txtBusqueda.Focus();

            // Actualizamos dinámicamente la ayuda visual del Placeholder
            if (cboBuscarPor.Text == "Nombre")
                txtBusqueda.PlaceholderText = "Escriba el nombre del panificado...";
            else
                txtBusqueda.PlaceholderText = "Escriba el código SKU...";
        }
    }
}
