using BLL.DomainDtos;
using BLL.GestioónProveedor.Exceptions;
using BLL.GestioónProveedor.Facade;

namespace UI.GestiónProveedor
{
    public partial class fmsModificarProveedor : Form
    {
        private readonly ProveedorFacade _proveedorFacade;
        private readonly ProveedorDTO _proveedorOriginal;

        public fmsModificarProveedor
        (
            ProveedorFacade proveedorFacade,
            ProveedorDTO proveedorAEditar

        )
        {
            InitializeComponent();
            _proveedorFacade = proveedorFacade;
            _proveedorOriginal = proveedorAEditar;
        }
        private void fmsModificarProveedor_Load(object sender, EventArgs e)
        {
            CargarDatosDelProveedor();
        }
        private void CargarDatosDelProveedor()
        {
            txtNombre.Text = _proveedorOriginal.Nombre;
            txtRazonSocial.Text = _proveedorOriginal.RazonSocial;
            txtEmail.Text = _proveedorOriginal.Email;
            txtTelefono.Text = _proveedorOriginal.Telefono.ToString();
            mtxtCuit.Text = _proveedorOriginal.Cuit.ToString();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                _proveedorOriginal.Nombre = txtNombre.Text.Trim();
                _proveedorOriginal.RazonSocial = txtRazonSocial.Text.Trim();
                _proveedorOriginal.Email = txtEmail.Text.Trim();
                _proveedorOriginal.Telefono = txtTelefono.Text.Trim();
                _proveedorOriginal.Cuit = mtxtCuit.Text.Trim();

                _proveedorFacade.ModificarProveedor(_proveedorOriginal);

                MessageBox.Show("Proveedor actualizado correctamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ProveedorServiceException ex)
            {
                MessageBox.Show("Error de negocio: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
