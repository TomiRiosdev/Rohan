using BLL.DomainDtos;
using BLL.GestioónProveedor.Facade;
using Microsoft.Extensions.DependencyInjection;


namespace UI.GestiónProveedor
{
    public partial class fmsGestionProveedor : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProveedorFacade _proveedorFacade;

        private bool _viendoEliminados = false; // Para alternar entre activos y deshabilitados

        public fmsGestionProveedor
        (
            IServiceProvider serviceProvider,
            ProveedorFacade proveedorFacade
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _proveedorFacade = proveedorFacade;

            ConfigurarDataGridView();
            ConfigurarFiltrosIniciales();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CargarProveedor();        // Actualiza los datos cada vez que se muestra
        }
        private void fmsGestionProveedor_Load(object sender, EventArgs e)
        {
            CargarProveedor();
            btnActivar.Enabled = false;
          

        }

        #region Eventos de botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var fmsCrear = _serviceProvider.GetRequiredService<fmsCrearProveedor>();
            fmsCrear.ShowDialog();
            CargarProveedor();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un proveedor de la lista.");
                return;
            }

            // Obtenemos el objeto completo de la fila
            var proveedorSeleccionado = (ProveedorDTO)dgvProveedor.CurrentRow.DataBoundItem;

            // Abrimos el form 
            using (var frmModificar = new fmsModificarProveedor(_proveedorFacade, proveedorSeleccionado))
            {
                if (frmModificar.ShowDialog() == DialogResult.OK)
                {
                    CargarProveedor(); // Refrescamos la grilla
                }
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            // 1. Validar que haya una fila seleccionada
            if (dgvProveedor.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor para deshabilitar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtener el DTO (usamos DataBoundItem porque es una lista de objetos)
            var producto = (ProveedorDTO)dgvProveedor.CurrentRow.DataBoundItem;

            // 3. Confirmación
            var result = MessageBox.Show($"¿Está seguro que desea deshabilitar el Proveedor: {producto.Nombre}?",
                                         "Confirmar Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _proveedorFacade.BajaLogica(producto.Id); // Asumo que se llama así en tu Facade
                    MessageBox.Show("Producto deshabilitado con éxito.");
                    CargarProveedor(); // Refresca la grilla
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow == null) return;

            var proveedor = (ProveedorDTO)dgvProveedor.CurrentRow.DataBoundItem;

            if (MessageBox.Show($"¿Desea habilitar nuevamente al proveedor {proveedor.Nombre}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _proveedorFacade.RehabilitarProveedor(proveedor.Id); //
                    CargarProveedoresDeshabilitados(); // Refrescamos la lista de "muertos"
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnListarDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoEliminados = !_viendoEliminados; // Invierte estados

            if (_viendoEliminados)
            {
                btnListarDeshabilitados.Text = "Ver Activos";
                btnAgregar.Enabled = false;   // Bloqueamos para evitar errores
                btnModificar.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnActivar.Enabled = true;  //  Mostramos el botón de habilitar
                btnActualizar.Enabled = false;   // Evitamos limpiar en esta vista

                CargarProveedoresDeshabilitados();
            }
            else
            {
                btnListarDeshabilitados.Text = "Productos Deshabilitado";
                btnAgregar.Enabled = true;
                btnModificar.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnActivar.Enabled = false;
                btnActualizar.Enabled = true;

                CargarProveedor(); // Vuelve a la carga normal
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarProveedor();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var todos = _proveedorFacade.GetHabilitados();
                string criterio = cbxBuscar.Text;

                if (string.IsNullOrEmpty(criterio))
                {
                    MessageBox.Show("Seleccione un criterio de búsqueda.");
                    return;
                }

                IEnumerable<ProveedorDTO> resultados;

                switch (criterio)
                {

                    case "Nombre":
                        resultados = todos.Where(p => p.Nombre.Contains(txtBuscar.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                        break;
                    case "CUIT":
                        resultados = todos.Where(p => p.Cuit.Contains(txtBuscar.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                        break;
                    default:
                        resultados = todos;
                        break;
                }

                dgvProveedor.DataSource = resultados.ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}");
            }
        }
        private void btnAtras_Click(object sender, EventArgs e)
        {
            var fmsPrincipal = _serviceProvider.GetRequiredService<fmsPrincipal>();
            this.Close();
            fmsPrincipal.Show();
        }

        #endregion

        #region Métodos auxiliares

        private void ConfigurarFiltrosIniciales()
        {
            cbxBuscar.Items.Clear();
            cbxBuscar.Items.Add("Nombre");
            cbxBuscar.Items.Add("CUIT");
            cbxBuscar.SelectedIndex = 0; // Por defecto que marque Nombre
        }

        private void ConfigurarDataGridView()
        {
            dgvProveedor.AutoGenerateColumns = false;
            dgvProveedor.AllowUserToAddRows = false;
            dgvProveedor.ReadOnly = true;
            dgvProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedor.MultiSelect = false;

            // Limpiar columnas previas (por si acaso)
            dgvProveedor.Columns.Clear();

            dgvProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProveedor",
                HeaderText = "Proveedor",
                DataPropertyName = "Nombre",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CUIT",
                HeaderText = "CUIT",
                DataPropertyName = "Cuit",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 230
            });

            dgvProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Telefono",
                DataPropertyName = "Telefono",
                Width = 110
            });

            dgvProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RazonSocial",
                HeaderText = "Razon Social",
                DataPropertyName = "RazonSocial",
                Width = 230,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
            });


        }

        private void CargarProveedor()
        {
            try
            {
                var proveedor = _proveedorFacade.GetHabilitados();

                dgvProveedor.DataSource = null;               // Limpiar antes de asignar
                dgvProveedor.DataSource = proveedor.ToList(); // Asignar la lista de proveedores habilitados


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los proveedores: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProveedoresDeshabilitados()
        {
            try
            {
                var deshabilitados = _proveedorFacade.GetDeshabilitados();
                dgvProveedor.DataSource = null;
                dgvProveedor.DataSource = deshabilitados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar deshabilitados: " + ex.Message);
            }
        }

        #endregion

        private void cbxBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterio = cbxBuscar.Text.ToString();

            if (criterio == "Nombre")
            {
                txtBuscar.PlaceholderText = "Ingrese el nombre del proveedor...";
     
            }
            else if (criterio == "CUIT")
            { 
                txtBuscar.PlaceholderText = "Ingrese el CUIT del proveedor...";
            }
           

        }
    }
}
