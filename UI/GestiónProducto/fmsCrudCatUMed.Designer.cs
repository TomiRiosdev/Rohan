namespace UI.GestiónProducto
{
    partial class fmsCrudCatUMed
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
            btnAgregarCategoria = new Button();
            btnAgregarUnidadMedida = new Button();
            txtCategoria = new TextBox();
            txtUnidadMedida = new TextBox();
            label2 = new Label();
            button1 = new Button();
            label5 = new Label();
            gbCategoria = new GroupBox();
            gbUnidadMedida = new GroupBox();
            label1 = new Label();
            gbCategoria.SuspendLayout();
            gbUnidadMedida.SuspendLayout();
            SuspendLayout();
            // 
            // btnAgregarCategoria
            // 
            btnAgregarCategoria.BackColor = Color.Azure;
            btnAgregarCategoria.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAgregarCategoria.Location = new Point(321, 40);
            btnAgregarCategoria.Name = "btnAgregarCategoria";
            btnAgregarCategoria.Size = new Size(78, 27);
            btnAgregarCategoria.TabIndex = 0;
            btnAgregarCategoria.Text = "Agregar";
            btnAgregarCategoria.UseVisualStyleBackColor = false;
            btnAgregarCategoria.Click += btnAgregarCategoria_Click;
            // 
            // btnAgregarUnidadMedida
            // 
            btnAgregarUnidadMedida.BackColor = Color.Azure;
            btnAgregarUnidadMedida.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAgregarUnidadMedida.Location = new Point(318, 39);
            btnAgregarUnidadMedida.Name = "btnAgregarUnidadMedida";
            btnAgregarUnidadMedida.Size = new Size(81, 28);
            btnAgregarUnidadMedida.TabIndex = 1;
            btnAgregarUnidadMedida.Text = "Agregar";
            btnAgregarUnidadMedida.UseVisualStyleBackColor = false;
            btnAgregarUnidadMedida.Click += btnAgregarUnidadMedida_Click;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(81, 40);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.Size = new Size(223, 27);
            txtCategoria.TabIndex = 2;
            // 
            // txtUnidadMedida
            // 
            txtUnidadMedida.Location = new Point(81, 40);
            txtUnidadMedida.Name = "txtUnidadMedida";
            txtUnidadMedida.Size = new Size(223, 27);
            txtUnidadMedida.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(3, 43);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 5;
            label2.Text = "Nombre";
            // 
            // button1
            // 
            button1.BackColor = Color.Azure;
            button1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            button1.Location = new Point(342, 327);
            button1.Name = "button1";
            button1.Size = new Size(75, 32);
            button1.TabIndex = 8;
            button1.Text = "Atras";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label5.Location = new Point(15, 25);
            label5.Name = "label5";
            label5.Size = new Size(260, 20);
            label5.TabIndex = 9;
            label5.Text = "Alta Categorias y Unidad de medidas";
            // 
            // gbCategoria
            // 
            gbCategoria.Controls.Add(label1);
            gbCategoria.Controls.Add(txtCategoria);
            gbCategoria.Controls.Add(btnAgregarCategoria);
            gbCategoria.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            gbCategoria.Location = new Point(12, 69);
            gbCategoria.Name = "gbCategoria";
            gbCategoria.Size = new Size(405, 94);
            gbCategoria.TabIndex = 10;
            gbCategoria.TabStop = false;
            gbCategoria.Text = "Categoria";
            // 
            // gbUnidadMedida
            // 
            gbUnidadMedida.Controls.Add(btnAgregarUnidadMedida);
            gbUnidadMedida.Controls.Add(txtUnidadMedida);
            gbUnidadMedida.Controls.Add(label2);
            gbUnidadMedida.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            gbUnidadMedida.Location = new Point(12, 197);
            gbUnidadMedida.Name = "gbUnidadMedida";
            gbUnidadMedida.Size = new Size(405, 94);
            gbUnidadMedida.TabIndex = 11;
            gbUnidadMedida.TabStop = false;
            gbUnidadMedida.Text = "Unidad Medida";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(6, 47);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 6;
            label1.Text = "Nombre";
            // 
            // fmsCrudCatUMed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(437, 378);
            Controls.Add(gbUnidadMedida);
            Controls.Add(gbCategoria);
            Controls.Add(label5);
            Controls.Add(button1);
            Name = "fmsCrudCatUMed";
            Text = "fmsCrudCatUMed";
            gbCategoria.ResumeLayout(false);
            gbCategoria.PerformLayout();
            gbUnidadMedida.ResumeLayout(false);
            gbUnidadMedida.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregarCategoria;
        private Button btnAgregarUnidadMedida;
        private TextBox txtCategoria;
        private TextBox txtUnidadMedida;
        private Label label2;
        private Button button1;
        private Label label5;
        private GroupBox gbCategoria;
        private GroupBox gbUnidadMedida;
        private Label label1;
    }
}