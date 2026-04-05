using BLL.DomainDtos;
using BLL.GestiónProducto.Exceptions;
using BLL.GestiónProducto.Facade;


namespace UI.GestiónProducto
{
    public partial class fmsCrudCatUMed : Form
    {
        private readonly CategoriaFacade _categoriaFacade;
        private readonly UnidadMedidaFacade _unidadMedidaFacade;

        public fmsCrudCatUMed
        (
            CategoriaFacade categoriaFacade,
            UnidadMedidaFacade unidadMedidaFacade
        )
        {
            InitializeComponent();
            _categoriaFacade = categoriaFacade ?? throw new ArgumentNullException(nameof(categoriaFacade));
            _unidadMedidaFacade = unidadMedidaFacade ?? throw new ArgumentNullException(nameof(unidadMedidaFacade));
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCategoria.Text))
                {
                    MessageBox.Show("El nombre del producto es obligatorio.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategoria.Focus();
                    return;
                }

                var categoria = new CategoriaDTO
                {
                    Descripcion = txtCategoria.Text.Trim()
                };


                _categoriaFacade.AgregarCategoria(categoria);
                MessageBox.Show("Categoría agregada exitosamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCategoria.Clear();
            }
            catch (CategoriaServiceException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarUnidadMedida_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUnidadMedida.Text))
                {
                    MessageBox.Show("El nombre del producto es obligatorio.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUnidadMedida.Focus();

                    return;
                }

                var unidadmedida = new UnidadMedidaDTO
                {
                    Descripcion = txtUnidadMedida.Text.Trim()
                };

                _unidadMedidaFacade.AgregarUnidadMedidad(unidadmedida);
                MessageBox.Show("Unidad de medida agregada exitosamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtUnidadMedida.Clear();
            }
            catch (UnidadMedidaServiceException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
