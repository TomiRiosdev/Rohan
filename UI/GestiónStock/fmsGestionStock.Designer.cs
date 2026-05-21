namespace UI.GestiónStock
{
    partial class fmsGestionStock
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
            btnAgregarManual = new Button();
            btnAgregarPorOC = new Button();
            btnSolicitudPedido = new Button();
            btnParametros = new Button();
            btnActualizar = new Button();
            button5 = new Button();
            label1 = new Label();
            panelContenedor = new Panel();
            SuspendLayout();
            // 
            // btnAgregarManual
            // 
            btnAgregarManual.BackColor = Color.Azure;
            btnAgregarManual.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarManual.Location = new Point(12, 52);
            btnAgregarManual.Name = "btnAgregarManual";
            btnAgregarManual.Size = new Size(104, 49);
            btnAgregarManual.TabIndex = 14;
            btnAgregarManual.Text = "Agregar manual";
            btnAgregarManual.UseVisualStyleBackColor = false;
            // 
            // btnAgregarPorOC
            // 
            btnAgregarPorOC.BackColor = Color.Azure;
            btnAgregarPorOC.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarPorOC.Location = new Point(122, 52);
            btnAgregarPorOC.Name = "btnAgregarPorOC";
            btnAgregarPorOC.Size = new Size(104, 49);
            btnAgregarPorOC.TabIndex = 15;
            btnAgregarPorOC.Text = " Agregar por OC";
            btnAgregarPorOC.UseVisualStyleBackColor = false;
            // 
            // btnSolicitudPedido
            // 
            btnSolicitudPedido.BackColor = Color.Azure;
            btnSolicitudPedido.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnSolicitudPedido.Location = new Point(232, 52);
            btnSolicitudPedido.Name = "btnSolicitudPedido";
            btnSolicitudPedido.Size = new Size(104, 49);
            btnSolicitudPedido.TabIndex = 16;
            btnSolicitudPedido.Text = "Solicitud de pedido";
            btnSolicitudPedido.UseVisualStyleBackColor = false;
            // 
            // btnParametros
            // 
            btnParametros.BackColor = Color.Azure;
            btnParametros.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnParametros.Location = new Point(342, 52);
            btnParametros.Name = "btnParametros";
            btnParametros.Size = new Size(104, 49);
            btnParametros.TabIndex = 17;
            btnParametros.Text = "Parametros";
            btnParametros.UseVisualStyleBackColor = false;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.Azure;
            btnActualizar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnActualizar.Location = new Point(562, 52);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(104, 49);
            btnActualizar.TabIndex = 18;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = Color.Azure;
            button5.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            button5.Location = new Point(452, 52);
            button5.Name = "button5";
            button5.Size = new Size(104, 49);
            button5.TabIndex = 19;
            button5.Text = "Agregar ";
            button5.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(173, 30);
            label1.TabIndex = 20;
            label1.Text = "Gestión de stock";
            // 
            // panelContenedor
            // 
            panelContenedor.Location = new Point(1, 107);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1108, 589);
            panelContenedor.TabIndex = 21;
            // 
            // fmsGestionStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1111, 700);
            Controls.Add(panelContenedor);
            Controls.Add(label1);
            Controls.Add(button5);
            Controls.Add(btnActualizar);
            Controls.Add(btnParametros);
            Controls.Add(btnSolicitudPedido);
            Controls.Add(btnAgregarPorOC);
            Controls.Add(btnAgregarManual);
            Name = "fmsGestionStock";
            Text = "Gestion Stock";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregarManual;
        private Button btnAgregarPorOC;
        private Button btnSolicitudPedido;
        private Button btnParametros;
        private Button btnActualizar;
        private Button button5;
        private Label label1;
        private Panel panelContenedor;
    }
}