namespace UI.GestiónProducto
{
    partial class fmsModificarProducto
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
            btnModificar = new Button();
            btnAtras = new Button();
            txtNombre = new TextBox();
            txtCodigoSku = new TextBox();
            txtContenidoVenta = new TextBox();
            cbxCategoria = new ComboBox();
            cbxUnidadMedida = new ComboBox();
            Nombre = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label6 = new Label();
            txtDescripcion = new TextBox();
            label5 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.Azure;
            btnModificar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnModificar.Location = new Point(172, 358);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(88, 38);
            btnModificar.TabIndex = 0;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAtras.Location = new Point(381, 470);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(80, 28);
            btnAtras.TabIndex = 1;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(172, 44);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(266, 27);
            txtNombre.TabIndex = 2;
            // 
            // txtCodigoSku
            // 
            txtCodigoSku.Location = new Point(172, 93);
            txtCodigoSku.Name = "txtCodigoSku";
            txtCodigoSku.Size = new Size(266, 27);
            txtCodigoSku.TabIndex = 3;
            // 
            // txtContenidoVenta
            // 
            txtContenidoVenta.Location = new Point(172, 243);
            txtContenidoVenta.Name = "txtContenidoVenta";
            txtContenidoVenta.Size = new Size(266, 27);
            txtContenidoVenta.TabIndex = 5;
            // 
            // cbxCategoria
            // 
            cbxCategoria.FormattingEnabled = true;
            cbxCategoria.Location = new Point(172, 145);
            cbxCategoria.Name = "cbxCategoria";
            cbxCategoria.Size = new Size(266, 28);
            cbxCategoria.TabIndex = 6;
            // 
            // cbxUnidadMedida
            // 
            cbxUnidadMedida.FormattingEnabled = true;
            cbxUnidadMedida.Location = new Point(172, 192);
            cbxUnidadMedida.Name = "cbxUnidadMedida";
            cbxUnidadMedida.Size = new Size(266, 28);
            cbxUnidadMedida.TabIndex = 7;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            Nombre.Location = new Point(6, 51);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(66, 20);
            Nombre.TabIndex = 8;
            Nombre.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(6, 100);
            label1.Name = "label1";
            label1.Size = new Size(91, 20);
            label1.TabIndex = 9;
            label1.Text = "Codigo  Sku";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(6, 153);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 10;
            label2.Text = "Categoria";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(6, 200);
            label3.Name = "label3";
            label3.Size = new Size(115, 20);
            label3.TabIndex = 11;
            label3.Text = "Unidad Medida";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label4.Location = new Point(6, 304);
            label4.Name = "label4";
            label4.Size = new Size(89, 20);
            label4.TabIndex = 12;
            label4.Text = "Descripción";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label6.Location = new Point(142, 9);
            label6.Name = "label6";
            label6.Size = new Size(142, 20);
            label6.TabIndex = 14;
            label6.Text = "Modificar Producto";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(172, 297);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(266, 27);
            txtDescripcion.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label5.Location = new Point(6, 250);
            label5.Name = "label5";
            label5.Size = new Size(150, 20);
            label5.TabIndex = 13;
            label5.Text = "Contenido por venta";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtCodigoSku);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtDescripcion);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtContenidoVenta);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cbxCategoria);
            groupBox1.Controls.Add(Nombre);
            groupBox1.Controls.Add(cbxUnidadMedida);
            groupBox1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(449, 402);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del producto";
            // 
            // fmsModificarProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(475, 525);
            Controls.Add(groupBox1);
            Controls.Add(label6);
            Controls.Add(btnAtras);
            Name = "fmsModificarProducto";
            Text = "ModificarProducto";
            Load += ModificarProducto_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnModificar;
        private Button btnAtras;
        private TextBox txtNombre;
        private TextBox txtCodigoSku;
        private TextBox txtContenidoVenta;
        private ComboBox cbxCategoria;
        private ComboBox cbxUnidadMedida;
        private Label Nombre;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label6;
        private TextBox txtDescripcion;
        private Label label5;
        private GroupBox groupBox1;
    }
}