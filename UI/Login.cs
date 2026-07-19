using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.DateAccess.Implementations;
using Service.DomainModel.Logging;
using Service.Facade;
using Service.Logic;


namespace UI
{
    public partial class Login : Form
    {
        private readonly IServiceProvider _serviceProvider;
      
        public Login
        (
            IServiceProvider serviceProvider
           
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        private void btnIniciarSesión_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. AUTENTICACIÓN: Delegamos la validación al LoginService
                // Si las credenciales fallan, este método lanza una excepción y el catch la captura.
                var usuarioValido = LoginService.Autenticar(txtUserName.Text, txtPassword.Text);

                var permisosService = _serviceProvider.GetRequiredService<PermisosService>();
              
                permisosService.CargarPrivilegios(usuarioValido);

                SessionManager.Current.Login(usuarioValido);
                // 2. LÓGICA DE SUCURSAL Y PERMISOS
                // Comprobamos si es administrador global mediante el permiso "Administrador"
                bool esAdminGlobal = SessionManager.Current.TienePermiso("Administrador");

                if (esAdminGlobal)
                {
                    // FLUJO ADMIN: Debe elegir sucursal
                    var fmsSelector = _serviceProvider.GetRequiredService<fmsSeleccionarSucursal>();
                    fmsSelector.StartPosition = FormStartPosition.CenterParent;

                    if (fmsSelector.ShowDialog() == DialogResult.OK)
                    {
                        var sucursal = fmsSelector.SucursalSeleccionada;

                        LoginService.FinalizarLogin(usuarioValido, sucursal.IdSucursal, sucursal.Nombre);
                        AbrirPrincipal(usuarioValido.Nombre);
                    }
                    else
                    {

                        return;
                    }
                }
                else
                {
                    // FLUJO USUARIO ESTÁNDAR: Validación de sucursal fija
                    if (!usuarioValido.IdSucursal.HasValue)
                        throw new Exception("El usuario no tiene una sucursal asignada. Contacte al Administrador.");

                    // Obtenemos info de la sucursal
                    var sucursalService = _serviceProvider.GetRequiredService<SucursalFacade>();
                    var sucursal = sucursalService.GetById(usuarioValido.IdSucursal.Value);

                    // Finalizamos el login
                    LoginService.FinalizarLogin(usuarioValido, sucursal.Id, sucursal.Nombre);
                    AbrirPrincipal(usuarioValido.Nombre);
                }
            }
            catch (Exception ex)
            {
                // Manejo centralizado de errores: usuario o contraseña incorrectos, 
                // fallos de red o falta de sucursal.
                MessageBox.Show(ex.Message, "Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void AbrirPrincipal(string Usuario)
        {
            MessageBox.Show($"¡Bienvenido, {Usuario}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var mainForm = _serviceProvider.GetRequiredService<fmsPrincipal>();
            mainForm.Show();
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var formRecuperar = new fmsRecuperarContraseña())
            {
                if (formRecuperar.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Contraseña actualizada con éxito. Ya puede ingresar.");
                }
            }
        }
    }
}
