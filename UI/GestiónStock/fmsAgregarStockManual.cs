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
            this.lblAyudaLogistica.Text = "Utilice el botón Buscar para seleccionar un producto.";
        }

        #region Eventos de Controles

        private void fmsAgregarStockManual_Load(object sender, EventArgs e)
        {
            cxbTipoMovimiento.DataSource = Enum.GetValues(typeof(TipoMovimientoEnum));
            cxmFormatoIngreso.Items.Clear();
            cxmFormatoIngreso.Enabled = false;
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
                    cxmFormatoIngreso.Items.Clear();
                    cxmFormatoIngreso.Enabled = true;

                    if (_productoElegido.CantidadPorBulto > 1)
                    {
                        cxmFormatoIngreso.Items.Add($"{_productoElegido.TipoEnvaseNombre} Cerrado/a ({_productoElegido.CantidadPorBulto} u.)");
                        cxmFormatoIngreso.Items.Add($"Unidades Sueltas ({_productoElegido.UnidadMedidaNombre})");
                    }
                    else
                    {
                        cxmFormatoIngreso.Items.Add($"{_productoElegido.TipoEnvaseNombre} Directo ({_productoElegido.ContenidoPorVenta} {_productoElegido.UnidadMedidaNombre})");
                    }

                    cxmFormatoIngreso.SelectedIndex = 0;
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

                if (cxbTipoMovimiento.SelectedItem == null)
                    throw new Exception("Debe seleccionar un tipo de movimiento válido.");

                if (cxmFormatoIngreso.SelectedItem == null)
                    throw new Exception("Debe seleccionar el formato de ingreso de la mercadería.");

                if (nupCantidad.Value <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero.");

                Guid sucursalId = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión.");


                TipoMovimientoEnum tipoEnum = (TipoMovimientoEnum)cxbTipoMovimiento.SelectedItem;
                int idTipoMovimientoInt = (int)tipoEnum;

                string UsuarioLogueado = "Desconocido";
                if (SessionManager.Current.UsuarioLogueado != null)
                {
                    // Ej: "Gerente de Sucursal - Juan" o el rol que recuperes de tu Session
                    UsuarioLogueado = $"{SessionManager.Current.UsuarioLogueado.Patentes} ({SessionManager.Current.UsuarioLogueado.Nombre})";
                }

                var stockDto = new StockPorSucursalDTO
                {
                    IdProducto = _productoElegido.Id,
                    CantidadTotal = (int)nupCantidad.Value,
                    IdTipoMovimiento = (int)(TipoMovimientoEnum)cxbTipoMovimiento.SelectedItem,
                   
                    Observaciones = $"[{UsuarioLogueado}] {txtObservacion.Text.Trim()}",

                    // PASAMOS LOS FACTORES LOGÍSTICOS DEL MAESTRO AL SERVICIO
                    CantidadPorBulto = _productoElegido.CantidadPorBulto , 
                    ContenidoPorVenta = _productoElegido.ContenidoPorVenta ?? 1,

                    // Si seleccionó el índice 0, significa que ingresa el bulto cerrado entero
                    EsIngresoPorBulto = (cxmFormatoIngreso.SelectedIndex == 0 && _productoElegido.CantidadPorBulto > 1),

                    StockMinimo = 5, // Valores base por defecto o los mapeás si tenés el control
                    StockMaximo = 100
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
