using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using Microsoft.Extensions.DependencyInjection;


namespace UI.GestiónProducto
{
    public partial class fmsGestiónProducto : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductoFacade _productoFacade;
        private readonly CategoriaFacade _categoriaFacade;
        private readonly UnidadMedidaFacade _unidadMedidaFacade;

        private bool _viendoEliminados = false;
        public fmsGestiónProducto
        (
            IServiceProvider serviceProvider,
            ProductoFacade productoFacade,
            CategoriaFacade categoriaFacade,
            UnidadMedidaFacade unidadMedidaFacade
        )
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _productoFacade = productoFacade;
            _categoriaFacade = categoriaFacade;
            _unidadMedidaFacade = unidadMedidaFacade;

            ConfigurarDataGridView();
            ConfigurarFiltrosIniciales();
        }
        private void fmsGestiónProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
            btnHabilitar.Enabled = false;
            txtBusquedaLibre.Enabled = false;
            cboFiltroMaestro.Enabled = false;
        }

        #region Eventos del Formulario
        // Se ejecuta cada vez que el formulario se muestra
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CargarProductos();        // Actualiza los datos cada vez que se muestra
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;

            // Limpiar columnas previas (por si acaso)
            dgvProductos.Columns.Clear();

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "Nombre",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                HeaderText = "Codigo SKU",
                DataPropertyName = "CodigoSku",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });


            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CategoriaNombre",
                HeaderText = "Categoría",
                DataPropertyName = "CategoriaNombre",
                Width = 198
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnidadMedidaNombre",
                HeaderText = "Unidad de Medida",
                DataPropertyName = "UnidadMedidaNombre",
                Width = 180
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 300,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContenidoPorVenta",
                HeaderText = "Contenido por Venta",
                DataPropertyName = "ContenidoPorVenta",
                Width = 125,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private void CargarProductos()
        {
            try
            {
                var productos = _productoFacade.ListarProductosActivos();

                dgvProductos.DataSource = null;           // Limpiar antes de asignar
                dgvProductos.DataSource = productos.ToList();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductosDeshabilitados()
        {
            try
            {
                var deshabilitados = _productoFacade.ListarProductosBaja();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = deshabilitados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar deshabilitados: " + ex.Message);
            }
        }

        private void ConfigurarFiltrosIniciales()
        {
            cboBuscarPor.Items.Clear();
            cboBuscarPor.Items.Add("Nombre");
            cboBuscarPor.Items.Add("SKU");
            cboBuscarPor.Items.Add("Categoría");
            cboBuscarPor.Items.Add("Unidad de Medida");
            cboBuscarPor.SelectedIndex = 0; // Por defecto que marque Nombre
        }

        #endregion

        #region Botones

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmCreate = _serviceProvider.GetRequiredService<fmsCrearProducto>();
            frmCreate.ShowDialog();

            CargarProductos();       // Refrescar la lista después de cerrar el formulario de creación
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista.");
                return;
            }

            // Obtenemos el objeto completo de la fila
            var productoSeleccionado = (ProductoDTO)dgvProductos.CurrentRow.DataBoundItem;

            // Abrimos el form pasando el producto
            using (var frmModificar = new fmsModificarProducto(_productoFacade, _categoriaFacade, _unidadMedidaFacade, productoSeleccionado))
            {
                if (frmModificar.ShowDialog() == DialogResult.OK)
                {
                    CargarProductos(); // Refrescamos la grilla
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // 1. Validar que haya una fila seleccionada
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para deshabilitar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtener el DTO (usamos DataBoundItem porque es una lista de objetos)
            var producto = (ProductoDTO)dgvProductos.CurrentRow.DataBoundItem;

            // 3. Confirmación
            var result = MessageBox.Show($"¿Está seguro que desea deshabilitar el producto: {producto.Nombre}?",
                                         "Confirmar Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _productoFacade.BajaLogica(producto.Id); // Asumo que se llama así en tu Facade
                    MessageBox.Show("Producto deshabilitado con éxito.");
                    CargarProductos(); // Refresca la grilla
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAgregarCatUnMed_Click(object sender, EventArgs e)
        {
            var frmCrudCatUMed = _serviceProvider.GetRequiredService<fmsCrudCatUMed>();
            frmCrudCatUMed.ShowDialog();
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fmsPrincipal = _serviceProvider.GetRequiredService<fmsPrincipal>();
            this.Close();
            fmsPrincipal.Show();
        }

        private void btnProductoEliminado_Click(object sender, EventArgs e)
        {
            _viendoEliminados = !_viendoEliminados; // Invertimos el estado

            if (_viendoEliminados)
            {
                btnProductoEliminado.Text = "Ver Activos";
                btnAgregar.Enabled = false;   // Bloqueamos para evitar errores
                btnModificar.Enabled = false;
                btnAgregarCatUnMed.Enabled = false;
                btnEliminar.Enabled = false;  // 
                btnHabilitar.Enabled = true;  // Mostramos el botón de habilitar
                btnLimpiar.Enabled = false;   // Evitamos limpiar en esta vista
                btnBuscar.Enabled = false;    // Evitamos buscar en esta vista

                CargarProductosDeshabilitados();
            }
            else
            {
                btnProductoEliminado.Text = "Productos Deshabilitado";
                btnAgregar.Enabled = true;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnAgregarCatUnMed.Enabled = true;
                btnHabilitar.Enabled = false;
                btnLimpiar.Enabled = true;
                btnBuscar.Enabled = true;

                CargarProductos(); // Vuelve a la carga normal
            }
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;

            var producto = (ProductoDTO)dgvProductos.CurrentRow.DataBoundItem;

            if (MessageBox.Show($"¿Desea habilitar nuevamente el producto {producto.Nombre}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _productoFacade.RehabilitarProducto(producto.Id);
                    CargarProductosDeshabilitados(); // Refrescamos la lista de "muertos"
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var todos = _productoFacade.ListarProductosActivos();
                string criterio = cboBuscarPor.Text;
                IEnumerable<ProductoDTO> resultados;

                switch (criterio)
                {
                    case "Nombre":
                        resultados = todos.Where(p => p.Nombre.ToLower().Contains(txtBusquedaLibre.Text.ToLower()));
                        break;
                    case "SKU":
                        resultados = todos.Where(p => p.CodigoSku.ToString().Contains(txtBusquedaLibre.Text));
                        break;
                    case "Categoría":
                        var idCat = (Guid)cboFiltroMaestro.SelectedValue;
                        resultados = todos.Where(p => p.IdCategoria == idCat);
                        break;
                    case "Unidad de Medida":
                        var idUn = (Guid)cboFiltroMaestro.SelectedValue;
                        resultados = todos.Where(p => p.IdUnidadMedida == idUn);
                        break;
                    default:
                        resultados = todos;
                        break;
                }

                dgvProductos.DataSource = resultados.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void cboBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string criterio = cboBuscarPor.Text;

            // Lógica de visibilidad
            if (criterio == "Categoría" || criterio == "Unidad de Medida")
            {
                txtBusquedaLibre.Enabled = false;
                cboFiltroMaestro.Enabled = true;

                // Cargamos el segundo combo según la elección
                if (criterio == "Categoría")
                {
                    cboFiltroMaestro.DataSource = _categoriaFacade.GetHabilitados();
                    cboFiltroMaestro.DisplayMember = "Descripcion"; // Propiedad del DTO
                    cboFiltroMaestro.ValueMember = "Id";       // Guid del DTO
                }
                else // Es Unidad de Medida
                {
                    cboFiltroMaestro.DataSource = _unidadMedidaFacade.GetHabilitados();
                    cboFiltroMaestro.DisplayMember = "Descripcion";
                    cboFiltroMaestro.ValueMember = "Id";
                }
            }
            else // Es Nombre o SKU
            {
                if (criterio == "Nombre")
                {
                    txtBusquedaLibre.Enabled = true;
                    cboFiltroMaestro.Enabled = false;
                    txtBusquedaLibre.Clear();
                    txtBusquedaLibre.Focus();
                    txtBusquedaLibre.PlaceholderText = "Ingrese el nombre del Producto...";

                }
                else if (criterio == "SKU")
                {
                    txtBusquedaLibre.Enabled = true;
                    cboFiltroMaestro.Enabled = false;
                    txtBusquedaLibre.Clear();
                    txtBusquedaLibre.Focus();
                    txtBusquedaLibre.PlaceholderText = "Ingrese el Codigo SKU...";
                }
                
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusquedaLibre.Clear();
            cboFiltroMaestro.SelectedIndex = -1;
            CargarProductos();

        }

        #endregion

        private void txtBusquedaLibre_TextChanged(object sender, EventArgs e)
        {
            string criterio = cboBuscarPor.Text.ToString();

            if (criterio == "Nombre")
            {
                txtBusquedaLibre.PlaceholderText = "Ingrese el nombre del producto...";

            }
            else if (criterio == "SKU")
            {
                txtBusquedaLibre.PlaceholderText = "Ingrese el codigo SKU...";
            }
        }
    }
}


