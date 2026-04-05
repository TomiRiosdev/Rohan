using Microsoft.Extensions.DependencyInjection;
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
                var fmsPrincipal = _serviceProvider.GetRequiredService<fmsPrincipal>();

                this.Hide();                    // Oculta el login
                fmsPrincipal.Show();              // Muestra el menú principal     

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el sistema: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
