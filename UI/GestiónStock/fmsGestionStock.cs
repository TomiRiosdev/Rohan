using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using BLL.GestiónStock;
using BLL.GestiónStock.Interface;


namespace UI.GestiónStock
{
    public partial class fmsGestionStock : Form
    {
        private readonly IStockFacade _stockFacade;
        private readonly ProductoFacade _productoFacade;
        private readonly IMermaService _mermaService;
        public fmsGestionStock
        (
            IStockFacade stockFacade,
            ProductoFacade productoFacade,
            IMermaService mermaService
        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            _productoFacade = productoFacade;
            _mermaService = mermaService;
        }
        #region buttons
        private void btnVerInventario_Click(object sender, EventArgs e)
        {
            var formInventario = new fmsInventario(_stockFacade);

            //  Escuchamos cuando la grilla pida configurar
            formInventario.OnSolicitarConfiguracionMermas += EscucharSolicitudMermas;

            // Despachamos al contenedor general
            AbrirFormInPanel(formInventario);
        }

        private void btnAgregarManual_Click(object sender, EventArgs e)
        {
            // Lo levantamos de forma flotante con 'using' para liberar la memoria RAM al cerrar
            using (var frmManual = new fmsAgregarStockManual(_stockFacade, _productoFacade, null))
            {
                frmManual.StartPosition = FormStartPosition.CenterParent; // Clave para que aparezca centrado

                // Si el usuario guardó con éxito, al volver podemos refrescar la grilla de fondo automáticamente
                if (frmManual.ShowDialog() == DialogResult.OK)
                {

                    btnVerInventario_Click(this, EventArgs.Empty);
                }
            }
        }
   
        private void btnAgregarPorOC_Click(object sender, EventArgs e)
        {
            //   AbrirFormInPanel(new fmsAgregarStockPorOC(_stockFacade));
        }

        private void btnSolicitudPedido_Click(object sender, EventArgs e)
        {

        }

        private void btnMermaAlerta_Click(object sender, EventArgs e)
        {
            if (this.panelContenedor.Controls.Count > 0 && this.panelContenedor.Controls[0] is fmsInventario formInv)
            {
                // Usamos la propiedad pública que el formulario de inventario expone para obtener el producto seleccionado actualmente en la grilla.
                if (formInv.ProductoSeleccionadoActual != null)
                {
                    // Gatillamos el flujo pasándole el DTO que el hijo nos dio
                    EscucharSolicitudMermas(formInv, formInv.ProductoSeleccionadoActual);
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
        #endregion

        #region Eventos Generales del Formulario Contenedor
        private void fmsGestionStock_Load(object sender, EventArgs e)
        {
            // El formulario principal de stock se ejecuta y, por defecto, simula 
            // un clic en el botón de "Ver Inventario" para no arrancar con la pantalla vacía.
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
        private void EscucharSolicitudMermas(object sender, StockPorSucursalDTO productoElegido)
        {
            if (productoElegido == null) return;

            using (var popUp = new fmsMermaAlerta(_mermaService, productoElegido))
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

            #endregion

        }
    }
}
