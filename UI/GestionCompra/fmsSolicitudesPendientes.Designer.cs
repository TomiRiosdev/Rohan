namespace UI.GestionCompra
{
    partial class fmsSolicitudesPendientes
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
            btnGenerarOrdenCompra = new Button();
            dgvSolicitudPedido = new DataGridView();
            dgvDetalleSolicitud = new DataGridView();
            lblSolicitud = new Label();
            lblDetalle = new Label();
            btnRechazar = new Button();
            btnSolicitarTraspaso = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleSolicitud).BeginInit();
            SuspendLayout();
            // 
            // btnGenerarOrdenCompra
            // 
            btnGenerarOrdenCompra.BackColor = Color.Azure;
            btnGenerarOrdenCompra.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarOrdenCompra.Location = new Point(365, 406);
            btnGenerarOrdenCompra.Name = "btnGenerarOrdenCompra";
            btnGenerarOrdenCompra.Size = new Size(121, 67);
            btnGenerarOrdenCompra.TabIndex = 18;
            btnGenerarOrdenCompra.Text = "Generar pre OC";
            btnGenerarOrdenCompra.UseVisualStyleBackColor = false;
            btnGenerarOrdenCompra.Click += btnGenerarOrdenCompra_Click;
            // 
            // dgvSolicitudPedido
            // 
            dgvSolicitudPedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitudPedido.Location = new Point(10, 47);
            dgvSolicitudPedido.Name = "dgvSolicitudPedido";
            dgvSolicitudPedido.Size = new Size(477, 334);
            dgvSolicitudPedido.TabIndex = 19;
            dgvSolicitudPedido.SelectionChanged += dgvSolicitudPedido_SelectionChanged;
            // 
            // dgvDetalleSolicitud
            // 
            dgvDetalleSolicitud.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleSolicitud.Location = new Point(499, 44);
            dgvDetalleSolicitud.Name = "dgvDetalleSolicitud";
            dgvDetalleSolicitud.Size = new Size(642, 429);
            dgvDetalleSolicitud.TabIndex = 20;
            // 
            // lblSolicitud
            // 
            lblSolicitud.AutoSize = true;
            lblSolicitud.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSolicitud.Location = new Point(9, 22);
            lblSolicitud.Name = "lblSolicitud";
            lblSolicitud.Size = new Size(150, 19);
            lblSolicitud.TabIndex = 21;
            lblSolicitud.Text = "Solicitudes Pendientes";
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDetalle.Location = new Point(499, 22);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(100, 19);
            lblDetalle.TabIndex = 22;
            lblDetalle.Text = "Detalle Pedido";
            // 
            // btnRechazar
            // 
            btnRechazar.BackColor = Color.Azure;
            btnRechazar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRechazar.Location = new Point(9, 406);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(132, 67);
            btnRechazar.TabIndex = 23;
            btnRechazar.Text = "Rechazar ";
            btnRechazar.UseVisualStyleBackColor = false;
            btnRechazar.Click += btnRechazar_Click;
            // 
            // btnSolicitarTraspaso
            // 
            btnSolicitarTraspaso.BackColor = Color.Azure;
            btnSolicitarTraspaso.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSolicitarTraspaso.Location = new Point(190, 406);
            btnSolicitarTraspaso.Name = "btnSolicitarTraspaso";
            btnSolicitarTraspaso.Size = new Size(132, 67);
            btnSolicitarTraspaso.TabIndex = 24;
            btnSolicitarTraspaso.Text = "Solicitar a Central";
            btnSolicitarTraspaso.UseVisualStyleBackColor = false;
            btnSolicitarTraspaso.Click += btnSolicitarTraspaso_Click;
            // 
            // fmsSolicitudesPendientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1156, 500);
            Controls.Add(btnSolicitarTraspaso);
            Controls.Add(btnRechazar);
            Controls.Add(lblDetalle);
            Controls.Add(lblSolicitud);
            Controls.Add(dgvDetalleSolicitud);
            Controls.Add(dgvSolicitudPedido);
            Controls.Add(btnGenerarOrdenCompra);
            Name = "fmsSolicitudesPendientes";
            Text = "SolicitudYOc";
            Load += fmsSolicitudesPendientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleSolicitud).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnGenerarOrdenCompra;
        private DataGridView dgvSolicitudPedido;
        private DataGridView dgvDetalleSolicitud;
        private Label lblSolicitud;
        private Label lblDetalle;
        private Button btnRechazar;
        private Button btnSolicitarTraspaso;
    }
}