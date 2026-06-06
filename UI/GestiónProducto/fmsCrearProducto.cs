using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Facade;



namespace UI.GestiónProducto
{
    public partial class fmsCrearProducto : Form
    {
        private readonly ProductoFacade _productoFacade;
        private readonly CategoriaFacade _categoriaFacade;
        private readonly UnidadMedidaFacade _unidadMedidaFacade;
        public fmsCrearProducto
        (
            ProductoFacade productoFacade,
            CategoriaFacade categoriaFacade,
            UnidadMedidaFacade unidadMedidaFacade
        )
        {
            InitializeComponent();
            _productoFacade = productoFacade;
            _categoriaFacade = categoriaFacade;
            _unidadMedidaFacade = unidadMedidaFacade;

            CargarCombos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de campos obligatorio - Nombre
                if (string.IsNullOrWhiteSpace(TxtNombre.Text ))
                {
                    MessageBox.Show("El nombre del producto es obligatorio.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtNombre.Focus();
            
                    return;
                }

                // Validación de Código SKU 
                if (!string.IsNullOrWhiteSpace(TxtCodigoSku.Text))
                {
                    if (!int.TryParse(TxtCodigoSku.Text, out int sku) || sku <= 0)
                    {
                        MessageBox.Show("El Código SKU debe ser un número positivo.", "Validación",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtCodigoSku.Focus();
                        return;
                    }
                }
                // Validación de tipo de envase comercial
                if (CbxTipoEnvase.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un tipo de envase comercial.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CbxTipoEnvase.Focus();
                    return;
                }

                // Crear el DTO
                var productoDto = new ProductoDTO
                {
                    Nombre = TxtNombre.Text.Trim(),
                    Descripcion = TxtDescripcion?.Text?.Trim(),
                    CodigoSku = string.IsNullOrWhiteSpace(TxtCodigoSku.Text) ? null : int.Parse(TxtCodigoSku.Text),
                    ContenidoPorVenta = string.IsNullOrWhiteSpace(TxtContVenta.Text) ? null : int.Parse(TxtContVenta.Text),
                    IdCategoria = (Guid)CbxCategoria.SelectedValue,
                    IdUnidadMedida = (Guid)CbxUnidadMedida.SelectedValue,
                    IdTipoEnvase = (int)(TipoEnvaseEnum)CbxTipoEnvase.SelectedItem, 
                    CantidadPorBulto = (int)nudCantidadPorEnvase.Value 
                };

                _productoFacade.AgregarProducto(productoDto);

                MessageBox.Show("Producto agregado correctamente.", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();

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
        private void LimpiarFormulario()
        {
            TxtNombre.Clear();
            TxtDescripcion?.Clear();
            TxtCodigoSku.Clear();
            TxtContVenta.Clear();
            CbxCategoria.SelectedIndex = -1;
            CbxUnidadMedida.SelectedIndex = -1;
            CbxTipoEnvase.SelectedIndex = -1;
            nudCantidadPorEnvase.Value = 1;
            TxtNombre.Focus();
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void CargarCombos()
        {
            try
            {
                // Cargar Categorías
                var categorias = _categoriaFacade.GetHabilitados();
                CbxCategoria.DataSource = categorias.ToList();
                CbxCategoria.DisplayMember = "Descripcion";
                CbxCategoria.ValueMember = "Id";
                CbxCategoria.SelectedIndex = -1;

                // Cargar Unidades de Medida
                var unidades = _unidadMedidaFacade.GetHabilitados();
                CbxUnidadMedida.DataSource = unidades.ToList();
                CbxUnidadMedida.DisplayMember = "Descripcion";
                CbxUnidadMedida.ValueMember = "Id";
                CbxUnidadMedida.SelectedIndex = -1;

                CbxTipoEnvase.DataSource = Enum.GetValues(typeof(TipoEnvaseEnum));
                CbxTipoEnvase.SelectedIndex = -1;

                nudCantidadPorEnvase.Minimum = 1;
                nudCantidadPorEnvase.Maximum = 10000;
                nudCantidadPorEnvase.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}