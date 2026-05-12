using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.DomainModel.Composite;
using Service.Logic;

namespace UI.GestionUsuario
{
    public partial class fmsAgregarUsuario : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SucursalFacade _sucursalFacade;
        private readonly UsuarioService _usuarioService;
        public fmsAgregarUsuario
        (
            IServiceProvider serviceProvider,
            SucursalFacade sucursalFacade,
            UsuarioService usuarioService
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _sucursalFacade = sucursalFacade;
            _usuarioService = usuarioService;

        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una sucursal.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxSucursal.Focus();
                    return;
                }
                if (txtContraseña.Text != txtConfContraseña.Text)
                {
                    throw new Exception("Las contraseñas no coinciden.");
                }
                var usuarioExistente = new Usuario
                {
                    Username = txtNombreUsuario.Text,
                    Nombre = txtNombre.Text,
                    Password = txtContraseña.Text,
                    Email = txtEmail.Text,
                    Telefono = txtTelefono.Text,
                    Fecha = DateTime.Now,
                    Habilitado = true,
                    IdSucursal = (Guid)cbxSucursal.SelectedValue

                };

               _usuarioService.RegistrarUsuario(usuarioExistente);

                MessageBox.Show($"Usuario '{txtNombreUsuario.Text}' creado con éxito.", "Gestión de Usuarios",
                                MessageBoxButtons.OK, MessageBoxIcon.Information); 
                LimpiarFormulario();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al crear usuario",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        

        private void CargarCombos()
        {
            try
            {
                var sucursal = _sucursalFacade.GetHabilitados();
                cbxSucursal.DataSource = sucursal.ToList();
                cbxSucursal.DisplayMember = "Nombre";
                cbxSucursal.ValueMember = "Id";
                cbxSucursal.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fmsAgregarUsuario_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }

        private void LimpiarFormulario()
        {
            txtNombreUsuario.Clear();
            txtNombre.Clear();
            txtContraseña.Clear();
            txtConfContraseña.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            cbxSucursal.SelectedIndex = -1;
            txtNombreUsuario.Focus();
        }
    }
}
