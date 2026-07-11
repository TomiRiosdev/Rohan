namespace UI.GestionCompra
{
    partial class fmsGestionCompra
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
            lblAlertaSolicitudes = new Label();
            panelContenedor = new Panel();
            btnSolicitud = new Button();
            btnCatalogoCosto = new Button();
            btnHistorialEstados = new Button();
            btnCargaManualOC = new Button();
            lblGestionCompra = new Label();
            SuspendLayout();
            // 
            // lblAlertaSolicitudes
            // 
            lblAlertaSolicitudes.AutoSize = true;
            lblAlertaSolicitudes.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblAlertaSolicitudes.Location = new Point(725, 9);
            lblAlertaSolicitudes.Name = "lblAlertaSolicitudes";
            lblAlertaSolicitudes.Size = new Size(53, 19);
            lblAlertaSolicitudes.TabIndex = 20;
            lblAlertaSolicitudes.Text = "Estado:";
            // 
            // panelContenedor
            // 
            panelContenedor.Location = new Point(2, 97);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1172, 539);
            panelContenedor.TabIndex = 29;
            // 
            // btnSolicitud
            // 
            btnSolicitud.BackColor = Color.Azure;
            btnSolicitud.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnSolicitud.Location = new Point(12, 42);
            btnSolicitud.Name = "btnSolicitud";
            btnSolicitud.Size = new Size(104, 49);
            btnSolicitud.TabIndex = 27;
            btnSolicitud.Text = "Solicitudes Pendientes\r\n";
            btnSolicitud.UseVisualStyleBackColor = false;
            btnSolicitud.Click += btnSolicitud_Click;
            // 
            // btnCatalogoCosto
            // 
            btnCatalogoCosto.BackColor = Color.Azure;
            btnCatalogoCosto.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCatalogoCosto.Location = new Point(232, 42);
            btnCatalogoCosto.Name = "btnCatalogoCosto";
            btnCatalogoCosto.Size = new Size(104, 49);
            btnCatalogoCosto.TabIndex = 24;
            btnCatalogoCosto.Text = "Catálogo y Costos";
            btnCatalogoCosto.UseVisualStyleBackColor = false;
            btnCatalogoCosto.Click += btnCatalogoCosto_Click;
            // 
            // btnHistorialEstados
            // 
            btnHistorialEstados.BackColor = Color.Azure;
            btnHistorialEstados.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnHistorialEstados.Location = new Point(342, 42);
            btnHistorialEstados.Name = "btnHistorialEstados";
            btnHistorialEstados.Size = new Size(104, 49);
            btnHistorialEstados.TabIndex = 23;
            btnHistorialEstados.Text = "Historial y Estados";
            btnHistorialEstados.UseVisualStyleBackColor = false;
            btnHistorialEstados.Click += btnHistorialEstados_Click;
            // 
            // btnCargaManualOC
            // 
            btnCargaManualOC.BackColor = Color.Azure;
            btnCargaManualOC.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCargaManualOC.Location = new Point(122, 42);
            btnCargaManualOC.Name = "btnCargaManualOC";
            btnCargaManualOC.Size = new Size(104, 49);
            btnCargaManualOC.TabIndex = 22;
            btnCargaManualOC.Text = " Orden de Compra";
            btnCargaManualOC.UseVisualStyleBackColor = false;
            btnCargaManualOC.Click += btnCargaManualOC_Click;
            // 
            // lblGestionCompra
            // 
            lblGestionCompra.AutoSize = true;
            lblGestionCompra.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            lblGestionCompra.Location = new Point(12, 9);
            lblGestionCompra.Name = "lblGestionCompra";
            lblGestionCompra.Size = new Size(208, 30);
            lblGestionCompra.TabIndex = 30;
            lblGestionCompra.Text = "Gestión de Compras";
            // 
            // fmsGestionCompra
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1175, 638);
            Controls.Add(lblGestionCompra);
            Controls.Add(panelContenedor);
            Controls.Add(btnSolicitud);
            Controls.Add(btnCatalogoCosto);
            Controls.Add(btnHistorialEstados);
            Controls.Add(btnCargaManualOC);
            Controls.Add(lblAlertaSolicitudes);
            Name = "fmsGestionCompra";
            Text = " Gestion Compra";
            Load += fmsGestionCompra_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAlertaSolicitudes;
        private Panel panelContenedor;
        private Button btnSolicitud;
        private Button btnHistorial;
        private Button btnMermaAlerta;
        private Button btnCatalogoCosto;
        private Button btnHistorialEstados;
        private Button btnCargaManualOC;
        private Label lblGestionCompra;
    }
}