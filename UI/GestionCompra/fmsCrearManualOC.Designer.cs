namespace UI.GestionCompra
{
    partial class fmsCrearManualOC
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            btnCerrar = new Button();
            lblCantidadBulto = new Label();
            nupCantidad = new NumericUpDown();
            lblCantidad = new Label();
            btnConfirmar = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label4 = new Label();
            dgvPreOrdenCompra = new DataGridView();
            btnAgregar = new Button();
            groupBox2 = new GroupBox();
            txtTelefono = new TextBox();
            label3 = new Label();
            cxbNombreProveedor = new ComboBox();
            label2 = new Label();
            lblBuscarProveedor = new Label();
            lblRazonSocial = new Label();
            txtEmail = new TextBox();
            txtProvRazonSocial = new TextBox();
            lblProveedor = new Label();
            dgvProducto = new DataGridView();
            Producto = new GroupBox();
            label6 = new Label();
            lblSubtotal = new Label();
            btnEliminar = new Button();
            lblPrecio = new Label();
            ((System.ComponentModel.ISupportInitialize)nupCantidad).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreOrdenCompra).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).BeginInit();
            Producto.SuspendLayout();
            SuspendLayout();
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Azure;
            btnCerrar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCerrar.Location = new Point(798, 594);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(92, 38);
            btnCerrar.TabIndex = 21;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblCantidadBulto
            // 
            lblCantidadBulto.AutoSize = true;
            lblCantidadBulto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCantidadBulto.Location = new Point(615, 267);
            lblCantidadBulto.Name = "lblCantidadBulto";
            lblCantidadBulto.Size = new Size(98, 20);
            lblCantidadBulto.TabIndex = 16;
            lblCantidadBulto.Text = "Pack cerrado";
            // 
            // nupCantidad
            // 
            nupCantidad.Location = new Point(138, 303);
            nupCantidad.Name = "nupCantidad";
            nupCantidad.Size = new Size(178, 23);
            nupCantidad.TabIndex = 0;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCantidad.Location = new Point(33, 305);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(74, 20);
            lblCantidad.TabIndex = 8;
            lblCantidad.Text = "Cantidad:";
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.Azure;
            btnConfirmar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnConfirmar.Location = new Point(25, 560);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(102, 52);
            btnConfirmar.TabIndex = 10;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(254, 9);
            label1.Name = "label1";
            label1.Size = new Size(131, 20);
            label1.TabIndex = 23;
            label1.Text = "Orden de Compra";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dgvPreOrdenCompra);
            groupBox1.Font = new Font("Segoe UI", 9F);
            groupBox1.Location = new Point(19, 346);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(864, 195);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label4.Location = new Point(6, 1);
            label4.Name = "label4";
            label4.Size = new Size(144, 19);
            label4.TabIndex = 17;
            label4.Text = "Pre Orden de Compra";
            // 
            // dgvPreOrdenCompra
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPreOrdenCompra.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvPreOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvPreOrdenCompra.DefaultCellStyle = dataGridViewCellStyle2;
            dgvPreOrdenCompra.Location = new Point(7, 23);
            dgvPreOrdenCompra.Name = "dgvPreOrdenCompra";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvPreOrdenCompra.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvPreOrdenCompra.Size = new Size(838, 155);
            dgvPreOrdenCompra.TabIndex = 6;
            dgvPreOrdenCompra.CellContentClick += dgvPreOrdenCompra_CellContentClick;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAgregar.Location = new Point(343, 296);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(92, 38);
            btnAgregar.TabIndex = 25;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtTelefono);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cxbNombreProveedor);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(lblBuscarProveedor);
            groupBox2.Controls.Add(lblRazonSocial);
            groupBox2.Controls.Add(txtEmail);
            groupBox2.Controls.Add(txtProvRazonSocial);
            groupBox2.Controls.Add(lblProveedor);
            groupBox2.Location = new Point(26, 50);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(409, 214);
            groupBox2.TabIndex = 28;
            groupBox2.TabStop = false;
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
            // dgvProducto
            // 
            dgvProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducto.Location = new Point(6, 26);
            dgvProducto.Name = "dgvProducto";
            dgvProducto.Size = new Size(436, 177);
            dgvProducto.TabIndex = 33;
            dgvProducto.SelectionChanged += dgvProducto_SelectionChanged;
            // 
            // Producto
            // 
            Producto.Controls.Add(label6);
            Producto.Controls.Add(dgvProducto);
            Producto.Font = new Font("Segoe UI", 9F);
            Producto.Location = new Point(441, 50);
            Producto.Name = "Producto";
            Producto.Size = new Size(453, 214);
            Producto.TabIndex = 34;
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
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblSubtotal.Location = new Point(696, 544);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(70, 20);
            lblSubtotal.TabIndex = 35;
            lblSubtotal.Text = "Subtotal:";
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Azure;
            btnEliminar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnEliminar.Location = new Point(396, 547);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(92, 38);
            btnEliminar.TabIndex = 36;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblPrecio.Location = new Point(615, 296);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(52, 20);
            lblPrecio.TabIndex = 37;
            lblPrecio.Text = "Precio";
            // 
            // fmsCrearManualOC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(902, 644);
            Controls.Add(lblPrecio);
            Controls.Add(btnEliminar);
            Controls.Add(lblSubtotal);
            Controls.Add(Producto);
            Controls.Add(groupBox2);
            Controls.Add(btnAgregar);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(btnCerrar);
            Controls.Add(lblCantidadBulto);
            Controls.Add(btnConfirmar);
            Controls.Add(nupCantidad);
            Controls.Add(lblCantidad);
            Name = "fmsCrearManualOC";
            Text = "Crear Orden de Compra";
            Load += fmsCrearManualOC_Load;
            ((System.ComponentModel.ISupportInitialize)nupCantidad).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreOrdenCompra).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).EndInit();
            Producto.ResumeLayout(false);
            Producto.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCerrar;
        private Label lblCantidadBulto;
        private Button btnConfirmar;
        private NumericUpDown nupCantidad;
        private Label lblCantidad;
        private Label label1;
        private GroupBox groupBox1;
        private Label label4;
        private DataGridView dgvPreOrdenCompra;
        private Button btnAgregar;
        private GroupBox groupBox2;
        private Label lblRazonSocial;
        private TextBox txtEmail;
        private TextBox txtProvRazonSocial;
        private Label lblProveedor;
        private Label lblBuscarProveedor;
        private ComboBox cxbNombreProveedor;
        private Label label2;
        private DataGridView dgvProducto;
        private GroupBox Producto;
        private TextBox txtTelefono;
        private Label label3;
        private Label lblSubtotal;
        private Label label6;
        private Button btnEliminar;
        private Label lblPrecio;
    }
}