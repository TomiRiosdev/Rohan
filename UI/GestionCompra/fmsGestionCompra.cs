using BLL.GestiónCompra.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.Facade;

namespace UI.GestionCompra
{
    public partial class fmsGestionCompra : Form
    {
        private readonly OrdenCompraFacade _comprasFacade;
        private readonly IServiceProvider _serviceProvider;
        public fmsGestionCompra
        (
            OrdenCompraFacade comprasFacade,
            IServiceProvider serviceProvider
        )
        {
            _comprasFacade = comprasFacade ?? throw new ArgumentNullException(nameof(comprasFacade));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            InitializeComponent();
        }

        private void fmsGestionCompra_Load(object sender, EventArgs e)
        {
            VerificarAlertasDeStock();
        }
        private void VerificarAlertasDeStock()
        {
            Guid sucursalId = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión actual.");
            // Consultamos directo a la Fachada de Compras
            bool hayPendientesLocal = _comprasFacade.ExistenSolicitudesPendientes(sucursalId);

            if (hayPendientesLocal)
            {
                lblAlertaSolicitudes.Text = "⚠️ ATENCIÓN: Hay Solicitudes de Pedido pendientes de procesar.";
                lblAlertaSolicitudes.ForeColor = Color.DarkRed;
                lblAlertaSolicitudes.BackColor = Color.MistyRose;

                // Opcional: Podés habilitar un pequeño icono de alerta al lado
                //imgAlerta.Visible = true;
            }
            else
            {
                lblAlertaSolicitudes.Text = "✅ No se registran solicitudes pendientes en el almacén.";
                lblAlertaSolicitudes.ForeColor = Color.DarkGreen;
                lblAlertaSolicitudes.BackColor = Color.Honeydew;
                //imgAlerta.Visible = false;
            }

        }

        private void btnSolicitud_Click(object sender, EventArgs e)
        {
            try
            {

                var formSolicitudes = _serviceProvider.GetRequiredService<fmsSolicitudesPendientes>();

                // Si en el futuro necesitas suscribirte a un evento del formulario (ej: avisarle a este padre que actualice el lbl de alertas)
                // formSolicitudes.OnSolicitudProcesada += ActualizarCartelAlertaGeneral;

                // Despachamos al contenedor general para que se dibuje a pantalla completa
                AbrirFormInPanel(formSolicitudes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al inicializar la pantalla de solicitudes: {ex.Message}",
                                "Error Crítico de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AbrirFormInPanel(Form formHijo)
        {
            if (formHijo == null) return;

            // 1. Limpieza de memoria RAM y desuscripción de eventos del formulario viejo
            if (this.panelContenedor.Controls.Count > 0)
            {
                Form? formAnterior = this.panelContenedor.Controls[0] as Form;

                // [Opcional]: Si tus pantallas de compras manejan eventos customizados, los desenganchás acá:
                // if (formAnterior is fmsSolicitudesPendientes formSolViejo)
                // {
                //     formSolViejo.OnAlgúnEvento -= MiMétodoManejador;
                // }

                formAnterior?.Dispose();
                this.panelContenedor.Controls.Clear();
            }

            // 2. Configuración para empotrar la ventana sin comportamiento flotante
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill; // 🚀 Clave para que use el 100% del recuadro gris

            // 3. Inyección en el control visual y renderizado
            this.panelContenedor.Controls.Add(formHijo);
            this.panelContenedor.Tag = formHijo;
            formHijo.Show();
        }

        private void btnCargaManualOC_Click(object sender, EventArgs e)
        {
            try
            {

                var formOrdenCompra = _serviceProvider.GetRequiredService<fmsCargarOrdenCompra>();
                AbrirFormInPanel(formOrdenCompra);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al inicializar la pantalla de orden de compra: {ex.Message}",
                                "Error Crítico de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHistorialEstados_Click(object sender, EventArgs e)
        {
            try
            {

                var formHistorial= _serviceProvider.GetRequiredService<fmsHistorialOrdenCompra>();
                AbrirFormInPanel(formHistorial);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al inicializar la pantalla del historial de órdenes de compra: {ex.Message}",
                                "Error Crítico de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
