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
            }
            else
            {
                lblAlertaSolicitudes.Text = "✅ No se registran solicitudes pendientes en el almacén.";
                lblAlertaSolicitudes.ForeColor = Color.DarkGreen;
                lblAlertaSolicitudes.BackColor = Color.Honeydew;

            }

        }
      
        #region Botones de navegación
        private void btnSolicitud_Click(object sender, EventArgs e)
        {
            try
            {

                var formSolicitudes = _serviceProvider.GetRequiredService<fmsSolicitudesPendientes>();
                formSolicitudes.SolicitudProcesada += (s, args) => VerificarAlertasDeStock();

                AbrirFormInPanel(formSolicitudes);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al inicializar la pantalla de solicitudes: {ex.Message}",
                                "Error Crítico de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                var formHistorial = _serviceProvider.GetRequiredService<fmsHistorialOrdenCompra>();
                AbrirFormInPanel(formHistorial);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al inicializar la pantalla del historial de órdenes de compra: {ex.Message}",
                                "Error Crítico de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        private void AbrirFormInPanel(Form formHijo)
        {
            if (formHijo == null) return;

            // 1. Limpieza de memoria RAM y desuscripción de eventos del formulario viejo
            if (this.panelContenedor.Controls.Count > 0)
            {
                Form? formAnterior = this.panelContenedor.Controls[0] as Form;

                formAnterior?.Dispose();
                this.panelContenedor.Controls.Clear();
            }

            // 2. Configuración para empotrar la ventana sin comportamiento flotante
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill; 

            // 3. Inyección en el control visual y renderizado
            this.panelContenedor.Controls.Add(formHijo);
            this.panelContenedor.Tag = formHijo;
            formHijo.Show();
        }

        private void btnCatalogoCosto_Click(object sender, EventArgs e)
        {
            try
            {

                var formCatalogo = _serviceProvider.GetRequiredService<fmsCatalogoCostoProducto>();
                AbrirFormInPanel(formCatalogo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al inicializar la pantalla de catálogo de costos: {ex.Message}",
                                "Error Crítico de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
