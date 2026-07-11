namespace UI.GestiónStock
{
    partial class fmsVencimientosProducto
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
            dgvLote = new DataGridView();
            btnCerrar = new Button();
            txtBuscarLote = new TextBox();
            lblProducto = new Label();
            lblLotesActivosContador = new Label();
            lblProximoVencimientoFecha = new Label();
            lblEstadoSanitarioTexto = new Label();
            groupBox1 = new GroupBox();
            btnBuscar = new Button();
            btnRegistrarMerma = new Button();
            btnDescontarStock = new Button();
            nupCantidad = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLote).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nupCantidad).BeginInit();
            SuspendLayout();
            // 
            // dgvLote
            // 
            dgvLote.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLote.Location = new Point(12, 183);
            dgvLote.Name = "dgvLote";
            dgvLote.Size = new Size(774, 359);
            dgvLote.TabIndex = 0;
            dgvLote.CellContentClick += dgvLote_CellContentClick;
            dgvLote.CellFormatting += dgvLote_CellFormatting;
            dgvLote.SelectionChanged += dgvLote_SelectionChanged;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Azure;
            btnCerrar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCerrar.Location = new Point(705, 606);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(81, 29);
            btnCerrar.TabIndex = 14;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // txtBuscarLote
            // 
            txtBuscarLote.Location = new Point(176, 132);
            txtBuscarLote.Name = "txtBuscarLote";
            txtBuscarLote.Size = new Size(267, 23);
            txtBuscarLote.TabIndex = 15;
            txtBuscarLote.TextChanged += txtBuscarLote_TextChanged;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblProducto.Location = new Point(12, 132);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(158, 20);
            lblProducto.TabIndex = 16;
            lblProducto.Text = "Buscar por Nro. Lote: ";
            // 
            // lblLotesActivosContador
            // 
            lblLotesActivosContador.AutoSize = true;
            lblLotesActivosContador.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblLotesActivosContador.Location = new Point(6, 38);
            lblLotesActivosContador.Name = "lblLotesActivosContador";
            lblLotesActivosContador.Size = new Size(101, 20);
            lblLotesActivosContador.TabIndex = 17;
            lblLotesActivosContador.Text = "Lotes Activos:";
            // 
            // lblProximoVencimientoFecha
            // 
            lblProximoVencimientoFecha.AutoSize = true;
            lblProximoVencimientoFecha.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblProximoVencimientoFecha.Location = new Point(237, 38);
            lblProximoVencimientoFecha.Name = "lblProximoVencimientoFecha";
            lblProximoVencimientoFecha.Size = new Size(160, 20);
            lblProximoVencimientoFecha.TabIndex = 18;
            lblProximoVencimientoFecha.Text = "Próximo Vencimiento:";
            // 
            // lblEstadoSanitarioTexto
            // 
            lblEstadoSanitarioTexto.AutoSize = true;
            lblEstadoSanitarioTexto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblEstadoSanitarioTexto.Location = new Point(558, 38);
            lblEstadoSanitarioTexto.Name = "lblEstadoSanitarioTexto";
            lblEstadoSanitarioTexto.Size = new Size(58, 20);
            lblEstadoSanitarioTexto.TabIndex = 19;
            lblEstadoSanitarioTexto.Text = "Estado:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblLotesActivosContador);
            groupBox1.Controls.Add(lblEstadoSanitarioTexto);
            groupBox1.Controls.Add(lblProximoVencimientoFecha);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(774, 88);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Información de stock";
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.Azure;
            btnBuscar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnBuscar.Location = new Point(464, 128);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(81, 29);
            btnBuscar.TabIndex = 21;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnRegistrarMerma
            // 
            btnRegistrarMerma.BackColor = Color.Azure;
            btnRegistrarMerma.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnRegistrarMerma.Location = new Point(464, 550);
            btnRegistrarMerma.Name = "btnRegistrarMerma";
            btnRegistrarMerma.Size = new Size(137, 43);
            btnRegistrarMerma.TabIndex = 22;
            btnRegistrarMerma.Text = "Registrar Merma";
            btnRegistrarMerma.UseVisualStyleBackColor = false;
            btnRegistrarMerma.Click += btnRegistrarMerma_Click;
            // 
            // btnDescontarStock
            // 
            btnDescontarStock.BackColor = Color.Azure;
            btnDescontarStock.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnDescontarStock.Location = new Point(249, 550);
            btnDescontarStock.Name = "btnDescontarStock";
            btnDescontarStock.Size = new Size(137, 43);
            btnDescontarStock.TabIndex = 23;
            btnDescontarStock.Text = "Descontar stock";
            btnDescontarStock.UseVisualStyleBackColor = false;
            btnDescontarStock.Click += btnDescontarStock_Click;
            // 
            // nupCantidad
            // 
            nupCantidad.Location = new Point(98, 559);
            nupCantidad.Name = "nupCantidad";
            nupCantidad.Size = new Size(130, 23);
            nupCantidad.TabIndex = 24;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(18, 557);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 25;
            label1.Text = "Cantidad:";
            // 
            // fmsVencimientosProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(792, 647);
            Controls.Add(label1);
            Controls.Add(nupCantidad);
            Controls.Add(btnDescontarStock);
            Controls.Add(btnRegistrarMerma);
            Controls.Add(btnBuscar);
            Controls.Add(groupBox1);
            Controls.Add(lblProducto);
            Controls.Add(txtBuscarLote);
            Controls.Add(btnCerrar);
            Controls.Add(dgvLote);
            Name = "fmsVencimientosProducto";
            Text = "Vencimiento del Producto";
            Load += fmsVencimientosProducto_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLote).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nupCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLote;
        private Button btnCerrar;
        private TextBox txtBuscarLote;
        private Label lblProducto;
        private Label lblLotesActivosContador;
        private Label lblProximoVencimientoFecha;
        private Label lblEstadoSanitarioTexto;
        private GroupBox groupBox1;
        private Button btnBuscar;
        private Button btnRegistrarMerma;
        private Button btnDescontarStock;
        private NumericUpDown nupCantidad;
        private Label label1;
    }
}