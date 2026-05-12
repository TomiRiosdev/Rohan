using BLL.GestiónSucursal.Facade;
using BLL.GestiónSucursal.Interface;
using Microsoft.Extensions.DependencyInjection;
using Service.DomainModel.Composite;
using Service.Logic;

namespace UI.GestionUsuario
{
    public partial class fmsModificarUsuario : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly SucursalFacade _sucursalFacade;
        private readonly Usuario _usuarioAEditar;
      
      
        public fmsModificarUsuario
        (
            UsuarioService usuarioService,
            SucursalFacade sucursalService,
            Usuario usuarioAEditar
        )
        {

            InitializeComponent();
            _usuarioService = usuarioService;
           _sucursalFacade = sucursalService;
            _usuarioAEditar = usuarioAEditar;
            

        }
        private void fmsModificarUsuario_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatosDelProducto();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                _usuarioAEditar.Username = txtNombreUsuario.Text.Trim();
                _usuarioAEditar.Nombre = txtNombre.Text.Trim();
                _usuarioAEditar.Email = txtEmail.Text.Trim();
                _usuarioAEditar.Telefono = txtTelefono.Text.Trim();
                _usuarioAEditar.IdSucursal = (Guid)cbxSucursal.SelectedValue;

                _usuarioAEditar.Fecha = DateTime.Now; // Actualizamos la fecha de modificación

                _usuarioService.ActualizarUsuario(_usuarioAEditar);
                MessageBox.Show("Usuario actualizado correctamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void CargarDatosDelProducto()
        {
            txtNombreUsuario.Text = _usuarioAEditar.Username;
            txtNombre.Text = _usuarioAEditar.Nombre;
            txtEmail.Text = _usuarioAEditar.Email;
            txtTelefono.Text = _usuarioAEditar.Telefono;
            cbxSucursal.SelectedValue = _usuarioAEditar.IdSucursal;         
        }
      
    }
}
