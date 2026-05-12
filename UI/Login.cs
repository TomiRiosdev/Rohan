using Microsoft.Extensions.DependencyInjection;
using Service.Facade;
using Service.Logic;
using UI.GestiónProducto;

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
                UsuarioService usuarioService = new UsuarioService();
                var usuarioValido = usuarioService.ValidarCredenciales(txtUserName.Text, txtPassword.Text);
                SessionManager.Current.Login(usuarioValido);

                // Logica para sucursales y permisos 
                if (usuarioValido != null)
                {
                    MessageBox.Show("¡Bienvenido, " + usuarioValido.Nombre + "!", "Inicio de Sesión Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fmsPrincipal mainForm = new fmsPrincipal(_serviceProvider);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales inválidas. Por favor, inténtalo de nuevo.", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
