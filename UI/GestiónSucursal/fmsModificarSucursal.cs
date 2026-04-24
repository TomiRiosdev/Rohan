using BLL.DomainDtos;
using BLL.GestiónSucursal.Exceptions;
using BLL.GestiónSucursal.Facade;


namespace UI.GestiónSucursal
{
    public partial class fmsModificarSucursal : Form
    {
        private readonly SucursalFacade _sucursalFacade;
        private readonly TipoSucursalFacade _tipoSucursalFacade;
        private readonly SucursalDTO _sucursalOriginal;

        public fmsModificarSucursal
        (
            SucursalFacade sucursalFacade,
            TipoSucursalFacade tipoSucursalFacade,
            SucursalDTO sucursalAEditar

        )
        {
            InitializeComponent();
            _sucursalFacade = sucursalFacade;
            _tipoSucursalFacade = tipoSucursalFacade;
            _sucursalOriginal = sucursalAEditar;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                _sucursalOriginal.Nombre = txtNombre.Text.Trim();           
                _sucursalOriginal.Localidad = txtLocalidad.Text.Trim();
                _sucursalOriginal.Direccion = txtDireccion.Text.Trim();
                _sucursalOriginal.Telefono = int.Parse(txtTel.Text);
                _sucursalOriginal.CodigoPostal = int.Parse(txtCodPostal.Text);
                _sucursalOriginal.Email = txtEmail.Text.Trim();
                _sucursalOriginal.IdTipoSucursal = (Guid)cbxTipoSucursal.SelectedValue;

                _sucursalFacade.ModificarSucursal(_sucursalOriginal);
                MessageBox.Show("Sucursal modificada exitosamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (SucursalServiceException ex)
            {
                MessageBox.Show($"Error al modificar la sucursal: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un formato válido para Teléfono y Código Postal.", "Error de Formato",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {

              MessageBox.Show($"Ocurrió un error inesperado al modificar la sucursal. Por favor, intente nuevamente.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CargarCombos()
        {
            try
            {
                var tipoSucursal = _tipoSucursalFacade.GetHabilitados();
                cbxTipoSucursal.DataSource = tipoSucursal.ToList();
                cbxTipoSucursal.DisplayMember = "Descripcion";
                cbxTipoSucursal.ValueMember = "Id";
                cbxTipoSucursal.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarDatosDeLaSucursal()
        {
            txtNombre.Text = _sucursalOriginal.Nombre;
            txtTel.Text = _sucursalOriginal.Telefono.ToString();
            txtLocalidad.Text = _sucursalOriginal.Localidad;
            txtDireccion.Text = _sucursalOriginal.Direccion;
            txtCodPostal.Text = _sucursalOriginal.CodigoPostal.ToString();
            txtEmail.Text = _sucursalOriginal.Email;

            cbxTipoSucursal.SelectedValue = _sucursalOriginal.IdTipoSucursal;

        }

        private void fmsModificarSucursal_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatosDeLaSucursal();
        }
    }
}
