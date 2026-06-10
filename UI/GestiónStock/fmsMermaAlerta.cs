using BLL.DomainDtos;
using BLL.GestiónStock.Interface;


namespace UI.GestiónStock
{
    public partial class fmsMermaAlerta : Form
    {
        private readonly IFacade _stockFacade;
        private readonly StockPorSucursalDTO _productoOriginal;

        public fmsMermaAlerta
        (
            IFacade stockFacade,
            StockPorSucursalDTO productoOriginal


        )
        {
            InitializeComponent();
            _stockFacade = stockFacade; 
            _productoOriginal = productoOriginal;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            txtSku.Enabled = false;
            txtProducto.Enabled = false;
        }

        private void fmsMermaAlerta_Load(object sender, EventArgs e)
        {
            CargarCamposDesdeDTO();
        }

        /// <summary>
        /// Rellena los controles de la interfaz usando los datos que ya traía el DTO,
        /// sin necesidad de hacer un viaje extra a la base de datos por el nombre o el SKU.
        /// </summary>
        private void CargarCamposDesdeDTO()
        {
            // Datos fijos de lectura (Vienen del maestro cruzado en el consolidado)
            txtSku.Text = _productoOriginal.CodigoSku.ToString();
            txtProducto.Text = _productoOriginal.ProductoNombre;

            // Límites Operativos de Stock
            nudMinimo.Value = _productoOriginal.StockMinimo;
            nudMaximo.Value = _productoOriginal.StockMaximo;

            //  Plantilla de Vencimiento Base (Días de vida útil y alerta de la tabla Productos)
            nudVidaUtil.Value = _productoOriginal.DiasVidaUtil ?? 0;
            nudDiasAlerta.Value = _productoOriginal.DiasAlertaVencimiento ?? 0;
        }
        /// <summary>
        /// valida las entradas del usuario, construye un DTO consolidado y se lo envía a la capa BLL
        /// para que lo procese y guarde en la base de datos. Si todo sale bien, cierra el formulario con un resultado OK; 
        /// si hay errores de validación, muestra un mensaje al usuario.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones de Consistencia Lógica
                if (nudMinimo.Value >= nudMaximo.Value)
                    throw new Exception("El stock mínimo no puede ser igual o mayor al techo máximo operativo.");

                if (nudDiasAlerta.Value > nudVidaUtil.Value && nudVidaUtil.Value > 0)
                    throw new Exception("Los días de alerta preventiva no pueden superar a la vida útil total del producto.");

                // Armamos el DTO consolidado para enviar a la capa BLL usando el ID de nuestra referencia privada
                var dtoAlertas = new ConfiguracionAlertasDTO
                {
                    IdProducto = _productoOriginal.IdProducto,
                    StockMinimo = (int)nudMinimo.Value,
                    StockMaximo = (int)nudMaximo.Value,
                    DiasVidaUtil = nudVidaUtil.Value > 0 ? (int)nudVidaUtil.Value : (int?)null,
                    DiasAlertaVencimiento = nudDiasAlerta.Value > 0 ? (int)nudDiasAlerta.Value : (int?)null
                };

                _stockFacade.GuardarConfiguracionAlertas(dtoAlertas);

                MessageBox.Show("Parámetros de control de stock y mermas actualizados con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación de Alertas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
      
        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
    

