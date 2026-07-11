namespace UI.GestionCompra
{
    partial class fmsCargarOrdenCompra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRechazar = new Button();
            lblDetalle = new Label();
            lblSolicitud = new Label();
            dgvDetalleOrdenCompra = new DataGridView();
            dgvPreOrdenCompra = new DataGridView();
            btnGenerarOrdenCompra = new Button();
            btnCrear = new Button();
            btnModificar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOrdenCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPreOrdenCompra).BeginInit();
            SuspendLayout();
            // 
            // btnRechazar
            // 
            btnRechazar.BackColor = Color.Azure;
            btnRechazar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRechazar.Location = new Point(12, 356);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(132, 49);
            btnRechazar.TabIndex = 29;
            btnRechazar.Text = "Rechazar ";
            btnRechazar.UseVisualStyleBackColor = false;
            btnRechazar.Click += btnRechazar_Click;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDetalle.Location = new Point(502, 12);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(119, 19);
            lblDetalle.TabIndex = 28;
            lblDetalle.Text = "Detalle de Orden ";
            // 
            // lblSolicitud
            // 
            lblSolicitud.AutoSize = true;
            lblSolicitud.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSolicitud.Location = new Point(12, 12);
            lblSolicitud.Name = "lblSolicitud";
            lblSolicitud.Size = new Size(144, 19);
            lblSolicitud.TabIndex = 27;
            lblSolicitud.Text = "Pre Orden de Compra";
            // 
            // dgvDetalleOrdenCompra
            // 
            dgvDetalleOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleOrdenCompra.Location = new Point(502, 34);
            dgvDetalleOrdenCompra.Name = "dgvDetalleOrdenCompra";
            dgvDetalleOrdenCompra.Size = new Size(642, 440);
            dgvDetalleOrdenCompra.TabIndex = 26;
            // 
            // dgvPreOrdenCompra
            // 
            dgvPreOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPreOrdenCompra.Location = new Point(12, 34);
            dgvPreOrdenCompra.Name = "dgvPreOrdenCompra";
            dgvPreOrdenCompra.Size = new Size(477, 306);
            dgvPreOrdenCompra.TabIndex = 25;
            dgvPreOrdenCompra.SelectionChanged += dgvPreOrdenCompra_SelectionChanged;
            // 
            // btnGenerarOrdenCompra
            // 
            btnGenerarOrdenCompra.BackColor = Color.Azure;
            btnGenerarOrdenCompra.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarOrdenCompra.Location = new Point(178, 417);
            btnGenerarOrdenCompra.Name = "btnGenerarOrdenCompra";
            btnGenerarOrdenCompra.Size = new Size(132, 71);
            btnGenerarOrdenCompra.TabIndex = 24;
            btnGenerarOrdenCompra.Text = "Generar Orden";
            btnGenerarOrdenCompra.UseVisualStyleBackColor = false;
            btnGenerarOrdenCompra.Click += btnGenerarOrdenCompra_Click;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.Azure;
            btnCrear.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCrear.Location = new Point(178, 356);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(132, 49);
            btnCrear.TabIndex = 30;
            btnCrear.Text = "Crear ";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificar.Location = new Point(357, 356);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(132, 49);
            btnModificar.TabIndex = 31;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // fmsCargarOrdenCompra
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1156, 500);
            Controls.Add(btnModificar);
            Controls.Add(btnCrear);
            Controls.Add(btnRechazar);
            Controls.Add(lblDetalle);
            Controls.Add(lblSolicitud);
            Controls.Add(dgvDetalleOrdenCompra);
            Controls.Add(dgvPreOrdenCompra);
            Controls.Add(btnGenerarOrdenCompra);
            Name = "fmsCargarOrdenCompra";
            Text = "Orden de Compra";
            Load += fmsCargarOrdenCompra_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOrdenCompra).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPreOrdenCompra).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRechazar;
        private Label lblDetalle;
        private Label lblSolicitud;
        private DataGridView dgvDetalleOrdenCompra;
        private DataGridView dgvPreOrdenCompra;
        private Button btnGenerarOrdenCompra;
        private Button btnCrear;
        private Button btnModificar;
    }
}