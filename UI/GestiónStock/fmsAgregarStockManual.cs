using BLL.DomainDtos;
using BLL.Enum;
using BLL.GestiónProducto.Facade;
using BLL.GestiónStock.Interface;
using Service.Facade;
using BLL.GestiónStock.Exceptions;

namespace UI.GestiónStock
{
    public partial class fmsAgregarStockManual : Form
    {
        private readonly IStockFacade _stockFacade;
        private readonly ProductoFacade _productoFacade;
        private ProductoDTO _productoElegido;
        public fmsAgregarStockManual
        (
            IStockFacade stockFacade,
            ProductoFacade productoFacade,
            ProductoDTO productoElegido

        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            _productoFacade = productoFacade;
            _productoElegido = productoElegido;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        #region Eventos de Controles

        private void fmsAgregarStockManual_Load(object sender, EventArgs e)
        {
            cxbTipoMovimiento.DataSource = Enum.GetValues(typeof(TipoMovimientoEnum));
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var popUp = new fmsListarProductosPopUp(_productoFacade))
            {
                popUp.StartPosition = FormStartPosition.CenterParent;
                if (popUp.ShowDialog() == DialogResult.OK)
                {
                    _productoElegido = popUp.ProductoSeleccionado;
                    txtProducto.Text = _productoElegido.Nombre;
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            { 
                if (_productoElegido == null)
                    throw new Exception("Por favor, utilice el botón Buscar para seleccionar un producto.");

                if (cxbTipoMovimiento.SelectedValue == null)
                    throw new Exception("Debe seleccionar un tipo de movimiento válido.");

                Guid sucursalId = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión.");


                TipoMovimientoEnum tipoEnum = (TipoMovimientoEnum)cxbTipoMovimiento.SelectedItem;
                int idTipoMovimientoInt = (int)tipoEnum;

                var stockDto = new StockPorSucursalDTO
                {
                    IdProducto = _productoElegido.Id,
                    CantidadTotal = (int)nupCantidad.Value,
                    IdTipoMovimiento = idTipoMovimientoInt,
                    Observaciones = txtObservacion.Text.Trim(),
                    StockMaximo = 1000
                };

                _stockFacade.RegistrarStockManual(stockDto, sucursalId);

                MessageBox.Show($"¡Ajuste de stock para '{_productoElegido.Nombre}' registrado con éxito!",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (StockValidationException ex)
            {
                MessageBox.Show($"Validación de negocio: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (TechoOperativoException ex)
            {
                MessageBox.Show(ex.Message, "Límite Excedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
