namespace UI
{
    partial class fmsRecuperarContraseña
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
            txtPassword = new TextBox();
            btnAtras = new Button();
            txtEmail = new TextBox();
            lblPassword = new Label();
            lblEmail = new Label();
            btnCambiar = new Button();
            txtConfirmarPassword = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(174, 70);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(250, 23);
            txtPassword.TabIndex = 26;
            // 
            // btnAtras
            // 
            btnAtras.BackColor = Color.Azure;
            btnAtras.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAtras.Location = new Point(377, 248);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(73, 33);
            btnAtras.TabIndex = 24;
            btnAtras.Text = "Atras";
            btnAtras.UseVisualStyleBackColor = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(174, 22);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 23);
            txtEmail.TabIndex = 23;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblPassword.Location = new Point(9, 73);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(133, 20);
            lblPassword.TabIndex = 19;
            lblPassword.Text = "Nueva contraseña";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            lblEmail.Location = new Point(9, 25);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 18;
            lblEmail.Text = "Email";
            // 
            // btnCambiar
            // 
            btnCambiar.BackColor = Color.Azure;
            btnCambiar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCambiar.Location = new Point(165, 218);
            btnCambiar.Name = "btnCambiar";
            btnCambiar.Size = new Size(86, 37);
            btnCambiar.TabIndex = 15;
            btnCambiar.Text = "Cambiar";
            btnCambiar.UseVisualStyleBackColor = false;
            btnCambiar.Click += btnCambiar_Click;
            // 
            // txtConfirmarPassword
            // 
            txtConfirmarPassword.Location = new Point(174, 120);
            txtConfirmarPassword.Name = "txtConfirmarPassword";
            txtConfirmarPassword.Size = new Size(250, 23);
            txtConfirmarPassword.TabIndex = 28;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(9, 119);
            label1.Name = "label1";
            label1.Size = new Size(157, 20);
            label1.TabIndex = 27;
            label1.Text = "Confirmar contraseña";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(txtConfirmarPassword);
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lblPassword);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Location = new Point(12, 48);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(438, 164);
            groupBox1.TabIndex = 29;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(158, 20);
            label2.TabIndex = 29;
            label2.Text = "Recuperar contraseña";
            // 
            // fmsRecuperarContraseña
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(460, 292);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(btnAtras);
            Controls.Add(btnCambiar);
            Name = "fmsRecuperarContraseña";
            Text = "fmsRecuperarContraseña";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPassword;
        private Button btnAtras;
        private TextBox txtEmail;
        private Label lblPassword;
        private Label lblEmail;
        private Button btnCambiar;
        private TextBox txtConfirmarPassword;
        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
    }
}