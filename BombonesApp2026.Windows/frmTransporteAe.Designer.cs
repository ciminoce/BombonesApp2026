namespace BombonesApp2026.Windows
{
    partial class frmTransporteAe
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
            label1 = new Label();
            label2 = new Label();
            txtTransporte = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            btnOK = new Button();
            btnCancelar = new Button();
            cboProvincias = new ComboBox();
            label3 = new Label();
            txtTelefono = new TextBox();
            label4 = new Label();
            txtEmail = new TextBox();
            chkActivo = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            layoutPrincipal = new TableLayoutPanel();
            pnlContenido = new Panel();
            pnlAvisoInactividad = new Panel();
            layoutPrincipal.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 107);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Provincia:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 17);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 0;
            label2.Text = "Transporte:";
            // 
            // txtTransporte
            // 
            txtTransporte.Location = new Point(123, 14);
            txtTransporte.MaxLength = 50;
            txtTransporte.Name = "txtTransporte";
            txtTransporte.Size = new Size(334, 23);
            txtTransporte.TabIndex = 1;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // btnOK
            // 
            btnOK.Image = Properties.Resources.ok_24px;
            btnOK.Location = new Point(45, 166);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 60);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = Properties.Resources.cancel_24px;
            btnCancelar.Location = new Point(382, 165);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 60);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // cboProvincias
            // 
            cboProvincias.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProvincias.FormattingEnabled = true;
            cboProvincias.Location = new Point(123, 106);
            cboProvincias.Name = "cboProvincias";
            cboProvincias.Size = new Size(334, 23);
            cboProvincias.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 46);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 0;
            label3.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(123, 43);
            txtTelefono.MaxLength = 20;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(334, 23);
            txtTelefono.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 75);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 0;
            label4.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(123, 72);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(334, 23);
            txtEmail.TabIndex = 1;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.CheckAlign = ContentAlignment.MiddleRight;
            chkActivo.Location = new Point(123, 139);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(60, 19);
            chkActivo.TabIndex = 6;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            chkActivo.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // frmTransporteAe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 330);
            pnlContenido.Controls.Add(chkActivo);
            pnlContenido.Controls.Add(cboProvincias);
            pnlContenido.Controls.Add(btnCancelar);
            pnlContenido.Controls.Add(btnOK);
            pnlContenido.Controls.Add(txtEmail);
            pnlContenido.Controls.Add(label4);
            pnlContenido.Controls.Add(txtTelefono);
            pnlContenido.Controls.Add(label3);
            pnlContenido.Controls.Add(txtTransporte);
            pnlContenido.Controls.Add(label2);
            pnlContenido.Controls.Add(label1);
            MaximumSize = new Size(536, 369);
            MinimumSize = new Size(536, 369);
            // Filas independientes: contenido y aviso de inactividad.
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowCount = 2;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            layoutPrincipal.Size = new Size(520, 330);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Margin = new Padding(0);
            layoutPrincipal.Padding = new Padding(0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.TabIndex = 0;
            pnlContenido.Size = new Size(520, 234);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Margin = new Padding(0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.TabIndex = 0;
            pnlContenido.AutoScroll = true;
            pnlContenido.AutoScrollMinSize = new Size(520, 234);
            pnlAvisoInactividad.Size = new Size(520, 96);
            pnlAvisoInactividad.Dock = DockStyle.Fill;
            pnlAvisoInactividad.Margin = new Padding(0);
            pnlAvisoInactividad.Name = "pnlAvisoInactividad";
            pnlAvisoInactividad.TabIndex = 1;
            pnlAvisoInactividad.TabStop = false;
            layoutPrincipal.Controls.Add(pnlContenido, 0, 0);
            layoutPrincipal.Controls.Add(pnlAvisoInactividad, 0, 1);
            Controls.Add(layoutPrincipal);
            Name = "frmTransporteAe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmCiudadAe";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            layoutPrincipal.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel layoutPrincipal;
        private Panel pnlContenido;
        private Panel pnlAvisoInactividad;

        private Label label1;
        private Label label2;
        private TextBox txtTransporte;
        private ErrorProvider errorProvider1;
        private Button btnCancelar;
        private Button btnOK;
        private ComboBox cboProvincias;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtTelefono;
        private Label label3;
        private CheckBox chkActivo;
    }
}