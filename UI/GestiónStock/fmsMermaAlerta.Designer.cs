namespace UI.GestiónStock
{
    partial class fmsMermaAlerta
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
            txtProducto = new TextBox();
            txtSku = new TextBox();
            label1 = new Label();
            label2 = new Label();
            nudMinimo = new NumericUpDown();
            nudMaximo = new NumericUpDown();
            nudVidaUtil = new NumericUpDown();
            nudDiasAlerta = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            groupBox1 = new GroupBox();
            label7 = new Label();
            btnAceptar = new Button();
            btnAtras = new Button();
            ((System.ComponentModel.ISupportInitialize)nudMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaximo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudVidaUtil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiasAlerta).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(200, 33);
            txtProducto.Name = "txtProducto";
            txtProducto.Size = new Size(210, 23);
            txtProducto.TabIndex = 8;
            // 
            // txtSku
            // 
            txtSku.Location = new Point(290, 82);
            txtSku.Name = "txtSku";
            txtSku.Size = new Size(120, 23);
            txtSku.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(19, 36);
            label1.Name = "label1";
            label1.Size = new Size(66, 20);
            label1.TabIndex = 10;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(19, 85);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 11;
            label2.Text = "Código SKU";
            // 
            // nudMinimo
            // 
            nudMinimo.Location = new Point(290, 130);
            nudMinimo.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudMinimo.Name = "nudMinimo";
            nudMinimo.Size = new Size(120, 23);
            nudMinimo.TabIndex = 12;
            // 
            // nudMaximo
            // 
            nudMaximo.Location = new Point(290, 177);
            nudMaximo.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudMaximo.Name = "nudMaximo";
            nudMaximo.Size = new Size(120, 23);
            nudMaximo.TabIndex = 13;
            // 
            // nudVidaUtil
            // 
            nudVidaUtil.Location = new Point(290, 230);
            nudVidaUtil.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudVidaUtil.Name = "nudVidaUtil";
            nudVidaUtil.Size = new Size(120, 23);
            nudVidaUtil.TabIndex = 14;
            // 
            // nudDiasAlerta
            // 
            nudDiasAlerta.Location = new Point(290, 279);
            nudDiasAlerta.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudDiasAlerta.Name = "nudDiasAlerta";
            nudDiasAlerta.Size = new Size(120, 23);
            nudDiasAlerta.TabIndex = 15;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label3.Location = new Point(19, 133);
            label3.Name = "label3";
            label3.Size = new Size(139, 20);
            label3.TabIndex = 16;
            label3.Text = "Minímo stock (Un.)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label4.Location = new Point(19, 177);
            label4.Name = "label4";
            label4.Size = new Size(142, 20);
            label4.TabIndex = 17;
            label4.Text = "Maxímo stock (Un.)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label5.Location = new Point(19, 230);
            label5.Name = "label5";
            label5.Size = new Size(107, 20);
            label5.TabIndex = 18;
            label5.Text = "Vida útil (días)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label6.Location = new Point(19, 282);
            label6.Name = "label6";
            label6.Size = new Size(175, 20);
            label6.TabIndex = 19;
            label6.Text = "Alerta vencimiento(días)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtSku);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtProducto);
            groupBox1.Controls.Add(nudDiasAlerta);
            groupBox1.Controls.Add(nudMinimo);
            groupBox1.Controls.Add(nudVidaUtil);
            groupBox1.Controls.Add(nudMaximo);
            groupBox1.Location = new Point(12, 34);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(432, 331);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label7.Location = new Point(108, 11);
            label7.Name = "label7";
            label7.Size = new Size(214, 20);
            label7.TabIndex = 20;
            label7.Text = "Configuración Merma / Alerta";
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.Azure;
            btnAceptar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAceptar.Location = new Point(172, 371);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(92, 38);
            btnAceptar.TabIndex = 21;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnAtras.Location = new Point(365, 417);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(83, 30);
            btnAtras.TabIndex = 22;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // fmsMermaAlerta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(460, 459);
            Controls.Add(btnAtras);
            Controls.Add(btnAceptar);
            Controls.Add(label7);
            Controls.Add(groupBox1);
            Name = "fmsMermaAlerta";
            Text = "Configuración Merma / Alerta";
            Load += fmsMermaAlerta_Load;
            ((System.ComponentModel.ISupportInitialize)nudMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaximo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudVidaUtil).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiasAlerta).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtProducto;
        private TextBox txtSku;
        private Label label1;
        private Label label2;
        private NumericUpDown nudMinimo;
        private NumericUpDown nudMaximo;
        private NumericUpDown nudVidaUtil;
        private NumericUpDown nudDiasAlerta;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private GroupBox groupBox1;
        private Label label7;
        private Button btnAceptar;
        private Button btnAtras;
    }
}