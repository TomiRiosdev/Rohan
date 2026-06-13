using BLL.DomainDtos;
using BLL.GestiónProveedor.Exceptions;
using BLL.GestiónProveedor.Facade;
using Microsoft.Extensions.DependencyInjection;


namespace UI.GestiónProveedor
{
    public partial class fmsCrearProveedor : Form
    {
        private readonly ProveedorFacade _proveedorFacade;

        public fmsCrearProveedor
        (
            ProveedorFacade proveedorFacade      
        )
        {
            InitializeComponent();
            _proveedorFacade = proveedorFacade;    
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validación de campos obligatorios (Nombre y Razón Social)
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MessageBox.Show("El Nombre y la Razón Social son obligatorios.", "Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Validación de CUIT 
                string cuitLimpio = mtxtCuit.Text.Replace("-", "").Replace(" ", "").Trim();
                if (string.IsNullOrWhiteSpace(cuitLimpio) || cuitLimpio.Length != 11 || !long.TryParse(cuitLimpio, out _))
                {
                    MessageBox.Show("El CUIT debe tener exactamente 11 dígitos numéricos.", "Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtxtCuit.Focus();
                    return;
                }

                // 3. Validación de Email 
                if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("El formato del Email no es válido.", "Validación",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Focus();
                        return;
                    }
                }

                // 4. Validación de Teléfono (básica)
                string telefonoLimpio = txtTel.Text.Trim();
                if (!string.IsNullOrWhiteSpace(telefonoLimpio) && telefonoLimpio.Length < 6)
                {
                    MessageBox.Show("El teléfono debe tener al menos 6 dígitos.", "Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTel.Focus();
                    return;
                }

                // 5. Crear DTO
                var proveedorDto = new ProveedorDTO
                {
                    Nombre = txtNombre.Text.Trim(),
                    RazonSocial = txtRazonSocial.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = telefonoLimpio,
                    Cuit = cuitLimpio   // Guardamos sin guiones
                };

                // 5. Llamada a la Fachada
                _proveedorFacade.AgregarProveedor(proveedorDto);

                MessageBox.Show("Proveedor agregado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            catch (ProveedorServiceException ex)
            {
                MessageBox.Show(ex.Message, "Error de Negocio",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtRazonSocial.Clear();
            txtEmail.Clear();
            txtTel.Clear();
            mtxtCuit.Clear();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        { 
            this.Close();

        }
    }
}
