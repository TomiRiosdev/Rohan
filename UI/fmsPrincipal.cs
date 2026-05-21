using Microsoft.Extensions.DependencyInjection;
using Service.Facade;
using UI.GestiónProducto;
using UI.GestiónProveedor;
using UI.GestiónStock;
using UI.GestiónSucursal;
using UI.GestionUsuario;

namespace UI
{
    public partial class fmsPrincipal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private Form formularioActivo = null;
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
            AbrirFormularioEnContenedor<fmsGestiónProducto>();
        }

        private void btnCerrarSesión_Click(object sender, EventArgs e)
        {
            var loginForm = _serviceProvider.GetRequiredService<Login>();
            this.Close();
            loginForm.Show();
        }

        private void btnGestionProveedor_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnContenedor<fmsGestionProveedor>();
        }

        private void btnGestionSucursal_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnContenedor<fmsGestionSucursal>();

        }

        private void btnGestionUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnContenedor<fmsGestionUsuario>();
        }

        private void CargarInformacionSucursal()
        {
            Guid? idActual = SessionManager.Current.IdSucursalActual;

            if (idActual.HasValue)
            {
                // Si tiene valor, mostramos el nombre que guardamos en el login o al cambiar
                lblSucursalDireccion.Text = $"Sucursal: {SessionManager.Current.NombreSucursalActual}";
                lblAdministrador.Text = $"Usuario: {SessionManager.Current.UsuarioLogueado.Nombre}";
            }

        }

        private void fmsPrincipal_Load(object sender, EventArgs e)
        {
            btnCambiarSucursal.Visible = (SessionManager.Current.UsuarioLogueado.IdSucursal == null);

        }

        private void btnCambiarSucursal_Click(object sender, EventArgs e)
        {
            // 1. Le pedimos una nueva instancia del selector al ServiceProvider
            var fmsSelector = _serviceProvider.GetRequiredService<fmsSeleccionarSucursal>();

            // 2. Lo mostramos de forma modal flotante
            if (fmsSelector.ShowDialog() == DialogResult.OK)
            {
                // 3. Si el Admin seleccionó una nueva sucursal y le dio a "Ingresar",
                // refrescamos los labels de la interfaz con los nuevos datos del SessionManager
                CargarInformacionSucursal();

                MessageBox.Show($"Cambiando contexto operativo a: {SessionManager.Current.NombreSucursalActual}",
                                "Cambio Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. (Opcional) Si tenés grillas de stock o datos cargados en esta pantalla,
                // acá deberías llamar a sus métodos de actualización para que filtren por el nuevo ID.
            }
        }

        private void btnGestionStock_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnContenedor<fmsGestionStock>();
        }

        private void AbrirFormularioEnContenedor<T>() where T : Form
        {
            // 1. Si ya hay un formulario abierto, lo cerramos para liberar memoria RAM
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            // 2. Pedimos la instancia del formulario al ServiceProvider (Inyección de Dependencias)
            T formularioHijo = _serviceProvider.GetRequiredService<T>();
            formularioActivo = formularioHijo;

            // 3. Configuraciones para que se comporte como un control común dentro del panel
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;

            formularioHijo.Dock = DockStyle.Fill;
            // Agregalo al panel de tu fmsPrincipal (reemplazá "panelContenedor" por el nombre de tu Panel)
            panelContenedor.Controls.Add(formularioHijo);

            // 4. Lógica de centrado dinámico usando el tamaño de tu panel
            //  formularioHijo.Location = new Point(
            //      (panelContenedor.Width - formularioHijo.Width) / 2,
            //      (panelContenedor.Height - formularioHijo.Height) / 2
            //  );

            // 5. Evitamos que se estire feo si se maximiza la pantalla principal
            //   formularioHijo.Anchor = AnchorStyles.None;

            formularioHijo.BringToFront(); // En C# es BringToFront(), ojo con el tipeo de la imagen
            formularioHijo.Show();
        }



    }
}
