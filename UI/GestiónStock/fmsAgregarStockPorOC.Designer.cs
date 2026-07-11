namespace UI.GestiónStock
{
    partial class fmsAgregarStockPorOC
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
            lblOrdenCompra = new Label();
            dgvDetalleOrdenCompra = new DataGridView();
            dgvOrdenCompra = new DataGridView();
            btnIngresar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOrdenCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenCompra).BeginInit();
            SuspendLayout();
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDetalle.Location = new Point(485, 9);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(119, 19);
            lblDetalle.TabIndex = 35;
            lblDetalle.Text = "Detalle de Orden ";
            // 
            // lblOrdenCompra
            // 
            lblOrdenCompra.AutoSize = true;
            lblOrdenCompra.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblOrdenCompra.Location = new Point(12, 9);
            lblOrdenCompra.Name = "lblOrdenCompra";
            lblOrdenCompra.Size = new Size(120, 19);
            lblOrdenCompra.TabIndex = 34;
            lblOrdenCompra.Text = "Orden de Compra";
            // 
            // dgvDetalleOrdenCompra
            // 
            dgvDetalleOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleOrdenCompra.Location = new Point(485, 31);
            dgvDetalleOrdenCompra.Name = "dgvDetalleOrdenCompra";
            dgvDetalleOrdenCompra.Size = new Size(649, 424);
            dgvDetalleOrdenCompra.TabIndex = 33;
            dgvDetalleOrdenCompra.CellContentClick += dgvDetalleOrdenCompra_CellContentClick;
            dgvDetalleOrdenCompra.DataError += dgvDetalleOrdenCompra_DataError;
            // 
            // dgvOrdenCompra
            // 
            dgvOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenCompra.Location = new Point(12, 31);
            dgvOrdenCompra.Name = "dgvOrdenCompra";
            dgvOrdenCompra.Size = new Size(455, 336);
            dgvOrdenCompra.TabIndex = 32;
            dgvOrdenCompra.SelectionChanged += dgvOrdenCompra_SelectionChanged;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.Azure;
            btnIngresar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(346, 388);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(121, 67);
            btnIngresar.TabIndex = 31;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // fmsAgregarStockPorOC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1146, 480);
            Controls.Add(lblDetalle);
            Controls.Add(lblOrdenCompra);
            Controls.Add(dgvDetalleOrdenCompra);
            Controls.Add(dgvOrdenCompra);
            Controls.Add(btnIngresar);
            Name = "fmsAgregarStockPorOC";
            Text = "fmsAgregarStockPorOC";
            Load += fmsAgregarStockPorOC_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOrdenCompra).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenCompra).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblDetalle;
        private Label lblOrdenCompra;
        private DataGridView dgvDetalleOrdenCompra;
        private DataGridView dgvOrdenCompra;
        private Button btnIngresar;
    }
}