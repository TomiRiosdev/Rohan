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
            btnMermaAlerta = new Button();
            btnHistorial = new Button();
            btnVerInventario = new Button();
            label1 = new Label();
            panelContenedor = new Panel();
            SuspendLayout();
            // 
            // btnAgregarManual
            // 
            btnAgregarManual.BackColor = Color.Azure;
            btnAgregarManual.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarManual.Location = new Point(122, 52);
            btnAgregarManual.Name = "btnAgregarManual";
            btnAgregarManual.Size = new Size(104, 49);
            btnAgregarManual.TabIndex = 14;
            btnAgregarManual.Text = "Agregar manual";
            btnAgregarManual.UseVisualStyleBackColor = false;
            btnAgregarManual.Click += btnAgregarManual_Click;
            // 
            // btnAgregarPorOC
            // 
            btnAgregarPorOC.BackColor = Color.Azure;
            btnAgregarPorOC.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnAgregarPorOC.Location = new Point(232, 52);
            btnAgregarPorOC.Name = "btnAgregarPorOC";
            btnAgregarPorOC.Size = new Size(104, 49);
            btnAgregarPorOC.TabIndex = 15;
            btnAgregarPorOC.Text = " Agregar por OC";
            btnAgregarPorOC.UseVisualStyleBackColor = false;
            btnAgregarPorOC.Click += btnAgregarPorOC_Click;
            // 
            // btnSolicitudPedido
            // 
            btnSolicitudPedido.BackColor = Color.Azure;
            btnSolicitudPedido.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnSolicitudPedido.Location = new Point(342, 52);
            btnSolicitudPedido.Name = "btnSolicitudPedido";
            btnSolicitudPedido.Size = new Size(104, 49);
            btnSolicitudPedido.TabIndex = 16;
            btnSolicitudPedido.Text = "Solicitud de pedido";
            btnSolicitudPedido.UseVisualStyleBackColor = false;
            btnSolicitudPedido.Click += btnSolicitudPedido_Click;
            // 
            // btnMermaAlerta
            // 
            btnMermaAlerta.BackColor = Color.Azure;
            btnMermaAlerta.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnMermaAlerta.Location = new Point(452, 52);
            btnMermaAlerta.Name = "btnMermaAlerta";
            btnMermaAlerta.Size = new Size(104, 49);
            btnMermaAlerta.TabIndex = 17;
            btnMermaAlerta.Text = "Merma \r\nAlerta";
            btnMermaAlerta.UseVisualStyleBackColor = false;
            btnMermaAlerta.Click += btnMermaAlerta_Click;
            // 
            // btnHistorial
            // 
            btnHistorial.BackColor = Color.Azure;
            btnHistorial.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnHistorial.Location = new Point(562, 52);
            btnHistorial.Name = "btnHistorial";
            btnHistorial.Size = new Size(104, 49);
            btnHistorial.TabIndex = 18;
            btnHistorial.Text = "Historial";
            btnHistorial.UseVisualStyleBackColor = false;
            btnHistorial.Click += btnHistorial_Click;
            // 
            // btnVerInventario
            // 
            btnVerInventario.BackColor = Color.Azure;
            btnVerInventario.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnVerInventario.Location = new Point(12, 52);
            btnVerInventario.Name = "btnVerInventario";
            btnVerInventario.Size = new Size(104, 49);
            btnVerInventario.TabIndex = 19;
            btnVerInventario.Text = "Ver inventario";
            btnVerInventario.UseVisualStyleBackColor = false;
            btnVerInventario.Click += btnVerInventario_Click;
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
            panelContenedor.Size = new Size(1162, 519);
            panelContenedor.TabIndex = 21;
            // 
            // fmsGestionStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1175, 638);
            Controls.Add(panelContenedor);
            Controls.Add(label1);
            Controls.Add(btnVerInventario);
            Controls.Add(btnHistorial);
            Controls.Add(btnMermaAlerta);
            Controls.Add(btnSolicitudPedido);
            Controls.Add(btnAgregarPorOC);
            Controls.Add(btnAgregarManual);
            Name = "fmsGestionStock";
            Text = "Gestion Stock";
            Load += fmsGestionStock_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregarManual;
        private Button btnAgregarPorOC;
        private Button btnSolicitudPedido;
        private Button btnMermaAlerta;
        private Button btnHistorial;
        private Button btnVerInventario;
        private Label label1;
        private Panel panelContenedor;
    }
}