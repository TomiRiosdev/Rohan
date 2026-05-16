using BLL.GestiónProducto.Facade;
using BLL.GestiónSucursal.Facade;
using Microsoft.Extensions.DependencyInjection;
using Service.DomainModel.Composite;
using Service.Logic;


namespace UI.GestionUsuario
{
    public partial class fmsGestionUsuario : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UsuarioService _usuarioService;
        private readonly SucursalFacade _sucursalFacade;

        private bool _mostrandoDeshabilitados = false;

        public fmsGestionUsuario
        (
            IServiceProvider serviceProvider,
            UsuarioService usuarioService,
            SucursalFacade sucursalFacade
        )
        {
            InitializeComponent();
            _sucursalFacade = sucursalFacade;
            _serviceProvider = serviceProvider;
            _usuarioService = usuarioService;

            ConfigurarDataGridView();

        }
        private void fmsGestionUsuario_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarCombos(); 
            btnActivar.Enabled = false;
        }
       
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CargarUsuarios();        // Actualiza los datos cada vez que se muestra
        }

        private void ConfigurarDataGridView()
        {
            dgvUsuario.AutoGenerateColumns = false;
            dgvUsuario.AllowUserToAddRows = false;
            dgvUsuario.ReadOnly = true;
            dgvUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuario.MultiSelect = false;

            dgvUsuario.Columns.Clear();

            dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "Nombre de Usuario",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                DataPropertyName = "Nombre",
                HeaderText = "Nombre Completo",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Correo Electrónico",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Width = 109,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

        }

        #region BOTONES 
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var fmsAgregarUsuario = _serviceProvider.GetRequiredService<fmsAgregarUsuario>();
          
            fmsAgregarUsuario.ShowDialog();

        }

        private void btnGestionPermiso_Click(object sender, EventArgs e)
        {
            var fmsAgregarPermisos = _serviceProvider.GetRequiredService<fmsAgregarPermisos>();
            fmsAgregarPermisos.Show();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            var fmsPrincipal = _serviceProvider.GetRequiredService<fmsPrincipal>();
            this.Close();
            fmsPrincipal.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuarioSeleccionado = (Usuario)dgvUsuario.CurrentRow.DataBoundItem;

            using (var fmsModificar = new fmsModificarUsuario(_usuarioService, _sucursalFacade, usuarioSeleccionado))
            {
                if (fmsModificar.ShowDialog() == DialogResult.OK)
                {
                    CargarUsuarios();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxSucursal.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, seleccione una sucursal para filtrar.", "Atención",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Guid idSucursalSeleccionada = (Guid)cbxSucursal.SelectedValue;

                var todosLosUsuarios = _usuarioService.ListarHabilitados();

                var usuariosFiltrados = todosLosUsuarios
                    .Where(u => u.IdSucursal == idSucursalSeleccionada)
                    .ToList();

                dgvUsuario.DataSource = null;
                dgvUsuario.DataSource = usuariosFiltrados;

                if (usuariosFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron usuarios en la sucursal seleccionada.", "Búsqueda",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al filtrar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnListarDeshabilitados_Click(object sender, EventArgs e)
        {
            _mostrandoDeshabilitados = !_mostrandoDeshabilitados;
            if (_mostrandoDeshabilitados)
            {
                btnListarDeshabilitados.Text = "Listar Habilitados";
                btnModificar.Enabled = false;
                btnActualizar.Enabled = false;
                btnAgregar.Enabled = false;
                btnDeshabilitar.Enabled = false;
                btnGestionPermiso.Enabled = false;
                btnActivar.Enabled = true;

                CargarUsuariosDeshabilitados();
            }
            else
            {
                btnListarDeshabilitados.Text = "Listar Deshabilitados";
                btnModificar.Enabled = true;
                btnActivar.Enabled = false;
                btnActualizar.Enabled = true;
                btnAgregar.Enabled = true;
                btnDeshabilitar.Enabled = true;
                btnGestionPermiso.Enabled = true;
                CargarUsuarios();
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuario.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"¿Desea habilitar al usuario '{usuario.Username}'?", "Confirmar Habilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.HabilitarUsuario(usuario.IdUsuario);
                    MessageBox.Show("Usuario habilitado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuariosDeshabilitados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al habilitar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvUsuario.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para deshabilitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuario = (Usuario)dgvUsuario.CurrentRow.DataBoundItem;

            var result = MessageBox.Show($"¿Está seguro que desea deshabilitar al usuario '{usuario.Username}'?",
                                            "Confirmar Deshabilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.DeshabilitarUsuario(usuario.IdUsuario);
                    MessageBox.Show("Usuario deshabilitado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al deshabilitar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }


        #endregion

        #region METODOS 
        private void CargarUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.ListarHabilitados();

                dgvUsuario.DataSource = null;
                dgvUsuario.DataSource = usuarios.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los usuarios: {ex.Message}",
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuariosDeshabilitados()
        {
            try
            {
                var deshabilitados = _usuarioService.ListarDeshabilitados();
                dgvUsuario.DataSource = null;
                dgvUsuario.DataSource = deshabilitados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar deshabilitados: " + ex.Message);
            }
        }

       
        #endregion

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

         
    }
}

