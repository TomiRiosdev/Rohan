using BLL.DomainDtos;
using BLL.GestiónProveedor.Facade;
using System;
using System.Text;

namespace UI.GestionCompra
{
    public partial class fmsCatalogoCostoProducto : Form
    {
        private readonly ProveedorFacade _proveedorFacade;
        private readonly ProductoProveedorFacade _prodProvService;

        private ProductoProveedorDTO _productoElegidoActual;

        public fmsCatalogoCostoProducto
        (
            ProveedorFacade proveedorFacade,
            ProductoProveedorFacade prodProvService
        )
        {
            InitializeComponent();
            _proveedorFacade = proveedorFacade ?? throw new ArgumentNullException(nameof(proveedorFacade));
            _prodProvService = prodProvService ?? throw new ArgumentNullException(nameof(prodProvService));
        }

        private void fmsCatalogoCostoProducto_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Configuraciones de solo lectura de la UI (Blindaje)
                txtProvRazonSocial.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtTelefono.ReadOnly = true;

                nupPrecio.Enabled = false;

                // 2. Apagamos eventos temporalmente
                cxbNombreProveedor.SelectedIndexChanged -= cxbNombreProveedor_SelectedIndexChanged!;
                dgvProducto.SelectionChanged -= dgvProducto_SelectionChanged!;


                // 3. Cargamos todos los proveedores
                var proveedores = _proveedorFacade.GetHabilitados();

                // 4. Configuramos el ComboBox
                cxbNombreProveedor.DataSource = proveedores;
                cxbNombreProveedor.DisplayMember = "Nombre";
                cxbNombreProveedor.ValueMember = "Id";
                cxbNombreProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cxbNombreProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;

                cxbNombreProveedor.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 5. Encendemos eventos
                cxbNombreProveedor.SelectedIndexChanged += cxbNombreProveedor_SelectedIndexChanged!;
                dgvProducto.SelectionChanged += dgvProducto_SelectionChanged!;
            }
        }

        private void cxbNombreProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si no hay nada seleccionado (ej. cuando limpiamos el form), salimos
            if (cxbNombreProveedor.SelectedIndex < 0) return;

            try
            {
                // objeto completo directamente de la selección
                if (cxbNombreProveedor.SelectedItem is ProveedorDTO proveedorSeleccionado)
                {
                    // datos en los TextBox (Solo lectura)
                    txtProvRazonSocial.Text = proveedorSeleccionado.RazonSocial;
                    txtEmail.Text = proveedorSeleccionado.Email;
                    txtTelefono.Text = proveedorSeleccionado.Telefono;


                    nupPrecio.Value = 0;
                    nupPrecio.Enabled = false;
                    _productoElegidoActual = null;

                    CargarProductosDelProveedor(proveedorSeleccionado.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el contexto del proveedor: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarProductosDelProveedor(Guid idProveedor)
        {
            try
            {
                // Apagamos el evento de la grilla para que no salte al asignar el DataSource
                dgvProducto.SelectionChanged -= dgvProducto_SelectionChanged!;

                dgvProducto.AutoGenerateColumns = false;
                List<ProductoProveedorDTO> productos = _prodProvService.ListarProductosPorProveedor(idProveedor).ToList();

                dgvProducto.DataSource = null;
                dgvProducto.DataSource = productos;

                ConfigurarColumnasGrillaDerecha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos del proveedor: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reactivamos el evento y forzamos a que no haya nada seleccionado al inicio
                dgvProducto.ClearSelection();
                dgvProducto.SelectionChanged += dgvProducto_SelectionChanged!;
            }
        }
        private void ConfigurarColumnasGrillaDerecha()
        {
            dgvProducto.Columns.Clear();

            // Configuración estética general
            dgvProducto.AllowUserToAddRows = false;
            dgvProducto.ReadOnly = true;
            dgvProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducto.RowHeadersVisible = false;
            dgvProducto.BackgroundColor = Color.White;
            dgvProducto.BorderStyle = BorderStyle.None;
            dgvProducto.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvProducto.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku",
                HeaderText = "Código SKU",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvProducto.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario", // Ajustá este nombre si en tu DTO se llama distinto
                DataPropertyName = "PrecioUnitario",
                HeaderText = "Precio Actual",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void dgvProducto_SelectionChanged(object sender, EventArgs e)
        {
            // Validamos que haya una fila válida seleccionada
            if (dgvProducto.CurrentRow == null || dgvProducto.CurrentRow.Index < 0)
            {
                nupPrecio.Enabled = false;
                _productoElegidoActual = null;
                return;
            }

            try
            {
                // Casteamos al DTO que alimenta la grilla
                if (dgvProducto.CurrentRow.DataBoundItem is ProductoProveedorDTO prodSeleccionado)
                {
                    _productoElegidoActual = prodSeleccionado;
                    nupPrecio.Enabled = true;
                    nupPrecio.Value = prodSeleccionado.PrecioUnitario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al interpretar las propiedades del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvProducto.Rows.Count == 0) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo CSV (*.csv)|*.csv";
                sfd.FileName = $"Catalogo_{txtProvRazonSocial.Text.Replace(" ", "_")}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        // Cabeceras
                        sb.AppendLine("Codigo SKU;Producto;Precio Actual");

                        // Datos
                        foreach (DataGridViewRow row in dgvProducto.Rows)
                        {
                            var p = (ProductoProveedorDTO)row.DataBoundItem;
                            sb.AppendLine($"{p.CodigoSku};{p.ProductoNombre.Replace(";", ",")};{p.PrecioUnitario:F2}");
                        }

                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Archivo exportado con éxito.", "Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al exportar: {ex.Message}");
                    }
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_productoElegidoActual == null) return;

            try
            {
                _prodProvService.ActualizarPrecioUnitario(_productoElegidoActual.IdProducto, _productoElegidoActual.IdProveedor, nupPrecio.Value);
                MessageBox.Show("Precio actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Refrescamos la grilla para reflejar el cambio (usamos el id del proveedor actual)
                if (cxbNombreProveedor.SelectedItem is ProveedorDTO prov)
                {
                    CargarProductosDelProveedor(prov.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
