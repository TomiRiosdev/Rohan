namespace UI.GestiónStock
{
    partial class fmsTraspasoSucursal
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
            dgvDetalle = new DataGridView();
            dgvSolicitud = new DataGridView();
            btnConfirmar = new Button();
            cbxSucursal = new ComboBox();
            label2 = new Label();
            btnBuscar = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitud).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnRechazar
            // 
            btnRechazar.BackColor = Color.Azure;
            btnRechazar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRechazar.Location = new Point(71, 399);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(132, 67);
            btnRechazar.TabIndex = 30;
            btnRechazar.Text = "Rechazar ";
            btnRechazar.UseVisualStyleBackColor = false;
            btnRechazar.Click += btnRechazar_Click;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDetalle.Location = new Point(6, 0);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(100, 19);
            lblDetalle.TabIndex = 29;
            lblDetalle.Text = "Detalle Pedido";
            // 
            // lblSolicitud
            // 
            lblSolicitud.AutoSize = true;
            lblSolicitud.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSolicitud.Location = new Point(6, 0);
            lblSolicitud.Name = "lblSolicitud";
            lblSolicitud.Size = new Size(225, 19);
            lblSolicitud.TabIndex = 28;
            lblSolicitud.Text = "Solicitudes Pendientes de traspaso";
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(6, 22);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.Size = new Size(631, 357);
            dgvDetalle.TabIndex = 27;
      
            // 
            // dgvSolicitud
            // 
            dgvSolicitud.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitud.Location = new Point(6, 22);
            dgvSolicitud.Name = "dgvSolicitud";
            dgvSolicitud.Size = new Size(460, 294);
            dgvSolicitud.TabIndex = 26;
   
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.Azure;
            btnConfirmar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmar.Location = new Point(282, 401);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(121, 67);
            btnConfirmar.TabIndex = 25;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // cbxSucursal
            // 
            cbxSucursal.FormattingEnabled = true;
            cbxSucursal.Location = new Point(98, 23);
            cbxSucursal.Name = "cbxSucursal";
            cbxSucursal.Size = new Size(248, 23);
            cbxSucursal.TabIndex = 39;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(18, 21);
            label2.Name = "label2";
            label2.Size = new Size(63, 21);
            label2.TabIndex = 38;
            label2.Text = "Buscar:";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnBuscar.Location = new Point(374, 18);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(69, 33);
            btnBuscar.TabIndex = 37;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblDetalle);
            groupBox1.Controls.Add(dgvDetalle);
            groupBox1.Location = new Point(491, 71);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(643, 395);
            groupBox1.TabIndex = 40;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblSolicitud);
            groupBox2.Controls.Add(dgvSolicitud);
            groupBox2.Location = new Point(12, 71);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(472, 322);
            groupBox2.TabIndex = 41;
            groupBox2.TabStop = false;
            // 
            // fmsTraspasoSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1146, 480);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(cbxSucursal);
            Controls.Add(label2);
            Controls.Add(btnBuscar);
            Controls.Add(btnRechazar);
            Controls.Add(btnConfirmar);
            Name = "fmsTraspasoSucursal";
            Text = "Traspaso a Sucursal";
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitud).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnRechazar;
        private Label lblDetalle;
        private Label lblSolicitud;
        private DataGridView dgvDetalle;
        private DataGridView dgvSolicitud;
        private Button btnConfirmar;
        private ComboBox cbxSucursal;
        private Label label2;
        private Button btnBuscar;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}