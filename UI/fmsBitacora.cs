using Service.DomainModel.Logging;
using Service.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class fmsBitacora : Form
    {
        private readonly BitácoraService _bitacoraService;
        private BindingSource _bindingSource;
        public fmsBitacora()
        {
            InitializeComponent();
            _bitacoraService = new BitácoraService();
            _bindingSource = new BindingSource();
        }

        private void fmsBitacora_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            CargarBitacora();
            cbxTipoMovimiento.DataSource = Enum.GetValues(typeof(Criticidad));
            cbxTipoMovimiento.SelectedIndex = -1; 
        }

        #region Botones 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtenemos la lista original desde el BindingSource
            var listaOriginal = (List<Bitácora>)_bindingSource.DataSource;

            // Aplicamos los filtros
            var filtrado = listaOriginal.Where(x =>
            {
                // Filtro por ComboBox (Criticidad)
                bool cumpleTipo = (cbxTipoMovimiento.SelectedIndex == -1) ||
                                  (x.Criticidad == (Criticidad)cbxTipoMovimiento.SelectedItem);

                return cumpleTipo;
            }).ToList();

            // Actualizamos la grilla
            dgvBitacora.DataSource = filtrado;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
  
            CargarBitacora();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvBitacora.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"Bitacora_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportarACSV(sfd.FileName);
                    MessageBox.Show("Exportación exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
        private void ConfigurarGrilla()
        {
            // Configuración base de la grilla
            dgvBitacora.AutoGenerateColumns = false;
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false; // Seguridad extra
            dgvBitacora.MultiSelect = false;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.BackgroundColor = Color.White;
            dgvBitacora.BorderStyle = BorderStyle.None;
            dgvBitacora.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvBitacora.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvBitacora.Columns.Clear();

            // Definición de Columnas
            // Fecha: Ancho fijo, formato legible
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            // Usuario: Ancho definido
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreUsuario",
                HeaderText = "Usuario",
                Width = 150
            });

            // Criticidad: Ancho fijo, alineación centrada
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Criticidad",
                HeaderText = "Criticidad",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Mensaje: Fill (toma todo el espacio restante)
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Mensaje",
                HeaderText = "Mensaje",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void CargarBitacora()
        {
            try
            {
                // 1. Obtenemos datos
                var lista = _bitacoraService.ListarBitacoraSegunRol();

                // 2. Ordenamos en memoria para asegurar consistencia
                var listaOrdenada = lista.OrderByDescending(x => x.Fecha).ToList();

                // 3. Asignamos al BindingSource
                _bindingSource.DataSource = listaOrdenada;
                dgvBitacora.DataSource = _bindingSource;

                // Feedback visual si no hay registros
                if (listaOrdenada.Count == 0)
                {
                    // Opcional: podrías mostrar un label indicando que no hay logs
                }
            }
            catch (Exception ex)
            {
                // Logueamos el error usando tu BitacoraService si es posible, o mediante MessageBox
                MessageBox.Show($"Error al cargar la bitácora: {ex.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportarACSV(string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta, false, System.Text.Encoding.UTF8))
            {
                // Cabecera
                sw.WriteLine("Fecha;Usuario;Criticidad;Mensaje");

                // Filas
                foreach (DataGridViewRow row in dgvBitacora.Rows)
                {
                    Bitácora b = (Bitácora)row.DataBoundItem;
                    sw.WriteLine($"{b.Fecha:dd/MM/yyyy HH:mm};{b.NombreUsuario};{b.Criticidad};{b.Mensaje}");
                }
            }
        }
    }
}
