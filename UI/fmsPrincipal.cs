using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.Facade;
using UI.GestionCompra;
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
            LoginService.Logout();
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
            AplicarSeguridadUI(this.Controls);
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

        private void btnGestionCompra_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnContenedor<fmsGestionCompra>();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
  
           AbrirFormularioEnContenedor<fmsBitacora>();
        }

        private void AbrirFormularioEnContenedor<T>() where T : Form
        {
            //  Si ya hay un formulario abierto, lo cerramos para liberar memoria RAM
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            // Pedimos la instancia del formulario al ServiceProvider (Inyección de Dependencias)
            T formularioHijo = _serviceProvider.GetRequiredService<T>();
            formularioActivo = formularioHijo;

            // Configuraciones para que se comporte como un control común dentro del panel
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;

            formularioHijo.Dock = DockStyle.Fill;

            panelContenedor.Controls.Add(formularioHijo);

            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        private void AplicarSeguridadUI(Control.ControlCollection controles)
        {
            foreach (Control c in controles)
            {
                // Si el control tiene un Tag, validamos contra el SessionManager
                if (c.Tag != null && !string.IsNullOrEmpty(c.Tag.ToString()))
                {
                    string permisoRequerido = c.Tag.ToString();

                    // Usamos tu método del SessionManager
                    bool tieneAcceso = SessionManager.Current.TienePermiso(permisoRequerido);

                    // Ocultamos si no tiene permiso
                    c.Visible = tieneAcceso;
                }

                // Si el control tiene hijos (como un Panel, GroupBox o ToolStrip), llamamos recursivamente
                if (c.HasChildren)
                {
                    AplicarSeguridadUI(c.Controls);
                }
            }
        }

     

    }
}
