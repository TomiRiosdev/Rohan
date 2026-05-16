namespace UI
{
    partial class fmsSeleccionarSucursal
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
            cbxSucursal = new ComboBox();
            Sucursal = new Label();
            btnIngresar = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cbxSucursal
            // 
            cbxSucursal.FormattingEnabled = true;
            cbxSucursal.Location = new Point(100, 29);
            cbxSucursal.Name = "cbxSucursal";
            cbxSucursal.Size = new Size(248, 23);
            cbxSucursal.TabIndex = 0;
            // 
            // Sucursal
            // 
            Sucursal.AutoSize = true;
            Sucursal.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            Sucursal.Location = new Point(17, 32);
            Sucursal.Name = "Sucursal";
            Sucursal.Size = new Size(66, 20);
            Sucursal.TabIndex = 1;
            Sucursal.Text = "Sucursal";
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.Azure;
            btnIngresar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnIngresar.Location = new Point(158, 158);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(87, 36);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(125, 22);
            label1.Name = "label1";
            label1.Size = new Size(165, 21);
            label1.TabIndex = 3;
            label1.Text = "Selección de sucursal";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbxSucursal);
            groupBox1.Controls.Add(Sucursal);
            groupBox1.Location = new Point(12, 66);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(374, 77);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // fmsSeleccionarSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(412, 229);
            Controls.Add(btnIngresar);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "fmsSeleccionarSucursal";
            Text = "Selección de sucursal";
            Load += fmsSeleccionarSucursal_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbxSucursal;
        private Label Sucursal;
        private Button btnIngresar;
        private Label label1;
        private GroupBox groupBox1;
    }
}