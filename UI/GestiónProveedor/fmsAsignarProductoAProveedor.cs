using BLL.DomainDtos;
using BLL.GestiónProducto.Facade;
using BLL.GestiónProveedor.Facade;
using System;
using UI.GestiónStock;


namespace UI.GestiónProveedor
{
    public partial class fmsAsignarProductoAProveedor : Form
    {
        private readonly ProductoProveedorFacade _prodProvFacade;
        private readonly ProductoFacade _productoFacade;
        private readonly ProveedorDTO _proveedorOriginal;
        private ProductoDTO? _productoSeleccionado;

        public fmsAsignarProductoAProveedor
        (
            ProductoProveedorFacade prodProvService,
            ProveedorDTO proveedorSeleccionado,
            ProductoFacade productoFacade
        )
        {
            InitializeComponent();

            _prodProvFacade = prodProvService ?? throw new ArgumentNullException(nameof(prodProvService));
            _proveedorOriginal = proveedorSeleccionado ?? throw new ArgumentNullException(nameof(proveedorSeleccionado));
            _productoFacade = productoFacade ?? throw new ArgumentNullException(nameof(productoFacade));

            dgvProducto.CurrentCellDirtyStateChanged += dgvProducto_CurrentCellDirtyStateChanged;
            dgvProducto.CellValueChanged += dgvProducto_CellValueChanged;
        }

        #region Inicialización de Pantalla
        private void fmsAsignarProductoAProveedor_Load(object sender, EventArgs e)
        {
            txtProvNombre.Text = _proveedorOriginal.Nombre;
            txtProvRazonSocial.Text = _proveedorOriginal.RazonSocial;
            txtProvNombre.ReadOnly = true;
            txtProvRazonSocial.ReadOnly = true;
            txtProdNombre.ReadOnly = true;
            txtProdSku.ReadOnly = true;

            ConfigurarColumna();
            CargarDatosGrilla();
        }
        private void CargarDatosGrilla()
        {
            try
            {
              
                var listaAsignaciones = _prodProvFacade.ListarProductosPorProveedor(_proveedorOriginal.Id);

                dgvProducto.DataSource = null;
                dgvProducto.DataSource = listaAsignaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos asignados: {ex.Message}", "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumna()
        {
            dgvProducto.Columns.Clear();
            dgvProducto.AllowUserToAddRows = false;
            dgvProducto.ReadOnly = false;
            dgvProducto.AutoGenerateColumns = false;
            dgvProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducto.RowHeadersVisible = false;
            dgvProducto.BackgroundColor = Color.White;
            dgvProducto.BorderStyle = BorderStyle.None;
            dgvProducto.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvProducto.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Columna 1: Código SKU
            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku",
                HeaderText = "Código SKU",
                Width = 100,
                ReadOnly = true, // <-- Bloqueamos la edición del texto
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Columna 2: Nombre del Producto
            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                ReadOnly = true, // <-- Bloqueamos la edición del texto
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            //  Columna 3: CheckBox de Proveedor Principal
            dgvProducto.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "EsProveedorPrincipal",
                DataPropertyName = "EsProveedorPrincipal",
                HeaderText = "Prov. Principal",
                Width = 100,
                ReadOnly = false, // <-- LA ÚNICA COLUMNA EDITABLE
                FlatStyle = FlatStyle.Standard // Estilo visual limpio
            });
        }

        #endregion



        #region Eventos de Botones
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var popUp = new fmsListarProductosPopUp(_productoFacade))
            {
                popUp.StartPosition = FormStartPosition.CenterParent;

                if (popUp.ShowDialog() == DialogResult.OK)
                {
                    _productoSeleccionado = popUp.ProductoSeleccionado;

                    // Mostramos la selección en los TextBox bloqueados
                    txtProdNombre.Text = _productoSeleccionado.Nombre;
                    txtProdSku.Text = _productoSeleccionado.CodigoSku.ToString();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionado == null)
            {
                MessageBox.Show("Por favor, busque y seleccione un producto primero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Armamos el DTO de la tabla intermedia
                var nuevaAsignacion = new ProductoProveedorDTO
                {
                    IdProductoProveedor = Guid.NewGuid(),
                    IdProveedor = _proveedorOriginal.Id,
                    IdProducto = _productoSeleccionado.Id,
                    EsProveedorPrincipal = false,
                };

                // Enviamos a la BLL
                _prodProvFacade.VincularProductoAProveedor(nuevaAsignacion);

                // Limpiamos los controles para una nueva carga
                _productoSeleccionado = null;
                txtProdNombre.Clear();
                txtProdSku.Clear();

                // Refrescamos la grilla reactivamente
                CargarDatosGrilla();

                MessageBox.Show("Producto asignado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Asignar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProducto.CurrentRow == null || dgvProducto.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Seleccione una asignación de la grilla para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Casteamos la fila seleccionada a tu DTO intermedio
            var asignacionSeleccionada = (ProductoProveedorDTO)dgvProducto.CurrentRow.DataBoundItem;

            var confirmacion = MessageBox.Show(
                $"¿Está seguro que desea desvincular el producto de este proveedor?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _prodProvFacade.DesvincularProductoDeProveedor(
                        asignacionSeleccionada.IdProducto,
                        _proveedorOriginal.Id
                    );

                    // Refrescamos la grilla reactivamente
                    CargarDatosGrilla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al Desvincular", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        private void dgvProducto_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProducto.IsCurrentCellDirty && dgvProducto.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvProducto.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvProducto_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que sea una fila válida y que la columna modificada sea "EsProveedorPrincipal"
            if (e.RowIndex >= 0 && dgvProducto.Columns[e.ColumnIndex].Name == "EsProveedorPrincipal")
            {
                // Obtenemos la fila afectada casteada a tu DTO
                var asignacionModificada = (ProductoProveedorDTO)dgvProducto.Rows[e.RowIndex].DataBoundItem;

                try
                {
                    // TODO: Crear en tu IProductoProveedorService un método para actualizar este flag
                    // _prodProvFacade.ActualizarProveedorPrincipal(asignacionModificada.IdProductoProveedor, asignacionModificada.EsProveedorPrincipal);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar la prioridad del proveedor: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
