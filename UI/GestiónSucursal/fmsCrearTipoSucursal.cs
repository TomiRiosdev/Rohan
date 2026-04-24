using BLL.DomainDtos;
using BLL.GestiónSucursal.Exceptions;
using BLL.GestiónSucursal.Facade;


namespace UI.GestiónSucursal
{
    public partial class fmsCrearTipoSucursal : Form
    {
        private readonly SucursalFacade _sucursalFacade;
        private readonly TipoSucursalFacade _tipoSucursalFacade;
        public fmsCrearTipoSucursal
        (
             TipoSucursalFacade tipoSucursalFacade,
             SucursalFacade sucursalFacade
        )
        {
            InitializeComponent();
            _sucursalFacade = sucursalFacade;
            _tipoSucursalFacade = tipoSucursalFacade;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del producto es obligatorio.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                var tipoSucursal = new TipoSucursalDTO
                {
                    Descripcion = txtNombre.Text.Trim()
                };


                _tipoSucursalFacade.Agregar(tipoSucursal);
                MessageBox.Show("Tipo de sucursal agregada exitosamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombre.Clear();
            }
            catch (TipoSucursalException ex)
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
    }
 }
