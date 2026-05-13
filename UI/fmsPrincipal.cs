using Microsoft.Extensions.DependencyInjection;
using Service.Facade;
using UI.GestiónProducto;
using UI.GestiónProveedor;
using UI.GestiónSucursal;
using UI.GestionUsuario;

namespace UI
{
    public partial class fmsPrincipal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public fmsPrincipal
        (
            IServiceProvider serviceProvider
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            CargarInformacionSucursal();
        }

        private void btnGestionProducto_Click(object sender, EventArgs e)
        {
            var fmsGestiónProducto = _serviceProvider.GetRequiredService<fmsGestiónProducto>();
            this.Hide();
            fmsGestiónProducto.ShowDialog();

        }

        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            var loginForm = _serviceProvider.GetRequiredService<Login>();
            this.Close();
            loginForm.Show();
        }

        private void btnGestionProveedor_Click(object sender, EventArgs e)
        {
            var fmsGestiónProveedor = _serviceProvider.GetRequiredService<fmsGestionProveedor>();
            this.Hide();
            fmsGestiónProveedor.ShowDialog();
        }

        private void btnGestionSucursal_Click(object sender, EventArgs e)
        {
            var fmsGestiónSucursal = _serviceProvider.GetRequiredService<fmsGestionSucursal>();
            this.Hide();
            fmsGestiónSucursal.ShowDialog();
        }

        private void btnGestionUsuario_Click(object sender, EventArgs e)
        {
            var fmsGestiónUsuario = _serviceProvider.GetRequiredService<fmsGestionUsuario>();
            this.Hide();
            fmsGestiónUsuario.ShowDialog();
        }

        private void CargarInformacionSucursal()
        {
            Guid? idActual = SessionManager.Current.IdSucursalActual;

            if (idActual.HasValue)
            {
                // Si tiene valor, mostramos el nombre que guardamos en el login o al cambiar
                lblSucursalDireccion.Text = $"Sucursal: {SessionManager.Current.NombreSucursalActual}";
            }
            else
            {
                lblAdministrador.Visible = true;
                lblAdministrador.Text = "Acceso: Administración Global";
                lblSucursalDireccion.Text = $"Sucursal: {SessionManager.Current.NombreSucursalActual}";
                

            }
        }

        private void fmsPrincipal_Load(object sender, EventArgs e)
        {
            btnCambiarSucursal.Visible = (SessionManager.Current.UsuarioLogueado.IdSucursal == null);
            lblAdministrador.Visible = false;
        }
    }
}
