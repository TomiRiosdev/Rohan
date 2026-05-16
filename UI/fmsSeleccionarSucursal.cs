using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.Facade;


namespace UI
{
    public partial class fmsSeleccionarSucursal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SucursalFacade _sucursalFacade;
        public fmsSeleccionarSucursal
        (
            IServiceProvider serviceProvider,
            SucursalFacade sucursalFacade
        )
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _sucursalFacade = sucursalFacade ?? throw new ArgumentNullException(nameof(sucursalFacade));
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validamos que haya seleccionado algo válido
                if (cbxSucursal.SelectedValue == null || cbxSucursal.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione una sucursal para operar.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Extraemos los datos del ComboBox
                Guid idSucursalElegida = (Guid)cbxSucursal.SelectedValue;
                string nombreSucursalElegida = cbxSucursal.Text;

                // 3. Impactamos el Contexto de Ejecución Temporal en el SessionManager
                SessionManager.Current.IdSucursalActual = idSucursalElegida;
                SessionManager.Current.NombreSucursalActual = nombreSucursalElegida;

                // 4. Cerramos el formulario indicando ÉXITO
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la selección: {ex.Message}", "Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarCombos()
        {
            try
            {
                var sucursal = _sucursalFacade.GetHabilitados();
                cbxSucursal.DataSource = sucursal.ToList();
                cbxSucursal.DisplayMember = "Nombre";
                cbxSucursal.ValueMember = "Id";
                cbxSucursal.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fmsSeleccionarSucursal_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }

        
    }
}
