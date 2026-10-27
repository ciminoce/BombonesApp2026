namespace BombonesApp2026.Windows
{
    partial class frmDetalleBombon
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
            groupBox1 = new GroupBox();
            txtDescripcion = new TextBox();
            btnCerrar = new Button();
            groupBox1.SuspendLayout();
            layoutPrincipal = new TableLayoutPanel();
            pnlContenido = new Panel();
            pnlAvisoInactividad = new Panel();
            layoutPrincipal.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtDescripcion);
            groupBox1.Location = new Point(41, 33);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(578, 139);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = " Descripción ";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(10, 20);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(557, 100);
            txtDescripcion.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Image = Properties.Resources.cancel_24px;
            btnCerrar.Location = new Point(544, 207);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 60);
            btnCerrar.TabIndex = 11;
            btnCerrar.Text = "Cerrar";
            btnCerrar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // frmDetalleBombon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 381);
            ControlBox = false;
            pnlContenido.Controls.Add(btnCerrar);
            pnlContenido.Controls.Add(groupBox1);
            MaximumSize = new Size(680, 420);
            MinimumSize = new Size(680, 420);
            // Filas independientes: contenido y aviso de inactividad.
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowCount = 2;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            layoutPrincipal.Size = new Size(664, 381);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Margin = new Padding(0);
            layoutPrincipal.Padding = new Padding(0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.TabIndex = 0;
            pnlContenido.Size = new Size(664, 285);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Margin = new Padding(0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.TabIndex = 0;
            pnlContenido.AutoScroll = true;
            pnlContenido.AutoScrollMinSize = new Size(664, 285);
            pnlAvisoInactividad.Size = new Size(664, 96);
            pnlAvisoInactividad.Dock = DockStyle.Fill;
            pnlAvisoInactividad.Margin = new Padding(0);
            pnlAvisoInactividad.Name = "pnlAvisoInactividad";
            pnlAvisoInactividad.TabIndex = 1;
            pnlAvisoInactividad.TabStop = false;
            layoutPrincipal.Controls.Add(pnlContenido, 0, 0);
            layoutPrincipal.Controls.Add(pnlAvisoInactividad, 0, 1);
            Controls.Add(layoutPrincipal);
            Name = "frmDetalleBombon";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDetalleBombon";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            layoutPrincipal.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutPrincipal;
        private Panel pnlContenido;
        private Panel pnlAvisoInactividad;

        private GroupBox groupBox1;
        private TextBox txtDescripcion;
        private Button btnCerrar;
    }
}