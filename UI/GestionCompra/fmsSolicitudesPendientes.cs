using BLL.DomainDtos;
using BLL.GestiónCompra.Exceptions;
using BLL.GestiónCompra.Facade;
using BLL.GestiónStock.Facade;
using BLL.GestiónSucursal.Exceptions;
using BLL.GestiónSucursal.Facade;
using Service.Facade;
using System;

namespace UI.GestionCompra
{
    public partial class fmsSolicitudesPendientes : Form
    {
        private readonly OrdenCompraFacade _comprasFacade;
        private List<SolicitudPedidoDTO> _solicitudesLocales;
        private readonly SolicitudPedidoFacade _solicitudFacade;
        public event EventHandler SolicitudProcesada;
        private readonly StockFacade _stockFacade;
        private readonly SucursalFacade _sucursalFacade;


        public fmsSolicitudesPendientes
        (
            OrdenCompraFacade comprasFacade,
            SolicitudPedidoFacade solicitudFacade,
            StockFacade stockFacade,
            SucursalFacade sucursalFacade
        )
        {
            InitializeComponent();
            _comprasFacade = comprasFacade ?? throw new ArgumentNullException(nameof(comprasFacade));
            _solicitudFacade = solicitudFacade ?? throw new ArgumentNullException(nameof(solicitudFacade));
            _stockFacade = stockFacade ?? throw new ArgumentNullException(nameof(stockFacade));
            _sucursalFacade = sucursalFacade ?? throw new ArgumentNullException(nameof(sucursalFacade));
            this.dgvSolicitudPedido.SelectionChanged += dgvSolicitudPedido_SelectionChanged!;
        }
        private void fmsSolicitudesPendientes_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaMaestro();
            ConfigurarGrillaDetalle();
            ActualizarPantallaCompleta();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            // Notificamos al formulario padre que el estado de las solicitudes pudo haber cambiado
            SolicitudProcesada?.Invoke(this, EventArgs.Empty);
        }

        #region Refresco Reactivo

        /// <summary>
        /// Centraliza la lectura de la base de datos y redibuja los controles en cascada.
        /// Se llama en el Load y después de cada acción de botones.
        /// </summary>
        private void ActualizarPantallaCompleta()
        {
            try
            {
                // 1. Tomamos la sucursal logueada desde el contexto global ya existente
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                      ?? throw new Exception("No se detectó una sucursal activa en la sesión actual.");

                // 2. Traemos únicamente los registros que le pertenecen a esta terminal (Vienen filtrados desde SQL Server)
                _solicitudesLocales = _comprasFacade.ConsultarSolicitudesPendientes(idSucursalActual).ToList();

                // 3. Impactamos el DataSource directo del Maestro (dgvSolicitudPedido)
                dgvSolicitudPedido.DataSource = null;
                dgvSolicitudPedido.DataSource = _solicitudesLocales;

                // 4. Control reactivo de amortiguación: si la grilla izquierda quedó vacía, 
                // limpiamos la derecha inmediatamente para no dejar basura visual
                bool tieneRegistros = _solicitudesLocales.Count > 0;
                if (!tieneRegistros)
                {
                    dgvDetalleSolicitud.DataSource = null;
                }

                btnGenerarOrdenCompra.Enabled = tieneRegistros;
                btnRechazar.Enabled = tieneRegistros;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al sincronizar el catálogo de solicitudes locales: {ex.Message}",
                                "Error de Persistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Botones 
        private void btnGenerarOrdenCompra_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudPedido.CurrentRow == null) return;

            try
            {
                var solicitud = (SolicitudPedidoDTO)dgvSolicitudPedido.CurrentRow.DataBoundItem;

                DialogResult result = MessageBox.Show(
                    $"¿Desea pre-aprobar la Solicitud N° {solicitud.NroSolicitud}? " +
                    "Esto agrupará y creará las Órdenes de Compra en estado borrador listas para auditar en el Historial.",
                    "Confirmación Operativa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                        ?? throw new Exception("No se detectó una sucursal activa en la sesión actual.");


                    _comprasFacade.EjecutarGeneracionAutomatica(idSucursalActual, solicitud.IdSolicitudPedido);

                    MessageBox.Show("Pre-Órdenes de Compra generadas correctamente en el Historial.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refrescamos las grillas reactivamente
                    ActualizarPantallaCompleta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudPedido.CurrentRow == null) return;

            try
            {
                var solicitud = (SolicitudPedidoDTO)dgvSolicitudPedido.CurrentRow.DataBoundItem;

                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de rechazar y archivar la Solicitud de Pedido N° {solicitud.NroSolicitud}?",
                    "Alerta de Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Cambia el estado a Rechazada 
                    _solicitudFacade.CambiarEstado(solicitud.IdSolicitudPedido, 3);

                    MessageBox.Show("La solicitud fue desestimada y removida de la mesa de entradas.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarPantallaCompleta();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Mutación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region  Evento Maestro-Detalle (Sincronización de Grillas)
        private void dgvSolicitudPedido_SelectionChanged(object sender, EventArgs e)
        {
            // Validamos que haya una fila seleccionada físicamente
            if (dgvSolicitudPedido.CurrentRow == null) return;

            try
            {
                // Convertimos la fila activa directamente al DTO de negocio
                var solicitudSeleccionada = (SolicitudPedidoDTO)dgvSolicitudPedido.CurrentRow.DataBoundItem;

                if (solicitudSeleccionada != null)
                {
                    // Volcamos en tiempo real la lista interna de renglones en la grilla derecha
                    dgvDetalleSolicitud.DataSource = null;
                    dgvDetalleSolicitud.DataSource = solicitudSeleccionada.Detalles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al sincronizar los renglones del pedido: {ex.Message}",
                                "Error de Renderizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Configuración de los dgv
        private void ConfigurarGrillaMaestro()
        {
            dgvSolicitudPedido.AutoGenerateColumns = false;
            dgvSolicitudPedido.AllowUserToAddRows = false;
            dgvSolicitudPedido.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSolicitudPedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSolicitudPedido.RowHeadersVisible = false;
            dgvSolicitudPedido.BackgroundColor = Color.White;
            dgvSolicitudPedido.BorderStyle = BorderStyle.None;
            dgvSolicitudPedido.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvSolicitudPedido.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
            dgvSolicitudPedido.Columns.Clear();

            dgvSolicitudPedido.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NroSolicitud",
                DataPropertyName = "NroSolicitud", // Mapea directo al DTO
                HeaderText = "N° Solicitud",
                Width = 100,
                ReadOnly = true
            });

            var colFecha = new DataGridViewTextBoxColumn
            {
                Name = "FechaSolicitud",
                DataPropertyName = "FechaSolicitud",
                HeaderText = "Fecha Emisión",
                Width = 120,
                ReadOnly = true
            };
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvSolicitudPedido.Columns.Add(colFecha);

            dgvSolicitudPedido.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstadoNombre",
                DataPropertyName = "EstadoNombre",
                HeaderText = "Estado",
                Width = 100,
                ReadOnly = true
            });

            dgvSolicitudPedido.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioNombre",
                DataPropertyName = "UsuarioNombre",
                HeaderText = "Solicitante",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });
        }
        private void ConfigurarGrillaDetalle()
        {
            dgvDetalleSolicitud.AutoGenerateColumns = false;
            dgvDetalleSolicitud.AllowUserToAddRows = false;
            dgvDetalleSolicitud.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalleSolicitud.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalleSolicitud.RowHeadersVisible = false;
            dgvDetalleSolicitud.BackgroundColor = Color.White;
            dgvDetalleSolicitud.BorderStyle = BorderStyle.None;
            dgvDetalleSolicitud.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvDetalleSolicitud.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);

            dgvDetalleSolicitud.Columns.Clear();

            dgvDetalleSolicitud.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Renglon",
                DataPropertyName = "Renglon",
                HeaderText = "Rng",
                Width = 45,
                ReadOnly = true
            });

            dgvDetalleSolicitud.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoSku",
                DataPropertyName = "CodigoSku",
                HeaderText = "SKU",
                Width = 85,
                ReadOnly = true
            });

            dgvDetalleSolicitud.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                DataPropertyName = "ProductoNombre",
                HeaderText = "Descripción del Artículo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });


            var colCant = new DataGridViewTextBoxColumn
            {
                Name = "CantidadBultosSolicitada",
                DataPropertyName = "CantidadBultosSolicitada",
                HeaderText = "Cant. Bultos",
                Width = 100,
                ReadOnly = true
            };
            colCant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleSolicitud.Columns.Add(colCant);

        }

        #endregion

        private void btnSolicitarTraspaso_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudPedido.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una solicitud de la lista.", "Atención",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var solicitud = (SolicitudPedidoDTO)dgvSolicitudPedido.CurrentRow.DataBoundItem;

                // 1. Buscamos cuál es el verdadero Depósito Central en la BD (La Opción B)
                Guid idDepositoCentral = _sucursalFacade.ObtenerIdDepositoCentral();

                // 2. Identificamos en qué sucursal está trabajando el operario actualmente
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual
                                         ?? throw new Exception("No se detectó sucursal en sesión.");
                if (idSucursalActual == idDepositoCentral)
                {
                    MessageBox.Show("Estás operando desde el Depósito Central.\n\n" +
                                    "No puedes solicitar un traspaso hacia tu propio depósito. " +
                                    "Para reabastecerte, debes usar la opción 'Generar pre OC' para pedirle a un proveedor externo.",
                                    "Operación Logística Inválida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // 3. Confirmación al usuario
                DialogResult result = MessageBox.Show(
                    $"¿Desea derivar la Solicitud N° {solicitud.NroSolicitud} al Depósito Central para armar un Traspaso?\n\n" +
                    "Esto sacará la solicitud de tu mesa de compras local y la enviará al área de logística.",
                    "Confirmación de Traspaso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // 4. Enviamos el traspaso hacia el idDepositoCentral
                    _stockFacade.GenerarTraspasoDesdeSolicitud(idDepositoCentral, solicitud.IdSolicitudPedido);

                    MessageBox.Show("La solicitud fue derivada exitosamente al Depósito Central y se encuentra en estado 'Preparación'.",
                                    "Traspaso Derivado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refrescamos las grillas para que la solicitud desaparezca de aquí
                    ActualizarPantallaCompleta();
                }
            }
            catch (SucursalServiceException ex)
            {
                // Esto atrapará si por error borraron la sucursal de depósito de la BD
                MessageBox.Show(ex.Message, "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ReglaNegocioComprasException ex)
            {
                MessageBox.Show(ex.Message, "Validación de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar derivar el traspaso: " + ex.Message,
                                "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

