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
        private void fmsAgregarStockManual_Load(object sender, EventArgs e)
        { 
            CargarTiposMovimientoManuales();
        }

        #region Carga de Datos Locales

        private void CargarTiposMovimientoManuales()
        {
            try
            {
                // 1. Vamos a buscar los tipos reales que registraste en SQL Server
                var todosLosTipos = _stockFacade.ListarTiposMovimiento();

                // 2. Filtramos para que en esta pantalla de ajuste solo aparezcan los operativos manuales
                var tiposPermitidos = todosLosTipos.Where(t =>
                    t.Descripcion == "IngresoManual" || t.Descripcion == "EgresoPorMerma"
                ).ToList();

                // 3. Enlazamos al ComboBox de forma inteligente
                cxbTipoMovimiento.DataSource = tiposPermitidos;
                cxbTipoMovimiento.DisplayMember = "Descripcion";       // Lo que lee el panadero ("IngresoManual")
                cxbTipoMovimiento.ValueMember = "IdTipoMovimiento";   // El GUID oculto de la base de datos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar los tipos de movimientos: " + ex.Message,
                                "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos de Controles

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
                // 1. Validaciones básicas de interfaz de usuario
                if (_productoElegido == null)
                    throw new Exception("Por favor, utilice el botón Buscar para seleccionar un producto.");

                if (cxbTipoMovimiento.SelectedValue == null)
                    throw new Exception("Debe seleccionar un tipo de movimiento válido.");

                Guid sucursalId = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó una sucursal activa en la sesión.");

                // 2. EXTRACCIÓN DEL GUID OCULTO: Recuperamos la clave primaria exacta de SQL Server
                Guid idTipoMovimientoReal = (Guid)cxbTipoMovimiento.SelectedValue;

                // 3. Construcción del DTO enriquecido para mandar a las capas inferiores
                var stockDto = new StockPorSucursalDTO
                {
                    IdProducto = _productoElegido.Id,
                    CantidadTotal = (int)nupCantidad.Value,
                    IdTipoMovimiento = idTipoMovimientoReal, // <-- Viaja el GUID feliz a tu lógica de Kardex
                    StockMinimo = 10,
                    StockMaximo = 1000,
                    Observaciones = txtObservaciones.Text.Trim() // Si agregaste el cuadro de texto para auditoría
                };

                // 4. Inyección atómica en BLL -> DAL -> SQL Server
                _stockFacade.RegistrarStockManual(stockDto, sucursalId);

                // 5. Feedback de éxito comercial
                MessageBox.Show($"¡Ajuste de stock para '{_productoElegido.Nombre}' registrado con éxito!",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Indica a la pantalla de inventario de fondo que debe refrescar la grilla
                this.Close();
            }
            // Captura de excepciones específicas de tu capa de negocio para un software robusto
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
    }

}
