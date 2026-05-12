using Microsoft.Extensions.DependencyInjection;
using Service.Logic;
using System.Data;


namespace UI.GestionUsuario
{
    public partial class fmsAgregarPermisos : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UsuarioService _usuarioService;
        private readonly PermisosService _permisoService;
        public fmsAgregarPermisos
        (
            IServiceProvider serviceProvider,
            UsuarioService usuarioService,
            PermisosService permisoService
        )
        {
            
            _serviceProvider = serviceProvider;
            _usuarioService= usuarioService;
            _permisoService = permisoService;
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            var fmsGestionUsuario = _serviceProvider.GetRequiredService<fmsGestionUsuario>();
            this.Close();
            fmsGestionUsuario.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validamos selección en combos
                if (cbxUsuario.SelectedValue == null || cbxPermiso.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un usuario y un permiso.");
                    return;
                }

                Guid idUsuario = (Guid)cbxUsuario.SelectedValue;
                Guid idFamilia = (Guid)cbxPermiso.SelectedValue;

                // Ejecutamos la lógica a través del Service
                _permisoService.AsignarFamilia(idUsuario, idFamilia);

                MessageBox.Show("Permiso asignado con éxito.");
                ActualizarGrillaPermisos(); 
                LimpiarCombos(); 
            }
            catch (Exception ex)
            {
                // Aquí atraparás el "El usuario ya posee este permiso" que lanzamos en el Service
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarioFamilia.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una fila de la grilla para eliminar.");
                return;
            }

            try
            {
                // Rescatamos los IDs de las columnas ocultas
                Guid idUsuario = (Guid)dgvUsuarioFamilia.CurrentRow.Cells["IdUsuario"].Value;
                Guid idFamilia = (Guid)dgvUsuarioFamilia.CurrentRow.Cells["IdFamilia"].Value;
                string nombreUser = dgvUsuarioFamilia.CurrentRow.Cells["Usuario"].Value.ToString();
                string nombrePermiso = dgvUsuarioFamilia.CurrentRow.Cells["Permiso"].Value.ToString();

                var result = MessageBox.Show($"¿Desea quitar el permiso '{nombrePermiso}' al usuario '{nombreUser}'?",
                                            "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _permisoService.QuitarFamilia(idUsuario, idFamilia);
                    MessageBox.Show("Permiso revocado.");
                    ActualizarGrillaPermisos(); // Refrescamos
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }

        }

        private void fmsAgregarPermisos_Load(object sender, EventArgs e)
        { 
            ActualizarGrillaPermisos();

            try
            {
                // 1. Cargar Usuarios Habilitados
                cbxUsuario.DataSource = _usuarioService.ListarHabilitados();
                cbxUsuario.DisplayMember = "Nombre";
                cbxUsuario.ValueMember = "IdUsuario";

                // 2. Cargar Familias (Permisos)
                var familias = _permisoService.GetAllFamilias();
                cbxPermiso.DataSource = familias;
                cbxPermiso.ValueMember = "Id";
                cbxPermiso.DisplayMember = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void LimpiarCombos()
        {
            cbxUsuario.SelectedIndex = -1;
            cbxPermiso.SelectedIndex = -1;
        }   

        private void ActualizarGrillaPermisos()
        {
            try
            {
                // Llamamos al servicio (que a su vez llama al nuevo ExecuteDataTable del Repo)
                DataTable dt = _permisoService.ObtenerListaPermisosUsuarios();
                dgvUsuarioFamilia.DataSource = dt;

                // Ocultamos los IDs para que el usuario solo vea nombres
                if (dgvUsuarioFamilia.Columns.Contains("IdUsuario"))
                    dgvUsuarioFamilia.Columns["IdUsuario"].Visible = false;

                if (dgvUsuarioFamilia.Columns.Contains("IdFamilia"))
                    dgvUsuarioFamilia.Columns["IdFamilia"].Visible = false;

                // Estética: Ajustamos las columnas de nombres
                dgvUsuarioFamilia.Columns["Usuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvUsuarioFamilia.Columns["Permiso"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar grilla: " + ex.Message);
            }
        }
    }
}
