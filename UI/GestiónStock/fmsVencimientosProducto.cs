using BLL.DomainDtos;
using BLL.GestiónStock.Interface;
using Service.Facade;
using System.ComponentModel;
using System.Data;



namespace UI.GestiónStock
{
    public partial class fmsVencimientosProducto : Form
    {
        private readonly IFacade _stockFacade;
        private readonly StockPorSucursalDTO _productoOriginal;


        private DataGridView dgvLotes = null!;
        private List<LoteDetalleVencimientoDTO> _lotesCompletos = new();
        private BindingList<LoteDetalleVencimientoDTO> _lotesFiltrados = new();
        public fmsVencimientosProducto
        (
            IFacade stockFacade,
            StockPorSucursalDTO productoElegido

        )
        {
            InitializeComponent();
            _stockFacade = stockFacade;
            _productoOriginal = productoElegido;
           
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }
        #region Botones y Eventos
        private void btnRegistrarMerma_Click(object sender, EventArgs e)
        {

            if (dgvLote.CurrentRow != null && dgvLote.CurrentRow.DataBoundItem is LoteDetalleVencimientoDTO loteSeleccionado)
            {

                int cantidadAMermar = loteSeleccionado.CantidadActual;

                var resultado = MessageBox.Show($"¿Está seguro de registrar la baja por vencimiento de {cantidadAMermar} u. para el lote {loteSeleccionado.NumeroLote}?",
                                                "Confirmar Merma Sanitaria", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        Guid sucursalId = SessionManager.Current.IdSucursalActual ?? throw new Exception("Sesión inválida.");

                        _stockFacade.RegistrarMermaLote(loteSeleccionado.IdLote, cantidadAMermar, "Descarte por vencimiento confirmado en depósito.", sucursalId);

            
                        MessageBox.Show("Merma registrada con éxito. El stock fue descontado y auditado en el Kardex.", "Control de Depósito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarDatosLotes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error al mermar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un lote específico de la grilla para aplicar la baja.", "Control de Depósito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarLotes();
        }

        #endregion

        #region Carga y Configuración de Datos
        private void fmsVencimientosProducto_Load(object sender, EventArgs e)
        {
            this.Text = $"Desglose de Trazabilidad: {_productoOriginal.ProductoNombre}";

            ConfigurarColumnasGrilla(); 
            CargarDatosLotes();
        }

        private void ConfigurarColumnasGrilla()
        {
            dgvLote.Columns.Clear();
            dgvLote.AutoGenerateColumns = false;
            dgvLote.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLote.MultiSelect = false;
            dgvLote.RowHeadersVisible = false;
            dgvLote.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLote.BackgroundColor = Color.White;
            dgvLote.BorderStyle = BorderStyle.None;
            dgvLote.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvLote.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);

            // Definimos y estructuramos las columnas sobre TU dgvLote
            dgvLote.Columns.Add(new DataGridViewTextBoxColumn { Name = "Lote", DataPropertyName = "NumeroLote", HeaderText = "Nro. Lote", FillWeight = 110 });
            dgvLote.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ingreso", DataPropertyName = "FechaIngreso", HeaderText = "F. Ingreso", FillWeight = 85 });
            dgvLote.Columns.Add(new DataGridViewTextBoxColumn { Name = "Vencimiento", DataPropertyName = "TipoVencimientoTexto", HeaderText = "F. Vencimiento", FillWeight = 95 });
            dgvLote.Columns.Add(new DataGridViewTextBoxColumn { Name = "StockLote", DataPropertyName = "CantidadActual", HeaderText = "Stock Disp. (u)", FillWeight = 85 });
            dgvLote.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "EstadoVisualTexto", HeaderText = "Situación / Plazo", FillWeight = 110 });

            // Alineaciones perfectas para lectura rápida
            dgvLote.Columns["Ingreso"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLote.Columns["Vencimiento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLote.Columns["StockLote"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Enganchamos el evento para pintar de colores los lotes en riesgo sanitario
            dgvLote.CellFormatting += dgvLote_CellFormatting;

        }

        private void CargarDatosLotes()
        {
            try
            {
                Guid sucursalId = SessionManager.Current.IdSucursalActual ?? throw new Exception("Sesión inválida.");

                // Consultamos a la BLL
                _lotesCompletos = _stockFacade.ObtenerLotesPorProducto(_productoOriginal.IdProducto, sucursalId);

                // Calculamos KPIs de las etiquetas superiores
                CalcularIndicadoresMermas(_lotesCompletos);

                // Enlazamos a la grilla
                ActualizarDataSourceGrilla(_lotesCompletos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desglosar los lotes: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void CalcularIndicadoresMermas(List<LoteDetalleVencimientoDTO> lotes)
        {
            // 1. Conteo total de lotes físicos en estantería
            lblLotesActivosContador.Text = $"{lotes.Count} lote(s)";

            if (!lotes.Any())
            {
                lblProximoVencimientoFecha.Text = "-";
                lblEstadoSanitarioTexto.Text = "Sin existencias";
                lblEstadoSanitarioTexto.ForeColor = Color.Gray;
                return;
            }

            // 2. Buscar el lote que expire primero (excluyendo los que no vencen)
            var lotesConVencimiento = lotes.Where(l => l.FechaVencimiento.HasValue).ToList();
            if (lotesConVencimiento.Any())
            {
                var proximo = lotesConVencimiento.Min(l => l.FechaVencimiento!.Value);
                lblProximoVencimientoFecha.Text = proximo.ToString("dd/MM/yyyy");
            }
            else
            {
                lblProximoVencimientoFecha.Text = "No Perecedero";
            }

            // 3. Evaluar Estado Sanitario Crítico/Alerta
            int diasMinimos = lotes.Any() ? lotes.Min(l => l.DiasRestantes) : 9999;
            int diasAlertaSugeridos = _productoOriginal.DiasAlertaVencimiento ?? 7;

            if (diasMinimos < 0)
            {
                lblEstadoSanitarioTexto.Text = "RIESGO CRÍTICO (Vencidos)";
                lblEstadoSanitarioTexto.ForeColor = Color.DarkRed;
            }
            else if (diasMinimos <= diasAlertaSugeridos)
            {
                lblEstadoSanitarioTexto.Text = "ALERTA (Próximos a vencer)";
                lblEstadoSanitarioTexto.ForeColor = Color.DarkGoldenrod;
            }
            else
            {
                lblEstadoSanitarioTexto.Text = "Estable / Seguro";
                lblEstadoSanitarioTexto.ForeColor = Color.DarkGreen;
            }
        }

        private void ActualizarDataSourceGrilla(List<LoteDetalleVencimientoDTO> lista)
        {
            _lotesFiltrados = new BindingList<LoteDetalleVencimientoDTO>(lista);
            dgvLote.DataSource = _lotesFiltrados;
        }

        private void FiltrarLotes()
        {
            string criterio = txtBuscarLote.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(criterio))
            {
                ActualizarDataSourceGrilla(_lotesCompletos);
            }
            else
            {
                var filtrados = _lotesCompletos
                    .Where(l => l.NumeroLote.ToLower().Contains(criterio))
                    .ToList();
                ActualizarDataSourceGrilla(filtrados);
            }
        }

        private void txtBuscarLote_TextChanged(object sender, EventArgs e)
        {
            FiltrarLotes();
        }

        private void dgvLote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLote_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvLote.Columns[e.ColumnIndex].Name == "Estado"
                && dgvLote.Rows[e.RowIndex].DataBoundItem is LoteDetalleVencimientoDTO lote)
            {
                int diasAlertaSugeridos = _productoOriginal.DiasAlertaVencimiento ?? 7;

                if (lote.DiasRestantes < 0) // Lote Expirado 
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 214, 207);
                    e.CellStyle.ForeColor = Color.DarkRed;
                }
                else if (lote.DiasRestantes <= diasAlertaSugeridos) // Lote en margen de peligro 
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(133, 100, 4);
                }
            }
        }

        #endregion
    }
}
