namespace UI.GestionCompra
{
    partial class fmsModificarOrdenCompra
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            lblPrecio = new Label();
            lblSubtotal = new Label();
            Producto = new GroupBox();
            label6 = new Label();
            dgvProducto = new DataGridView();
            groupBox2 = new GroupBox();
            txtNroOrden = new TextBox();
            label3 = new Label();
            label2 = new Label();
            lblRazonSocial = new Label();
            txtCuil = new TextBox();
            txtProvRazonSocial = new TextBox();
            lblProveedor = new Label();
            btnAgregar = new Button();
            groupBox1 = new GroupBox();
            label4 = new Label();
            dgvPreOrdenCompra = new DataGridView();
            label1 = new Label();
            btnCerrar = new Button();
            lblCantidadBulto = new Label();
            btnConfirmar = new Button();
            nupCantidad = new NumericUpDown();
            lblCantidad = new Label();
            Producto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).BeginInit();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreOrdenCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nupCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblPrecio.Location = new Point(709, 282);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(52, 20);
            lblPrecio.TabIndex = 50;
            lblPrecio.Text = "Precio";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblSubtotal.Location = new Point(691, 546);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(70, 20);
            lblSubtotal.TabIndex = 48;
            lblSubtotal.Text = "Subtotal:";
            // 
            // Producto
            // 
            Producto.Controls.Add(label6);
            Producto.Controls.Add(dgvProducto);
            Producto.Font = new Font("Segoe UI", 9F);
            Producto.Location = new Point(436, 52);
            Producto.Name = "Producto";
            Producto.Size = new Size(453, 218);
            Producto.TabIndex = 47;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvProducto.DefaultCellStyle = dataGridViewCellStyle2;
            dgvProducto.Location = new Point(6, 26);
            dgvProducto.Name = "dgvProducto";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvProducto.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvProducto.Size = new Size(436, 177);
            dgvProducto.TabIndex = 33;
            dgvProducto.SelectionChanged += dgvProducto_SelectionChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtNroOrden);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(lblRazonSocial);
            groupBox2.Controls.Add(txtCuil);
            groupBox2.Controls.Add(txtProvRazonSocial);
            groupBox2.Controls.Add(lblProveedor);
            groupBox2.Location = new Point(21, 52);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(409, 166);
            groupBox2.TabIndex = 46;
            groupBox2.TabStop = false;
            // 
            // txtNroOrden
            // 
            txtNroOrden.Location = new Point(141, 127);
            txtNroOrden.Name = "txtNroOrden";
            txtNroOrden.ReadOnly = true;
            txtNroOrden.Size = new Size(250, 23);
            txtNroOrden.TabIndex = 32;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(6, 130);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 31;
            label3.Text = "Nro Orden";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(6, 35);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 25;
            label2.Text = "C.U.I.L";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblRazonSocial.Location = new Point(6, 82);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(95, 20);
            lblRazonSocial.TabIndex = 17;
            lblRazonSocial.Text = "Razon Social";
            // 
            // txtCuil
            // 
            txtCuil.Location = new Point(141, 32);
            txtCuil.Name = "txtCuil";
            txtCuil.ReadOnly = true;
            txtCuil.Size = new Size(250, 23);
            txtCuil.TabIndex = 21;
            // 
            // txtProvRazonSocial
            // 
            txtProvRazonSocial.Location = new Point(141, 79);
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
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAgregar.Location = new Point(338, 239);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(92, 38);
            btnAgregar.TabIndex = 45;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dgvPreOrdenCompra);
            groupBox1.Font = new Font("Segoe UI", 9F);
            groupBox1.Location = new Point(14, 348);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(871, 195);
            groupBox1.TabIndex = 44;
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvPreOrdenCompra.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvPreOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvPreOrdenCompra.DefaultCellStyle = dataGridViewCellStyle5;
            dgvPreOrdenCompra.Location = new Point(7, 23);
            dgvPreOrdenCompra.Name = "dgvPreOrdenCompra";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvPreOrdenCompra.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvPreOrdenCompra.Size = new Size(857, 155);
            dgvPreOrdenCompra.TabIndex = 6;
            dgvPreOrdenCompra.CellContentClick += dgvPreOrdenCompra_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(249, 11);
            label1.Name = "label1";
            label1.Size = new Size(131, 20);
            label1.TabIndex = 43;
            label1.Text = "Orden de Compra";
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Azure;
            btnCerrar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCerrar.Location = new Point(793, 596);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(92, 38);
            btnCerrar.TabIndex = 42;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblCantidadBulto
            // 
            lblCantidadBulto.AutoSize = true;
            lblCantidadBulto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCantidadBulto.Location = new Point(436, 282);
            lblCantidadBulto.Name = "lblCantidadBulto";
            lblCantidadBulto.Size = new Size(98, 20);
            lblCantidadBulto.TabIndex = 41;
            lblCantidadBulto.Text = "Pack cerrado";
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.Azure;
            btnConfirmar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnConfirmar.Location = new Point(20, 549);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(102, 51);
            btnConfirmar.TabIndex = 40;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // nupCantidad
            // 
            nupCantidad.Location = new Point(135, 247);
            nupCantidad.Name = "nupCantidad";
            nupCantidad.Size = new Size(178, 23);
            nupCantidad.TabIndex = 38;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblCantidad.Location = new Point(28, 248);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(74, 20);
            lblCantidad.TabIndex = 39;
            lblCantidad.Text = "Cantidad:";
            // 
            // fmsModificarOrdenCompra
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(902, 644);
            Controls.Add(lblPrecio);
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
            Name = "fmsModificarOrdenCompra";
            Text = "Modificar Orden Compra";
            Load += fmsModificarOrdenCompra_Load;
            Producto.ResumeLayout(false);
            Producto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreOrdenCompra).EndInit();
            ((System.ComponentModel.ISupportInitialize)nupCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPrecio;
        private Label lblSubtotal;
        private GroupBox Producto;
        private Label label6;
        private DataGridView dgvProducto;
        private GroupBox groupBox2;
        private TextBox txtNroOrden;
        private Label label3;
        private Label label2;
        private Label lblRazonSocial;
        private TextBox txtCuil;
        private TextBox txtProvRazonSocial;
        private Label lblProveedor;
        private Button btnAgregar;
        private GroupBox groupBox1;
        private Label label4;
        private DataGridView dgvPreOrdenCompra;
        private Label label1;
        private Button btnCerrar;
        private Label lblCantidadBulto;
        private Button btnConfirmar;
        private NumericUpDown nupCantidad;
        private Label lblCantidad;
    }
}