using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.DateAccess.Implementations;
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
                var usuarioService = _serviceProvider.GetRequiredService<UsuarioService>();
                var permisosRepo = _serviceProvider.GetRequiredService<PermisosRepository>();

                // 2. Validamos credenciales
                var usuarioValido = usuarioService.ValidarCredenciales(txtUserName.Text, txtPassword.Text);

                if (usuarioValido != null)
                {
                    // 3. Cargamos la mochila de privilegios (Composite) antes de decidir el flujo
                    permisosRepo.CargarPrivilegios(usuarioValido);

                    // 4. Seteamos la sesión inicial
                    SessionManager.Current.Login(usuarioValido);

                    // 5. Validamos si es Administrador Real por Patente/Permiso
                    // Usamos el DataKey 
                    bool esAdminGlobal = SessionManager.Current.TienePermiso("Administrador");

                    if (esAdminGlobal)
                    {
                        // Flujo Admin: Debe elegir sucursal obligatoriamente
                        var fmsSelector = _serviceProvider.GetRequiredService<fmsSeleccionarSucursal>();

                        if (fmsSelector.ShowDialog() == DialogResult.OK)
                        {
                            AbrirPrincipal(usuarioValido.Nombre);
                        }
                        else
                        {
                            // Si cancela el selector, cerramos la sesión por seguridad
                            SessionManager.Current.Logout();
                        }
                    }
                    else if (usuarioValido.IdSucursal != null)
                    {
                        SessionManager.Current.Login(usuarioValido);

                        // Si el usuario tiene una sucursal fija (no es Admin)
                        if (usuarioValido.IdSucursal.HasValue)
                        {
                            // Usamos el SucursalService para traer la info de esa sucursal específica
                            var sucursalService = _serviceProvider.GetRequiredService<SucursalFacade>();
                            var sucursal = sucursalService.GetById(usuarioValido.IdSucursal.Value);

                            // Guardamos el nombre en el SessionManager
                            SessionManager.Current.NombreSucursalActual = sucursal.Nombre;
                            AbrirPrincipal(usuarioValido.Nombre);
                        }
                        else
                        {
                            // Usuario sin sucursal y sin ser Admin (Error de seguridad)
                            SessionManager.Current.Logout();
                            throw new Exception("El usuario no tiene una sucursal asignada. Contacte al Administrador.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Credenciales inválidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
