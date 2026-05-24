using BLL.GestiónProducto.Facade;
using BLL.GestiónStock.Interface;


namespace UI.GestiónStock
{
    public partial class fmsGestionStock : Form
    {
        private readonly IStockFacade _stockFacade;
        private readonly ProductoFacade _productoFacade;
        public fmsGestionStock
        (
            IStockFacade stockFacade,
            ProductoFacade productoFacade
        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            _productoFacade = productoFacade;
        }
        #region buttons
        private void btnVerInventario_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new fmsInventario(_stockFacade));
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
         //   AbrirFormInPanel(new fmsMermaAlerta(_stockFacade));
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Eventos del Formulario
        private void fmsGestionStock_Load(object sender, EventArgs e)
        {
            // El formulario principal de stock se ejecuta y, por defecto, simula 
            // un clic en el botón de "Ver Inventario" para no arrancar con la pantalla vacía.
            btnVerInventario_Click(this, EventArgs.Empty);
        }
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

        #endregion

    }
}
