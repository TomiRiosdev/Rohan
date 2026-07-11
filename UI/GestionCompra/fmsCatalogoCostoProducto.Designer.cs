namespace UI.GestionCompra
{
    partial class fmsCatalogoCostoProducto
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            Producto = new GroupBox();
            label6 = new Label();
            dgvProducto = new DataGridView();
            gbProveedor = new GroupBox();
            txtTelefono = new TextBox();
            label3 = new Label();
            cxbNombreProveedor = new ComboBox();
            label2 = new Label();
            lblBuscarProveedor = new Label();
            lblRazonSocial = new Label();
            txtEmail = new TextBox();
            txtProvRazonSocial = new TextBox();
            lblProveedor = new Label();
            btnExportar = new Button();
            nupPrecio = new NumericUpDown();
            lblCantidad = new Label();
            btnModificar = new Button();
            Producto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).BeginInit();
            gbProveedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nupPrecio).BeginInit();
            SuspendLayout();
            // 
            // Producto
            // 
            Producto.Controls.Add(label6);
            Producto.Controls.Add(dgvProducto);
            Producto.Font = new Font("Segoe UI", 9F);
            Producto.Location = new Point(427, 25);
            Producto.Name = "Producto";
            Producto.Size = new Size(472, 271);
            Producto.TabIndex = 41;
            Producto.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label6.Location = new Point(6, 0);
            label6.Name = "label6";
            label6.Size = new Size(72, 20);
            label6.TabIndex = 36;
            label6.Text = "Producto";
            // 
            // dgvProducto
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dgvProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvProducto.DefaultCellStyle = dataGridViewCellStyle8;
            dgvProducto.Location = new Point(6, 22);
            dgvProducto.Name = "dgvProducto";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Control;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvProducto.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dgvProducto.Size = new Size(456, 237);
            dgvProducto.TabIndex = 33;
            dgvProducto.SelectionChanged += dgvProducto_SelectionChanged;
            // 
            // gbProveedor
            // 
            gbProveedor.Controls.Add(txtTelefono);
            gbProveedor.Controls.Add(label3);
            gbProveedor.Controls.Add(cxbNombreProveedor);
            gbProveedor.Controls.Add(label2);
            gbProveedor.Controls.Add(lblBuscarProveedor);
            gbProveedor.Controls.Add(lblRazonSocial);
            gbProveedor.Controls.Add(txtEmail);
            gbProveedor.Controls.Add(txtProvRazonSocial);
            gbProveedor.Controls.Add(lblProveedor);
            gbProveedor.Location = new Point(12, 25);
            gbProveedor.Name = "gbProveedor";
            gbProveedor.Size = new Size(409, 214);
            gbProveedor.TabIndex = 40;
            gbProveedor.TabStop = false;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(141, 166);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.ReadOnly = true;
            txtTelefono.Size = new Size(250, 23);
            txtTelefono.TabIndex = 32;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(6, 169);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 31;
            label3.Text = "Telefono";
            // 
            // cxbNombreProveedor
            // 
            cxbNombreProveedor.FormattingEnabled = true;
            cxbNombreProveedor.Location = new Point(141, 32);
            cxbNombreProveedor.Name = "cxbNombreProveedor";
            cxbNombreProveedor.Size = new Size(250, 23);
            cxbNombreProveedor.TabIndex = 30;
            cxbNombreProveedor.SelectedIndexChanged += cxbNombreProveedor_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(6, 74);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 25;
            label2.Text = "Email";
            // 
            // lblBuscarProveedor
            // 
            lblBuscarProveedor.AutoSize = true;
            lblBuscarProveedor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblBuscarProveedor.Location = new Point(6, 35);
            lblBuscarProveedor.Name = "lblBuscarProveedor";
            lblBuscarProveedor.Size = new Size(66, 20);
            lblBuscarProveedor.TabIndex = 29;
            lblBuscarProveedor.Text = "Nombre";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(6, 121);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(95, 20);
            lblRazonSocial.TabIndex = 17;
            lblRazonSocial.Text = "Razon Social";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(141, 71);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(250, 23);
            txtEmail.TabIndex = 21;
            // 
            // txtProvRazonSocial
            // 
            txtProvRazonSocial.Location = new Point(141, 118);
            txtProvRazonSocial.Name = "txtProvRazonSocial";
            txtProvRazonSocial.ReadOnly = true;
            txtProvRazonSocial.Size = new Size(250, 23);
            txtProvRazonSocial.TabIndex = 22;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblProveedor.Location = new Point(0, 0);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(81, 20);
            lblProveedor.TabIndex = 24;
            lblProveedor.Text = "Proveedor";
            // 
            // btnExportar
            // 
            btnExportar.BackColor = Color.Azure;
            btnExportar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnExportar.Location = new Point(164, 258);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(92, 38);
            btnExportar.TabIndex = 39;
            btnExportar.Text = "Exportar";
            btnExportar.UseVisualStyleBackColor = false;
            btnExportar.Click += btnExportar_Click;
            // 
            // nupPrecio
            // 
            nupPrecio.DecimalPlaces = 2;
            nupPrecio.Location = new Point(545, 310);
            nupPrecio.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            nupPrecio.Name = "nupPrecio";
            nupPrecio.Size = new Size(222, 23);
            nupPrecio.TabIndex = 35;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCantidad.Location = new Point(427, 313);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(101, 20);
            lblCantidad.TabIndex = 36;
            lblCantidad.Text = "Precio actual:";
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnModificar.Location = new Point(807, 304);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(92, 38);
            btnModificar.TabIndex = 42;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // fmsCatalogoCostoProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1156, 500);
            Controls.Add(btnModificar);
            Controls.Add(Producto);
            Controls.Add(gbProveedor);
            Controls.Add(btnExportar);
            Controls.Add(nupPrecio);
            Controls.Add(lblCantidad);
            Name = "fmsCatalogoCostoProducto";
            Text = "Catalogo de Costo";
            Load += fmsCatalogoCostoProducto_Load;
            Producto.ResumeLayout(false);
            Producto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).EndInit();
            gbProveedor.ResumeLayout(false);
            gbProveedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nupPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox Producto;
        private Label label6;
        private DataGridView dgvProducto;
        private GroupBox gbProveedor;
        private TextBox txtTelefono;
        private Label label3;
        private ComboBox cxbNombreProveedor;
        private Label label2;
        private Label lblBuscarProveedor;
        private Label lblRazonSocial;
        private TextBox txtEmail;
        private TextBox txtProvRazonSocial;
        private Label lblProveedor;
        private Button btnExportar;
        private NumericUpDown nupPrecio;
        private Label lblCantidad;
        private Button btnModificar;
    }
}