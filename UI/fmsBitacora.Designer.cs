namespace UI
{
    partial class fmsBitacora
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
            dgvBitacora = new DataGridView();
            groupBox1 = new GroupBox();
            btnExportar = new Button();
            btnActualizar = new Button();
            lblBuscar = new Label();
            cbxTipoMovimiento = new ComboBox();
            btnBuscar = new Button();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvBitacora
            // 
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitacora.Location = new Point(17, 22);
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.Size = new Size(1116, 475);
            dgvBitacora.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvBitacora);
            groupBox1.Location = new Point(12, 65);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1151, 513);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // btnExportar
            // 
            btnExportar.BackColor = Color.Azure;
            btnExportar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnExportar.Location = new Point(1035, 584);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(110, 42);
            btnExportar.TabIndex = 12;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = false;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.Location = new Point(705, 21);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(84, 38);
            btnActualizar.TabIndex = 30;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoEllipsis = true;
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBuscar.Location = new Point(29, 25);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(87, 20);
            lblBuscar.TabIndex = 29;
            lblBuscar.Text = "Buscar por:";
            // 
            // cbxTipoMovimiento
            // 
            cbxTipoMovimiento.FormattingEnabled = true;
            cbxTipoMovimiento.Location = new Point(126, 25);
            cbxTipoMovimiento.Name = "cbxTipoMovimiento";
            cbxTipoMovimiento.Size = new Size(203, 23);
            cbxTipoMovimiento.TabIndex = 28;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.Location = new Point(588, 21);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(84, 38);
            btnBuscar.TabIndex = 27;
            btnBuscar.Text = "Buscar ";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(351, 26);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(203, 23);
            comboBox1.TabIndex = 31;
            // 
            // fmsBitacora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1175, 638);
            Controls.Add(comboBox1);
            Controls.Add(btnActualizar);
            Controls.Add(lblBuscar);
            Controls.Add(cbxTipoMovimiento);
            Controls.Add(btnBuscar);
            Controls.Add(btnExportar);
            Controls.Add(groupBox1);
            Name = "fmsBitacora";
            Text = "Bitacora";
            Load += fmsBitacora_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvBitacora;
        private GroupBox groupBox1;
        private Button btnExportar;
        private Button btnActualizar;
        private Label lblBuscar;
        private ComboBox cbxTipoMovimiento;
        private Button btnBuscar;
        private ComboBox comboBox1;
    }
}