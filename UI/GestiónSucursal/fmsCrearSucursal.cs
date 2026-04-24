using BLL.DomainDtos;
using BLL.GestiónSucursal.Exceptions;
using BLL.GestiónSucursal.Facade;


namespace UI.GestiónSucursal
{
    public partial class fmsCrearSucursal : Form
    {
        private readonly SucursalFacade _sucursalFacade;
        private readonly TipoSucursalFacade _tipoSucursalFacade;
        public fmsCrearSucursal
        (
            SucursalFacade sucursalFacade,
            TipoSucursalFacade tipoSucursalFacade
        )
        {
            InitializeComponent();
            _sucursalFacade = sucursalFacade;
            _tipoSucursalFacade = tipoSucursalFacade;

            CargarCombo();
        }

        private void fmsCrearSucursal_Load(object sender, EventArgs e)
        {
           
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre de la sucursal es obligatorio.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txtCodPostal.Text))
                {
                    if (!int.TryParse(txtCodPostal.Text, out int cod) || cod <= 0)
                    {
                        MessageBox.Show("El código postal debe ser un número positivo.", "Validación",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCodPostal.Focus();
                        return;
                    }
                }
                if(string.IsNullOrWhiteSpace(txtLocalidad.Text))
                {
                    MessageBox.Show("La localidad es obligatorio.", "Validación",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocalidad.Focus();
                    return;
                }
                 if (!string.IsNullOrWhiteSpace(txtTel.Text))
                {
                    if (!int.TryParse(txtTel.Text, out int tel) || tel <= 0)
                    {
                        MessageBox.Show("El teléfono debe ser un número positivo.", "Validación",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTel.Focus();
                        return;
                    }
                }
                if (cbxTipoSucursal.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un tipo de sucursal.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxTipoSucursal.Focus();
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                        if (addr.Address != txtEmail.Text)
                        {
                            MessageBox.Show("El correo electrónico no es válido.", "Validación",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtEmail.Focus();
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("El correo electrónico no es válido.", "Validación",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Focus();
                        return;
                    }
                }

                var sucursalDto = new SucursalDTO
                {
                    Nombre = txtNombre.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Localidad = txtLocalidad.Text.Trim(),
                    CodigoPostal = string.IsNullOrWhiteSpace(txtCodPostal.Text) ? null : int.Parse(txtCodPostal.Text),
                    Email = txtEmail.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTel.Text) ? null : int.Parse(txtTel.Text),
                    IdTipoSucursal = (Guid)cbxTipoSucursal.SelectedValue
                };

                _sucursalFacade.AgregarSucursal(sucursalDto);
                MessageBox.Show("Sucursal creada exitosamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();

            }
            catch (SucursalServiceException ex)
            {
                MessageBox.Show($"Error al crear la sucursal: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Error de formato: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtLocalidad.Clear();
            txtCodPostal.Clear();
            txtEmail.Clear();
            txtTel.Clear();
            cbxTipoSucursal.SelectedIndex = -1;
            txtNombre.Focus();
        }   
    
        private void CargarCombo()
        {
            try
            {
                var tiposSucursal = _tipoSucursalFacade.GetHabilitados();
                cbxTipoSucursal.DataSource = tiposSucursal.ToList();
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
    }
}
