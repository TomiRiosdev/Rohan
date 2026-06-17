namespace UI.GestionCompra
{
    partial class fmsHistorialOrdenCompra
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
            lblDetalle = new Label();
            lblSolicitud = new Label();
            dgvDetalleHistorial = new DataGridView();
            dgvMasterHistorial = new DataGridView();
            cbxBuscar = new ComboBox();
            label2 = new Label();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleHistorial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMasterHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDetalle.Location = new Point(502, 69);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(119, 19);
            lblDetalle.TabIndex = 35;
            lblDetalle.Text = "Detalle de Orden ";
            // 
            // lblSolicitud
            // 
            lblSolicitud.AutoSize = true;
            lblSolicitud.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSolicitud.Location = new Point(12, 69);
            lblSolicitud.Name = "lblSolicitud";
            lblSolicitud.Size = new Size(124, 19);
            lblSolicitud.TabIndex = 34;
            lblSolicitud.Text = " Orden de Compra";
            // 
            // dgvDetalleHistorial
            // 
            dgvDetalleHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleHistorial.Location = new Point(502, 103);
            dgvDetalleHistorial.Name = "dgvDetalleHistorial";
            dgvDetalleHistorial.Size = new Size(642, 315);
            dgvDetalleHistorial.TabIndex = 33;
            // 
            // dgvMasterHistorial
            // 
            dgvMasterHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMasterHistorial.Location = new Point(12, 103);
            dgvMasterHistorial.Name = "dgvMasterHistorial";
            dgvMasterHistorial.Size = new Size(477, 266);
            dgvMasterHistorial.TabIndex = 32;
            dgvMasterHistorial.SelectionChanged += dgvMasterHistorial_SelectionChanged;
            // 
            // cbxBuscar
            // 
            cbxBuscar.FormattingEnabled = true;
            cbxBuscar.Location = new Point(81, 23);
            cbxBuscar.Name = "cbxBuscar";
            cbxBuscar.Size = new Size(202, 23);
            cbxBuscar.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 21);
            label2.Name = "label2";
            label2.Size = new Size(63, 21);
            label2.TabIndex = 37;
            label2.Text = "Buscar:";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnBuscar.Location = new Point(298, 16);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(69, 33);
            btnBuscar.TabIndex = 36;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // fmsHistorialOrdenCompra
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1156, 500);
            Controls.Add(cbxBuscar);
            Controls.Add(label2);
            Controls.Add(btnBuscar);
            Controls.Add(lblDetalle);
            Controls.Add(lblSolicitud);
            Controls.Add(dgvDetalleHistorial);
            Controls.Add(dgvMasterHistorial);
            Name = "fmsHistorialOrdenCompra";
            Text = "Historial de Compra";
            Load += fmsHistorialOrdenCompra_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetalleHistorial).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMasterHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDetalle;
        private Label lblSolicitud;
        private DataGridView dgvDetalleHistorial;
        private DataGridView dgvMasterHistorial;
        private ComboBox cbxBuscar;
        private Label label2;
        private Button btnBuscar;
    }
}