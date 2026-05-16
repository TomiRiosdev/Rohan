using BLL.DomainDtos;
using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace UI.GestiónSucursal
{
    public partial class fmsGestionSucursal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SucursalFacade _sucursalFacade;
        private readonly TipoSucursalFacade _tipoSucursalFacade;

        private bool _viendoDeshabilitados = false; // Variable para rastrear si se están viendo sucursales deshabilitadas

        public fmsGestionSucursal
        (
            IServiceProvider serviceProvider,
            SucursalFacade sucursalFacade,
            TipoSucursalFacade tipoSucursalFacade

        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _sucursalFacade = sucursalFacade;
            _tipoSucursalFacade = tipoSucursalFacade;

            ConfigurarFiltrosInciales();
            ConfigurarDataGridView();
            
        }

        private void fmsGestionSucursal_Load(object sender, EventArgs e)
        {
            CargarSucursales();
            btnActivar.Enabled = false;
        }

        #region BOTONES
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmCrear = _serviceProvider.GetRequiredService<fmsCrearSucursal>();
            frmCrear.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una sucursal para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var sucursalSeleccionada = (SucursalDTO)dgvSucursal.CurrentRow.DataBoundItem;

            using (var fmsModificar = new fmsModificarSucursal(_sucursalFacade, _tipoSucursalFacade, sucursalSeleccionada))
            {
                if (fmsModificar.ShowDialog() == DialogResult.OK)
                {
                    CargarSucursales(); // Recarga las su
                }
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una sucursal para deshabilitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var sucursalSeleccionada = (SucursalDTO)dgvSucursal.CurrentRow.DataBoundItem;

            var resul = MessageBox.Show($"¿Está seguro que desea deshabilitar la sucursal '{sucursalSeleccionada.Nombre}'?", "Confirmar Deshabilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resul == DialogResult.Yes)
            {
                try
                {
                    _sucursalFacade.BajaLogica(sucursalSeleccionada.Id);
                    MessageBox.Show("Sucursal deshabilitada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarSucursales(); // Recarga las sucursales después de deshabilitar
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al deshabilitar la sucursal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnListarDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoDeshabilitados = !_viendoDeshabilitados; // Alterna el estado

            if (_viendoDeshabilitados)
            {
                btnListarDeshabilitados.Text = "Ver Habilitados";
                btnBuscar.Enabled = false;
                btnActivar.Enabled = true;
                btnAgregar.Enabled = false;
                btnActualizar.Enabled = false;
                btnModificar.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnAgregarTipoSucursal.Enabled = false;
                CargarSucursalesDeshabilitadas();
            }
            else
            {
                btnListarDeshabilitados.Text = "Ver Deshabilitados";
                btnBuscar.Enabled = true;
                btnActivar.Enabled = false;
                btnAgregar.Enabled = true;
                btnActualizar.Enabled = true;
                btnModificar.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnAgregarTipoSucursal.Enabled = true;
                CargarSucursales();
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una sucursal para habilitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var sucursalSeleccionada = (SucursalDTO)dgvSucursal.CurrentRow.DataBoundItem;

            if (MessageBox.Show($"¿Está seguro que desea habilitar la sucursal '{sucursalSeleccionada.Nombre}'?", "Confirmar Habilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _sucursalFacade.HabilitarSucursal(sucursalSeleccionada.Id);
                    MessageBox.Show("Sucursal habilitada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarSucursalesDeshabilitadas(); // Recarga las sucursales deshabilitadas después de habilitar
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al habilitar la sucursal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cbxBuscar.SelectedIndex = 0;
            CargarSucursales();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var todos = _sucursalFacade.GetHabilitados();
                string criterio = cbxBuscar.Text;

                if (string.IsNullOrEmpty(criterio))
                {
                    MessageBox.Show("Seleccione un criterio de búsqueda.");
                    return;
                }
                IEnumerable<SucursalDTO> resultado;

                switch (criterio)
                {
                    case "Nombre":
                        resultado = todos.Where(s => s.Nombre.ToLower().Contains(txtBuscar.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "Tipo Sucursal":
                        resultado = todos.Where(s => s.TipoSucursalNombre.ToLower().Contains(txtBuscar.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                    default:
                        resultado = todos;
                        break;
                }
                

                dgvSucursal.DataSource = resultado.ToList();

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al buscar las sucursales: {ex.Message}");
            }
        }

        private void btnAgregarTipoSucursal_Click(object sender, EventArgs e)
        {
            var frmCrearTipoSucursal = _serviceProvider.GetRequiredService<fmsCrearTipoSucursal>();
            frmCrearTipoSucursal.ShowDialog();

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            var fmsPrincipal = _serviceProvider.GetRequiredService<fmsPrincipal>();
            this.Close();
            fmsPrincipal.Show();
        }
        #endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CargarSucursales();        // Actualiza los datos cada vez que se muestra
        }
        
        private void CargarSucursales()
        {
            try
            {
                var sucursales = _sucursalFacade.GetHabilitados();

                dgvSucursal.DataSource = null;
                dgvSucursal.DataSource = sucursales.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al cargar las sucursales: {ex.Message}",
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void CargarSucursalesDeshabilitadas()
        {
            try
            {
                var deshabilitado = _sucursalFacade.GetDeshabilitados();
                dgvSucursal.DataSource = null;
                dgvSucursal.DataSource = deshabilitado.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las sucursales deshabilitadas: {ex.Message}",
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ConfigurarFiltrosInciales()
        {
            cbxBuscar.Items.Clear();
            cbxBuscar.Items.Add("Nombre");
            cbxBuscar.Items.Add("Tipo de Sucursal");
            cbxBuscar.SelectedIndex = 0;
        }
        
        private void ConfigurarDataGridView()
        {
            dgvSucursal.AutoGenerateColumns = false;
            dgvSucursal.AllowUserToAddRows = false;
            dgvSucursal.ReadOnly = true;
            dgvSucursal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSucursal.MultiSelect = false;
            dgvSucursal.Columns.Clear();

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 200
            });

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                Width = 195
            });

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Localidad",
                HeaderText = "Localidad",
                Width = 150
            });

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TipoSucursalNombre",
                HeaderText = "Tipo de Sucursal",
                Width = 150
            });

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 150
            });

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telefono",
                HeaderText = "Telefono",
                Width = 130
            });

            dgvSucursal.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoPostal",
                HeaderText = "Codigo Postal",
                Width = 69
            });

        }
     
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string criterio = cbxBuscar.Text.ToString();

            if (criterio == "Nombre")
            {
                txtBuscar.PlaceholderText = "Ingrese el nombre de la sucursal...";

            }
            else if (criterio == "Tipo sucursal")
            {
                txtBuscar.PlaceholderText = "Ingrese tipo de sucursal...";
            }

        }
    }
}
