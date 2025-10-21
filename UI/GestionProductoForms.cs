
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows.Forms;
using BLL.Facade;
using ModelsDTO;

namespace UI
{
    public partial class GestionProductoForms : Form
    {
        private readonly ProductoFacade _productoFacade = new ProductoFacade();
        private readonly CategoriaFacade _categoriaFacade = new CategoriaFacade();
        private readonly ProveedorFacade _proveedorFacade = new ProveedorFacade();
        private readonly UnidadMedidaFacade _unidadMedidaFacade = new UnidadMedidaFacade();
        private readonly TipoProductoFacade _tipoProductoFacade = new TipoProductoFacade();
        public GestionProductoForms()
        {
            InitializeComponent();
            CargarDatosCombos();
        }
        //
        private void CargarDatosCombos()
        {
            try
            {
                var proveedor = _proveedorFacade.GetAllProveedores().ToList();
                var categorias = _categoriaFacade.GetAllCategorias().ToList();
                var unidadMedida = _unidadMedidaFacade.GetAll().ToList();
                var tipoProducto = _tipoProductoFacade.GetAll().ToList();

                // Validar que se hayan cargado datos antes de asignar el DataSource
                if (categorias.Count > 0)
                {
                    cbxCategoria.DataSource = categorias;
                    cbxCategoria.DisplayMember = "Nombre";
                    // comboBox1.ValueMember = "IdCategoriaProducto"; 
                    cbxCategoria.SelectedIndex = -1;
                }
                if (proveedor.Count > 0)
                {
                    cbxProveedor.DataSource = proveedor;
                    cbxProveedor.DisplayMember = "Nombre";
                    // comboBox2.ValueMember = "IdProveedor"; 
                    cbxProveedor.SelectedIndex = -1;
                }
                if (unidadMedida.Count > 0)
                {
                    cbxUnidadMedida.DataSource = unidadMedida;
                    cbxUnidadMedida.DisplayMember = "Nombre";
                    // comboBox3.ValueMember = "IdUnidadMedida"; 
                    cbxUnidadMedida.SelectedIndex = -1;
                }
                if (tipoProducto.Count > 0)
                {
                    cbxTipoProducto.DataSource = tipoProducto;
                    cbxTipoProducto.DisplayMember = "Nombre";
                    // comboBox4.ValueMember = "IdTipoProducto"; 
                    cbxTipoProducto.SelectedIndex = -1;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las categorías: {ex.Message}", "Error de Conexión o Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProducto();
        }
        private void BuscarProducto()
        {
            try
            {
                // Obtiene los prodcutos habilitados desde la capa de negocio
                var productos = _productoFacade.GetAllProductosHabilitados().ToList();

                // 2. Asignar la fuente de datos al DataGridView
                dgwListaProducto.DataSource = productos;

                // Opcional: Ocultar columnas que no son relevantes para la vista
                dgwListaProducto.Columns["IdProducto"].Visible = false;
                // Ocultar las FKs que solo tienen GUIDs (si es necesario)
                dgwListaProducto.Columns["IdCategoria"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. RECOLECCIÓN Y CONVERSIÓN DE DATOS DE UI
                // Usamos el ValueMember del ComboBox para obtener el GUID
                Guid idCategoria = (Guid)cbxCategoria.SelectedValue;
                Guid idUnidadMedida = (Guid)cbxUnidadMedida.SelectedValue;
                Guid idProveedor = (Guid)cbxProveedor.SelectedValue;
                Guid idTipoProducto = (Guid)cbxTipoProducto.SelectedValue;

                // Creamos el DTO de entrada (para ser mapeado a la Entidad en la BLL)
                var nuevoProductoData = new ProductoDTO // Asumimos un DTO de creación
                {
                    Nombre = txbNombre.Text.Trim(),
                    IdCategoria = idCategoria,
                    IdUnidadMedida = idUnidadMedida,
                    IdTipoProducto = idTipoProducto,
                    IdProveedor = idProveedor
                };


                // Llama al método que orquesta las dos inserciones (Producto y ProductoProveedor)
                Guid nuevoId = _productoFacade.AddProducto(nuevoProductoData);

                MessageBox.Show($"Producto '{nuevoProductoData.Nombre}' creado con éxito. ID: {nuevoId}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar formulario y recargar Grid/Lista
                LimpiarControles();
                BuscarProducto();

            }
            catch (Exception ex)
            {
                // Capturar errores de negocio (Ej: ProductoServiceException, ArgumentException)
                MessageBox.Show($"Error al agregar el producto: {ex.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. VALIDACIÓN Y RECOLECCIÓN DEL ID DEL PRODUCTO
                if (dgvProductos.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un producto para modificar.", "Advertencia");
                    return;
                }

                // Obtener el ID del producto (asumiendo que el DataGridView tiene el DTO enlazado)
                Guid idProducto = (Guid)dgvProductos.CurrentRow.Cells["IdProducto"].Value;

                // 2. CREACIÓN DEL DTO ACTUALIZADO
                var productoActualizado = new ProductoDTO
                {
                    IdProducto = idProducto, // Clave de la actualización
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    // Obtener todos los IDs de los ComboBox de la misma forma...
                    IdCategoria = (Guid)cmbCategoria.SelectedValue,
                    // ...
                };

                // 3. DELEGACIÓN A LA FACHADA
                ProductoFacade.Instance.UpdateProducto(productoActualizado);

                MessageBox.Show("Producto modificado con éxito.", "Éxito");
                RecargarLista();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el producto: {ex.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. VALIDACIÓN Y RECOLECCIÓN DEL ID
                if (dgvProductos.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar un producto para eliminar.", "Advertencia");
                    return;
                }

                if (MessageBox.Show("¿Está seguro de DESHABILITAR este producto?", "Confirmar Soft Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                // Obtener el ID del producto
                Guid idProducto = (Guid)dgvProductos.CurrentRow.Cells["IdProducto"].Value;

                // 2. DELEGACIÓN A LA FACHADA
                // Llama al método que cambia Habilitado = false en el repositorio.
                ProductoFacade.Instance.DeleteProducto(idProducto);

                MessageBox.Show("Producto deshabilitado con éxito.", "Éxito");
                RecargarLista();

            }
          catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgwListaProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                // 1. Obtener el DTO completo de la fila seleccionada
                // Esto asume que el DataSource del DGV es una lista de ProductoDTOs.
                var productoSeleccionado = (ProductoDTO)dgvProductos.Rows[e.RowIndex].DataBoundItem;

                // 2. Cargar los TextBoxes
                txtNumeroProducto.Text = productoSeleccionado.IdProducto.ToString(); // Mostrar el ID (solo lectura)
                txtNombre.Text = productoSeleccionado.Nombre;
                txtDescripcion.Text = productoSeleccionado.Descripcion;
                // Asignar el resto de los TextBoxes (Precio, etc.)

                // 3. Cargar los ComboBox (Usando el ValueMember)
                // Se utiliza la clave foránea (GUID) para seleccionar el ítem correcto.
                cmbCategoria.SelectedValue = productoSeleccionado.IdCategoria;
                cmbUnidadMedida.SelectedValue = productoSeleccionado.IdUnidadMedida;
                cmbProveedor.SelectedValue = productoSeleccionado.IdProveedor;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del producto seleccionado: " + ex.Message, "Error de Datos");
            }
        }

        private void LimpiarControles()
        {
            txbNombre.Clear();
            cbxCategoria.SelectedIndex = -1;
            cbxProveedor.SelectedIndex = -1;
            cbxTipoProducto.SelectedIndex = -1;
            cbxUnidadMedida.SelectedIndex = -1;
        }
    }
}
