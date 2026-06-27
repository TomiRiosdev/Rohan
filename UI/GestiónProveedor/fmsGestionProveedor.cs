using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using BLL.GestiónProveedor.Facade;
using Microsoft.Extensions.DependencyInjection;
using Models;


namespace UI.GestiónProveedor
{
    public partial class fmsGestionProveedor : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProveedorFacade _proveedorFacade;
        private readonly ProductoProveedorFacade _prodProvService;
        private readonly ProductoFacade _productoFacade;

        private bool _viendoEliminados = false; // Para alternar entre activos y deshabilitados

        public fmsGestionProveedor
        (
            IServiceProvider serviceProvider,
            ProveedorFacade proveedorFacade,
            ProductoProveedorFacade prodProvService,
            ProductoFacade productoFacade  
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _proveedorFacade = proveedorFacade;
            _prodProvService = prodProvService;
            _productoFacade = productoFacade;

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
            fmsCrear.StartPosition = FormStartPosition.CenterParent;
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
                frmModificar.StartPosition = FormStartPosition.CenterParent;

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

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Rescatamos el proveedor que está pintado en la pantalla
            if (dgvProveedor.CurrentRow != null && dgvProveedor.CurrentRow.DataBoundItem is ProveedorDTO provSeleccionado)
            {
                // Levantamos tu Pop-up pasando el servicio de asignación y el objeto seleccionado
                using (var frmAsignar = new fmsAsignarProductoAProveedor(_prodProvService, provSeleccionado, _productoFacade))
                {
                    frmAsignar.StartPosition = FormStartPosition.CenterParent;

                    // Si el usuario guardó con éxito en la ventana flotante, refrescamos al instante la grilla derecha
                    if (frmAsignar.ShowDialog() == DialogResult.OK)
                    {
                        CargarProductosDelProveedor(provSeleccionado.Id);
                       
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor de la lista izquierda antes de intentar asociar un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            dgvProveedor.RowHeadersVisible = false;
            dgvProveedor.BackgroundColor = Color.White;
            dgvProveedor.BorderStyle = BorderStyle.None;
            Color azulPastelRohan = Color.FromArgb(185, 210, 245);

            dgvProveedor.DefaultCellStyle.SelectionBackColor = azulPastelRohan;
            dgvProveedor.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30); // Texto oscuro para que contraste

            // Opcional: También modificamos el color cuando la grilla pierde el foco
            dgvProveedor.ColumnHeadersDefaultCellStyle.SelectionBackColor = azulPastelRohan;

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
                Width = 200
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
                Width = 210,
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

        #region Eventos de selección de grilla Proveedor a Producto
        private void CargarProductosDelProveedor(Guid idProveedor)
        {
            try
            {
                // 1. Apagamos la autogeneración para tomar el control total del diseño
                dgvProductoProveedor.AutoGenerateColumns = false;

                // 2. Llamamos a la BLL pasando el ID correcto (proveedorSeleccionado.Id)
                List<ProductoProveedorDTO> productos = _prodProvService.ListarProductosPorProveedor(idProveedor).ToList();

                // 3. Asignamos la lista
                dgvProductoProveedor.DataSource = null;
                dgvProductoProveedor.DataSource = productos;

                // 4. Armamos la estructura visual exacta
                ConfigurarColumnasGrillaDerecha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos del proveedor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasGrillaDerecha()
        {
            // Limpiamos las columnas para que no se dupliquen al cambiar de proveedor
            dgvProductoProveedor.Columns.Clear();

            // Configuración estética general
            dgvProductoProveedor.AllowUserToAddRows = false;
            dgvProductoProveedor.ReadOnly = true;
            dgvProductoProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductoProveedor.RowHeadersVisible = false;
            dgvProductoProveedor.BackgroundColor = Color.White;
            dgvProductoProveedor.BorderStyle = BorderStyle.None;
            dgvProductoProveedor.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvProductoProveedor.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvProductoProveedor.CurrentCell = null;

            // Columna 1: Código SKU (Alineada con tu DTO: 'CodigoSku')
            dgvProductoProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku", // Mapea directo al DTO
                HeaderText = "Código SKU",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Columna 2: Nombre del Producto (Alineada con tu DTO: 'ProductoNombre')
            dgvProductoProveedor.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre", // Mapea directo al DTO
                HeaderText = "Producto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Estira el control para ocupar el panel
            });
        }

       
        private void dgvProveedor_SelectionChanged_1(object sender, EventArgs e)
        {

            // 1. Verificamos que haya una fila seleccionada y que no sea una fila vacía de cabecera
            if (dgvProveedor.CurrentRow != null && dgvProveedor.CurrentRow.DataBoundItem != null)
            {
                // 2. Casteamos el objeto de la fila al DTO original de tu grilla
                var proveedorSeleccionado = (ProveedorDTO)dgvProveedor.CurrentRow.DataBoundItem;

                // 3. Cargamos los productos asociados usando su ID
                CargarProductosDelProveedor(proveedorSeleccionado.Id);
            }
            else
            {
                // Si por algún motivo no hay selección, limpiamos la grilla derecha
                dgvProductoProveedor.DataSource = null;
            }
        }
       
        #endregion
    }
}
