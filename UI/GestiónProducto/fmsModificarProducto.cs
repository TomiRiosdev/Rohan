using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Facade;


namespace UI.GestiónProducto
{
    public partial class fmsModificarProducto : Form
    {
        private readonly ProductoFacade _productoFacade;
        private readonly CategoriaFacade _categoriaFacade;
        private readonly UnidadMedidaFacade _unidadMedidaFacade;
        private readonly ProductoDTO _productoOriginal;
        public fmsModificarProducto
        (
            ProductoFacade productoFacade,
            CategoriaFacade categoriaFacade,
            UnidadMedidaFacade unidadMedidaFacade,
            ProductoDTO productoAEditar
        )
        {
            InitializeComponent();
            _productoFacade = productoFacade;
            _categoriaFacade = categoriaFacade;
            _unidadMedidaFacade = unidadMedidaFacade;
            _productoOriginal = productoAEditar;

        }

        private void ModificarProducto_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatosDelProducto();
            txtCodigoSku.Enabled = false; // No permitimos modificar el SKU
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar Categorías
                var categorias = _categoriaFacade.GetHabilitados();
                cbxCategoria.DataSource = categorias.ToList();
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.SelectedIndex = -1;

                // Cargar Unidades de Medida
                var unidades = _unidadMedidaFacade.GetHabilitados();
                cbxUnidadMedida.DataSource = unidades.ToList();
                cbxUnidadMedida.DisplayMember = "Descripcion";
                cbxUnidadMedida.ValueMember = "Id";
                cbxUnidadMedida.SelectedIndex = -1;

                cbxTipoEnvase.DataSource = Enum.GetValues(typeof(TipoEnvaseEnum));
                cbxTipoEnvase.SelectedIndex = -1;

                // Configuración de límites del control numérico
                nudCantidadPorEnvase.Minimum = 1;
                nudCantidadPorEnvase.Maximum = 10000;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarDatosDelProducto()
        {
            txtNombre.Text = _productoOriginal.Nombre;
            txtCodigoSku.Text = _productoOriginal.CodigoSku.ToString();
            txtDescripcion.Text = _productoOriginal.Descripcion;
            txtContenidoVenta.Text = _productoOriginal.ContenidoPorVenta.ToString();
            cbxCategoria.SelectedValue = _productoOriginal.IdCategoria;
            cbxUnidadMedida.SelectedValue = _productoOriginal.IdUnidadMedida;
            cbxTipoEnvase.SelectedItem = (TipoEnvaseEnum)_productoOriginal.IdTipoEnvase;
            nudCantidadPorEnvase.Value = (decimal)(_productoOriginal.CantidadPorBulto > 0
                                         ? _productoOriginal.CantidadPorBulto : 1);
        }
        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Actualizamos los valores del DTO que ya teníamos
                _productoOriginal.Nombre = txtNombre.Text.Trim();
                _productoOriginal.Descripcion = txtDescripcion.Text.Trim();
                _productoOriginal.ContenidoPorVenta = int.Parse(txtContenidoVenta.Text);
                _productoOriginal.IdCategoria = (Guid)cbxCategoria.SelectedValue;
                _productoOriginal.IdUnidadMedida = (Guid)cbxUnidadMedida.SelectedValue;
                _productoOriginal.IdTipoEnvase = (int)(TipoEnvaseEnum)cbxTipoEnvase.SelectedItem;
                _productoOriginal.CantidadPorBulto = (int)nudCantidadPorEnvase.Value;

                // 2. Llamamos a la Facade para que persista en DB
                _productoFacade.ModificarProducto(_productoOriginal);

                MessageBox.Show("Producto actualizado correctamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ProductoServiceException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("El Código SKU y Contenido por Venta deben ser números válidos.",
                               "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnModificar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
