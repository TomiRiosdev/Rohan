
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
            btnActualizar = new Button();
            tipodeingreso = new Label();
            cmbTipoRecepcion = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOrdenCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenCompra).BeginInit();
            SuspendLayout();
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblDetalle.Location = new Point(485, 55);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(119, 19);
            lblDetalle.TabIndex = 35;
            lblDetalle.Text = "Detalle de Orden ";
            // 
            // lblOrdenCompra
            // 
            lblOrdenCompra.AutoSize = true;
            lblOrdenCompra.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblOrdenCompra.Location = new Point(12, 55);
            lblOrdenCompra.Name = "lblOrdenCompra";
            lblOrdenCompra.Size = new Size(120, 19);
            lblOrdenCompra.TabIndex = 34;
            lblOrdenCompra.Text = "Orden de Compra";
            // 
            // dgvDetalleOrdenCompra
            // 
            dgvDetalleOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleOrdenCompra.Location = new Point(485, 77);
            dgvDetalleOrdenCompra.Name = "dgvDetalleOrdenCompra";
            dgvDetalleOrdenCompra.Size = new Size(649, 378);
            dgvDetalleOrdenCompra.TabIndex = 33;
            dgvDetalleOrdenCompra.DataError += dgvDetalleOrdenCompra_DataError;
            // 
            // dgvOrdenCompra
            // 
            dgvOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenCompra.Location = new Point(12, 77);
            dgvOrdenCompra.Name = "dgvOrdenCompra";
            dgvOrdenCompra.Size = new Size(455, 293);
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
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnActualizar.Location = new Point(425, 14);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(84, 29);
            btnActualizar.TabIndex = 39;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // tipodeingreso
            // 
            tipodeingreso.AutoEllipsis = true;
            tipodeingreso.AutoSize = true;
            tipodeingreso.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tipodeingreso.Location = new Point(12, 18);
            tipodeingreso.Name = "tipodeingreso";
            tipodeingreso.Size = new Size(119, 20);
            tipodeingreso.TabIndex = 38;
            tipodeingreso.Text = "Tipo de Ingreso:";
            // 
            // cmbTipoRecepcion
            // 
            cmbTipoRecepcion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoRecepcion.FormattingEnabled = true;
            cmbTipoRecepcion.Location = new Point(137, 19);
            cmbTipoRecepcion.Name = "cmbTipoRecepcion";
            cmbTipoRecepcion.Size = new Size(268, 23);
            cmbTipoRecepcion.TabIndex = 37;
            // 
            // fmsAgregarStockPorOC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1146, 480);
            Controls.Add(btnActualizar);
            Controls.Add(tipodeingreso);
            Controls.Add(cmbTipoRecepcion);
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
        private Button btnActualizar;
        private Label tipodeingreso;
        private ComboBox cmbTipoRecepcion;
    }
}