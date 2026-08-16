using BLL.DomainDtos;
using BLL.GestiónCompra.Facade;
using BLL.GestiónProducto.Facade;
using BLL.GestiónStock.Facade;
using Microsoft.Extensions.DependencyInjection;


namespace UI.GestiónStock
{
    public partial class fmsGestionStock : Form
    {
        private readonly StockFacade _stockFacade;
        private readonly ProductoFacade _productoFacade;
        private readonly OrdenCompraFacade _comprasFacade;
        private readonly IServiceProvider _serviceProvider;

        public fmsGestionStock
        (
            StockFacade stockFacade,
            ProductoFacade productoFacade,
            OrdenCompraFacade comprasFacade,
            IServiceProvider serviceProvider
        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            _productoFacade = productoFacade;
            _serviceProvider = serviceProvider;
            _comprasFacade = comprasFacade;
        }
        #region buttons
        private void btnVerInventario_Click(object sender, EventArgs e)
        {
            var formInventario = new fmsInventario(_stockFacade);


            formInventario.OnSolicitarVerVencimientos += VerVencimientosForms;


            formInventario.OnSolicitarConfiguracionMermas += SolicitudMermasAlertaForms;

            // Despachamos al contenedor general
            AbrirFormInPanel(formInventario);
        }

        private void btnAgregarManual_Click(object sender, EventArgs e)
        {
            using (var frmManual = new fmsAgregarStockManual(_stockFacade, _productoFacade, null))
            {
                frmManual.StartPosition = FormStartPosition.CenterParent;

                if (frmManual.ShowDialog() == DialogResult.OK)
                {
                    if (this.panelContenedor.Controls.Count > 0 && this.panelContenedor.Controls[0] is fmsInventario formInventarioActivo)
                    {
                        formInventarioActivo.ForzarRefrescoInventario();
                    }
                }
            }
        }

        private void btnAgregarPorOC_Click(object sender, EventArgs e)
        {
            try
            {
                var fmsAgregarPorOC = _serviceProvider.GetRequiredService<fmsAgregarStockPorOC>();
                AbrirFormInPanel(fmsAgregarPorOC);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al incrustar el formulario de Agregar Stock por OC: {ex.Message}",
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSolicitudPedido_Click(object sender, EventArgs e)
        {
            try
            {
                var fmsSolicitud = _serviceProvider.GetRequiredService<fmsSolicitudPedido>();
                AbrirFormInPanel(fmsSolicitud);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al incrustar la Solicitud de Pedido: {ex.Message}",
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMermaAlerta_Click(object sender, EventArgs e)
        {
            if (this.panelContenedor.Controls.Count > 0 && this.panelContenedor.Controls[0] is fmsInventario formInv)
            {
                if (formInv.ProductoSeleccionadoActual != null)
                {
                    SolicitudMermasAlertaForms(formInv, formInv.ProductoSeleccionadoActual);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un producto de la lista para configurar sus alertas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                btnVerInventario_Click(this, EventArgs.Empty);
                MessageBox.Show("Seleccione un producto del inventario para configurar mermas.", "Gestión de Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new fmsHistorial(_stockFacade));
        }


        private void btnTraspaso_Click(object sender, EventArgs e)
        {
            try
            {
                var fmsSolicitudTraspaso = _serviceProvider.GetRequiredService<fmsTraspasoSucursal>();
                AbrirFormInPanel(fmsSolicitudTraspaso);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de infraestructura al incrustar la Solicitud de Pedido: {ex.Message}",
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos Generales del Formulario Contenedor
        private void fmsGestionStock_Load(object sender, EventArgs e)
        {
            btnVerInventario_Click(this, EventArgs.Empty);
        }

        #endregion

        #region Motor de Renderizado (Sub-Paneles Dinámicos)

        private void AbrirFormInPanel(Form formHijo)
        {
            if (formHijo == null) return;

            // 1. Si ya hay un formulario adentro, lo limpiamos y liberamos memoria RAM
            if (this.panelContenedor.Controls.Count > 0)
            {
                // Al hacer Dispose(), nos aseguramos de que el formulario viejo se destruya correctamente
                Form? formAnterior = this.panelContenedor.Controls[0] as Form;

                if (formAnterior is fmsInventario formInvViejo)
                {
                    formInvViejo.OnSolicitarVerVencimientos -= VerVencimientosForms;
                    formInvViejo.OnSolicitarConfiguracionMermas -= SolicitudMermasAlertaForms;
                }

                formAnterior?.Dispose();
                this.panelContenedor.Controls.Clear();
            }

            // 2. Configuración obligatoria de WinForms para empotrar ventanas
            formHijo.TopLevel = false;                          // Le quita el comportamiento de ventana flotante nativa
            formHijo.FormBorderStyle = FormBorderStyle.None;    // Le vuela la barra azul de arriba, los botones de cerrar y minimizar
            formHijo.Dock = DockStyle.Fill;                     // Obliga al formulario hijo a estirarse al 100% del panel contenedor

            // 3. Lo agregamos físicamente al control visual y lo mostramos
            this.panelContenedor.Controls.Add(formHijo);
            this.panelContenedor.Tag = formHijo;
            formHijo.Show();
        }

        /// <summary>
        /// Manejador del evento. Se ejecuta cuando el hijo gatilla el DoubleClick en la grilla.
        /// </summary>
        private void SolicitudMermasAlertaForms(object sender, StockPorSucursalDTO productoElegido)
        {
            if (productoElegido == null) return;

            using (var popUp = new fmsMermaAlerta(_stockFacade, productoElegido))
            {
                popUp.StartPosition = FormStartPosition.CenterParent;
                if (popUp.ShowDialog() == DialogResult.OK)
                {
                    if (sender is fmsInventario formInventarioActivo)
                    {
                        formInventarioActivo.ForzarRefrescoInventario();
                    }
                }
            }

        }
        private void VerVencimientosForms(object sender, StockPorSucursalDTO productoElegido)
        {
            if (productoElegido == null) return;

            using (var popUpVencimientos = new fmsVencimientosProducto(_stockFacade, productoElegido))
            {
                popUpVencimientos.StartPosition = FormStartPosition.CenterParent;

                if (popUpVencimientos.ShowDialog() == DialogResult.OK || true)
                {
                    if (sender is fmsInventario formInventarioActivo)
                    {
                        formInventarioActivo.ForzarRefrescoInventario();
                    }
                }
            }
        }

        #endregion

    }
}
