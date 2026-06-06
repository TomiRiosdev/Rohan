namespace UI.GestiónProducto
{
    partial class fmsCrearProducto
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
            components = new System.ComponentModel.Container();
            button2 = new Button();
            TxtNombre = new TextBox();
            TxtCodigoSku = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            CbxCategoria = new ComboBox();
            CbxUnidadMedida = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            TxtContVenta = new TextBox();
            TxtDescripcion = new TextBox();
            label7 = new Label();
            btnAgregar = new Button();
            toolTip1 = new ToolTip(components);
            groupBox1 = new GroupBox();
            nudCantidadPorEnvase = new NumericUpDown();
            label8 = new Label();
            CbxTipoEnvase = new ComboBox();
            label9 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadPorEnvase).BeginInit();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.Azure;
            button2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            button2.Location = new Point(375, 635);
            button2.Name = "button2";
            button2.Size = new Size(83, 30);
            button2.TabIndex = 1;
            button2.Text = "Atras";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // TxtNombre
            // 
            TxtNombre.Location = new Point(168, 60);
            TxtNombre.Name = "TxtNombre";
            TxtNombre.Size = new Size(262, 27);
            TxtNombre.TabIndex = 2;
            // 
            // TxtCodigoSku
            // 
            TxtCodigoSku.Location = new Point(168, 109);
            TxtCodigoSku.Name = "TxtCodigoSku";
            TxtCodigoSku.Size = new Size(262, 27);
            TxtCodigoSku.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(11, 67);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 6;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(11, 116);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 7;
            label2.Text = "Codigo SKU";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(11, 169);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 8;
            label3.Text = "Categoria";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label4.Location = new Point(11, 222);
            label4.Name = "label4";
            label4.Size = new Size(115, 20);
            label4.TabIndex = 9;
            label4.Text = "Unidad Medida";
            // 
            // CbxCategoria
            // 
            CbxCategoria.FormattingEnabled = true;
            CbxCategoria.Location = new Point(168, 161);
            CbxCategoria.Name = "CbxCategoria";
            CbxCategoria.Size = new Size(262, 28);
            CbxCategoria.TabIndex = 10;
            // 
            // CbxUnidadMedida
            // 
            CbxUnidadMedida.FormattingEnabled = true;
            CbxUnidadMedida.Location = new Point(168, 214);
            CbxUnidadMedida.Name = "CbxUnidadMedida";
            CbxUnidadMedida.Size = new Size(263, 28);
            CbxUnidadMedida.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label5.Location = new Point(11, 319);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 12;
            label5.Text = "Descripción";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label6.Location = new Point(11, 269);
            label6.Name = "label6";
            label6.Size = new Size(150, 20);
            label6.TabIndex = 13;
            label6.Text = "Contenido por venta";
            // 
            // TxtContVenta
            // 
            TxtContVenta.Location = new Point(168, 262);
            TxtContVenta.Name = "TxtContVenta";
            TxtContVenta.Size = new Size(262, 27);
            TxtContVenta.TabIndex = 14;
            // 
            // TxtDescripcion
            // 
            TxtDescripcion.Location = new Point(168, 312);
            TxtDescripcion.Name = "TxtDescripcion";
            TxtDescripcion.Size = new Size(262, 27);
            TxtDescripcion.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label7.Location = new Point(170, 9);
            label7.Name = "label7";
            label7.Size = new Size(113, 20);
            label7.TabIndex = 16;
            label7.Text = "Crear Producto";
            label7.Click += label7_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.Azure;
            btnAgregar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAgregar.Location = new Point(158, 467);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(92, 38);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(nudCantidadPorEnvase);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(CbxTipoEnvase);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(TxtDescripcion);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(TxtNombre);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(TxtContVenta);
            groupBox1.Controls.Add(TxtCodigoSku);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(CbxUnidadMedida);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(CbxCategoria);
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 47);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(446, 548);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del producto";
            // 
            // nudCantidadPorEnvase
            // 
            nudCantidadPorEnvase.Location = new Point(168, 414);
            nudCantidadPorEnvase.Name = "nudCantidadPorEnvase";
            nudCantidadPorEnvase.Size = new Size(262, 27);
            nudCantidadPorEnvase.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label8.Location = new Point(11, 414);
            label8.Name = "label8";
            label8.Size = new Size(74, 20);
            label8.TabIndex = 18;
            label8.Text = "Cantidad ";
            // 
            // CbxTipoEnvase
            // 
            CbxTipoEnvase.FormattingEnabled = true;
            CbxTipoEnvase.Location = new Point(168, 359);
            CbxTipoEnvase.Name = "CbxTipoEnvase";
            CbxTipoEnvase.Size = new Size(263, 28);
            CbxTipoEnvase.TabIndex = 17;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label9.Location = new Point(11, 367);
            label9.Name = "label9";
            label9.Size = new Size(106, 20);
            label9.TabIndex = 16;
            label9.Text = "Envase / Bulto";
            // 
            // fmsCrearProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(469, 677);
            Controls.Add(groupBox1);
            Controls.Add(label7);
            Controls.Add(button2);
            Name = "fmsCrearProducto";
            Text = "CreateProducto";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidadPorEnvase).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button2;
        private TextBox TxtNombre;
        private TextBox TxtCodigoSku;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox CbxCategoria;
        private ComboBox CbxUnidadMedida;
        private Label label5;
        private Label label6;
        private TextBox TxtContVenta;
        private TextBox TxtDescripcion;
        private Label label7;
        private Button btnAgregar;
        private ToolTip toolTip1;
        private GroupBox groupBox1;
        private NumericUpDown nudCantidadPorEnvase;
        private Label label8;
        private ComboBox CbxTipoEnvase;
        private Label label9;
    }
}