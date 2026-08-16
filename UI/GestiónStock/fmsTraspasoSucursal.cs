using BLL.DomainDtos;
using BLL.GestiónCompra.Exceptions;
using BLL.GestiónCompra.Facade;
using BLL.GestiónStock.Facade;
using Service.Facade;
using System;
using System.ComponentModel;

namespace UI.GestiónStock
{
    public partial class fmsTraspasoSucursal : Form
    {
        private readonly StockFacade _stockFacade;
        private readonly SolicitudPedidoFacade _solicitudPedidoFacade;
        private List<OrdenTraspasoDTO> _traspasosPendientes;
        public fmsTraspasoSucursal
        (
            StockFacade stockFacade,
            SolicitudPedidoFacade solicitudPedidoFacade
        )
        {
            _stockFacade = stockFacade ?? throw new ArgumentNullException(nameof(stockFacade));
            _solicitudPedidoFacade = solicitudPedidoFacade ?? throw new ArgumentNullException(nameof(solicitudPedidoFacade));

            InitializeComponent();

            // Suscripción a eventos clave
            this.Load += FmsTraspasoSucursal_Load;
            this.dgvSolicitud.SelectionChanged += DgvSolicitud_SelectionChanged;
            this.dgvDetalle.CellValidating += DgvDetalle_CellValidating;
        }

        private void FmsTraspasoSucursal_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaMaestro();
            ConfigurarGrillaDetalle();
            ActualizarPantallaCompleta();
        }

        #region Refresco Reactivo

        private void ActualizarPantallaCompleta()
        {
            try
            {
                Guid idSucursalDeposito = SessionManager.Current.IdSucursalActual
                    ?? throw new Exception("No se detectó la sucursal activa.");

                // Obtenemos los traspasos en estado "Preparacion"
                _traspasosPendientes = _stockFacade.ObtenerTraspasosEnPreparacion(idSucursalDeposito).ToList();

                dgvSolicitud.DataSource = null;
                dgvSolicitud.DataSource = _traspasosPendientes;

                if (!_traspasosPendientes.Any())
                {
                    dgvDetalle.DataSource = null;
                    btnConfirmar.Enabled = false;
                    btnRechazar.Enabled = false;
                }
                else
                {
                    btnConfirmar.Enabled = true;
                    btnRechazar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los traspasos pendientes: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos Maestro-Detalle

        private void DgvSolicitud_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSolicitud.CurrentRow == null) return;

            try
            {
                var traspasoSeleccionado = (OrdenTraspasoDTO)dgvSolicitud.CurrentRow.DataBoundItem;

                if (traspasoSeleccionado != null)
                {
                    // Utilizamos BindingList para permitir la edición en la grilla y reflejarlo en el objeto
                    dgvDetalle.DataSource = null;
                    dgvDetalle.DataSource = new BindingList<OrdenTraspasoDetalleDTO>(traspasoSeleccionado.Detalles);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // VALIDACIÓN: Evita que el usuario ingrese letras, negativos o cantidades irreales
        private void DgvDetalle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Verificamos si la celda que se está editando es la columna de "Cantidad a Enviar"
            if (dgvDetalle.Columns[e.ColumnIndex].Name == "CantidadEnviadaBultos")
            {
                string valorIngresado = e.FormattedValue.ToString();

                if (!int.TryParse(valorIngresado, out int cantidadNueva) || cantidadNueva < 0)
                {
                    MessageBox.Show("Debe ingresar un número entero válido mayor o igual a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Cancela la edición y devuelve el valor anterior
                }
            }
        }

        #endregion

        #region Acciones (Botones)

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitud.CurrentRow == null) return;

            var traspaso = (OrdenTraspasoDTO)dgvSolicitud.CurrentRow.DataBoundItem;

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de confirmar el envío para el Traspaso N° {traspaso.NroTraspaso} hacia {traspaso.SucursalDestinoNombre}?\n\n" +
                "Esto descontará el stock físico del depósito.",
                "Confirmación de Envío", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Forzamos el fin de edición de la grilla para asegurar que el último número ingresado se guarde
                    dgvDetalle.EndEdit();

                    string usuarioActivo = SessionManager.Current.UsuarioLogueado.Nombre;

                    // NOTA LOGÍSTICA: Aquí puedes aplicar la conversión si tu BLL espera UNIDADES.
                    // Si tu DTO asume que CantidadEnviada son "Unidades", deberías iterar la lista 
                    // y hacer: det.CantidadEnviada = det.CantidadEnviadaBultos * det.UnidadesPorBulto;

                    _stockFacade.ConfirmarEnvioTraspaso(traspaso.IdOrdenTraspaso, usuarioActivo, traspaso.Detalles);

                    MessageBox.Show("El remito de traspaso fue generado y el stock ha sido descontado correctamente. La orden se encuentra 'En Tránsito'.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ActualizarPantallaCompleta();
                }
                catch (ReglaNegocioComprasException ex)
                {
                    MessageBox.Show(ex.Message, "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error crítico al procesar el envío: " + ex.Message, "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitud.CurrentRow == null) return;

            var traspaso = (OrdenTraspasoDTO)dgvSolicitud.CurrentRow.DataBoundItem;

            DialogResult result = MessageBox.Show(
                $"¿Desea RECHAZAR el traspaso N° {traspaso.NroTraspaso}? \n\n" +
                "La solicitud original volverá a la mesa de compras de la sucursal destino.",
                "Rechazar Traspaso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Aquí llamarías a un método en tu Facade para cancelar el traspaso
                    // Ejemplo: _stockFacade.CancelarTraspaso(traspaso.IdOrdenTraspaso);

                    MessageBox.Show("Traspaso rechazado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarPantallaCompleta();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al cancelar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Lógica para filtrar la lista _traspasosPendientes usando LINQ
            // según lo que se haya escrito en el TextBox o seleccionado en el ComboBox.
        }

        #endregion

        #region Configuración Visual de Grillas

        private void ConfigurarGrillaMaestro()
        {
            dgvSolicitud.AutoGenerateColumns = false;
            dgvSolicitud.AllowUserToAddRows = false;
            dgvSolicitud.ReadOnly = true; // Toda la grilla maestra es de lectura
            dgvSolicitud.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSolicitud.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSolicitud.RowHeadersVisible = false;
            dgvSolicitud.BackgroundColor = Color.White;

            dgvSolicitud.Columns.Clear();

            dgvSolicitud.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroTraspaso",
                DataPropertyName = "NroTraspaso",
                HeaderText = "N° Traspaso",
                Width = 80
            });

            dgvSolicitud.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SucursalDestinoNombre",
                DataPropertyName = "SucursalDestinoNombre",
                HeaderText = "Sucursal Solicitante",
                Width = 150
            });

            var colFecha = new DataGridViewTextBoxColumn
            {
                Name = "FechaEmision",
                DataPropertyName = "FechaEmision",
                HeaderText = "Fecha Asignación",
                Width = 120
            };
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvSolicitud.Columns.Add(colFecha);
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvDetalle.AutoGenerateColumns = false;
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.ReadOnly = false;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.CellSelect; // Permite seleccionar una celda para editar
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.BackgroundColor = Color.White;

            dgvDetalle.Columns.Clear();

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku",
                HeaderText = "SKU",
                Width = 80,
                ReadOnly = true 
            });

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Artículo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            var colStock = new DataGridViewTextBoxColumn
            {
                Name = "StockActualBultos",
                DataPropertyName = "StockActualBultos",
                HeaderText = "Stock Disp. (Bultos)",
                Width = 110,
                ReadOnly = true 
            };
            colStock.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); // Sombreado para indicar sistema
            dgvDetalle.Columns.Add(colStock);

            var colSolicitado = new DataGridViewTextBoxColumn
            {
                Name = "CantidadSolicitadaBultos",
                DataPropertyName = "CantidadSolicitadaBultos", 
                HeaderText = "Cant. Solicitada",
                Width = 100,
                ReadOnly = true 
            };
            dgvDetalle.Columns.Add(colSolicitado);

            var colAEnviar = new DataGridViewTextBoxColumn
            {
                Name = "CantidadEnviadaBultos",
                DataPropertyName = "CantidadEnviadaBultos",
                HeaderText = "Cant. a Enviar",
                Width = 100,
                ReadOnly = false
            };
            colAEnviar.DefaultCellStyle.BackColor = Color.LightYellow;
            colAEnviar.DefaultCellStyle.Font = new Font(dgvDetalle.Font, FontStyle.Bold);
            dgvDetalle.Columns.Add(colAEnviar);
        }
        #endregion
    }
}
