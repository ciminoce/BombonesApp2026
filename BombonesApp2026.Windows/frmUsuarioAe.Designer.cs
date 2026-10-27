namespace BombonesApp2026.Windows
{
    partial class frmUsuarioAe
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
            chkActivo = new CheckBox();
            btnCancelar = new Button();
            btnOK = new Button();
            label2 = new Label();
            txtUsuario = new TextBox();
            label1 = new Label();
            cboRoles = new ComboBox();
            textBox1 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            comboBox1 = new ComboBox();
            layoutPrincipal = new TableLayoutPanel();
            pnlContenido = new Panel();
            pnlAvisoInactividad = new Panel();
            layoutPrincipal.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.CheckAlign = ContentAlignment.MiddleRight;
            chkActivo.Location = new Point(53, 177);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(65, 19);
            chkActivo.TabIndex = 9;
            chkActivo.Text = "Activo?";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = Properties.Resources.cancel_24px;
            btnCancelar.Location = new Point(390, 214);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 60);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Image = Properties.Resources.ok_24px;
            btnOK.Location = new Point(57, 214);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 60);
            btnOK.TabIndex = 10;
            btnOK.Text = "OK";
            btnOK.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOK.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 81);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 5;
            label2.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(131, 78);
            txtUsuario.MaxLength = 30;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(334, 23);
            txtUsuario.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(89, 46);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 7;
            label1.Text = "Rol:";
            // 
            // cboRoles
            // 
            cboRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRoles.FormattingEnabled = true;
            cboRoles.Location = new Point(131, 43);
            cboRoles.Name = "cboRoles";
            cboRoles.Size = new Size(333, 23);
            cboRoles.TabIndex = 12;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(131, 107);
            textBox1.MaxLength = 100;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(334, 23);
            textBox1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 107);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 5;
            label3.Text = "Password:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(53, 136);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 5;
            label4.Text = "Empleado:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(131, 136);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(333, 23);
            comboBox1.TabIndex = 12;
            // 
            // frmUsuarioAe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 396);
            pnlContenido.Controls.Add(comboBox1);
            pnlContenido.Controls.Add(cboRoles);
            pnlContenido.Controls.Add(chkActivo);
            pnlContenido.Controls.Add(btnCancelar);
            pnlContenido.Controls.Add(btnOK);
            pnlContenido.Controls.Add(label3);
            pnlContenido.Controls.Add(label4);
            pnlContenido.Controls.Add(label2);
            pnlContenido.Controls.Add(textBox1);
            pnlContenido.Controls.Add(txtUsuario);
            pnlContenido.Controls.Add(label1);
            MaximumSize = new Size(545, 435);
            MinimumSize = new Size(545, 435);
            // Filas independientes: contenido y aviso de inactividad.
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowCount = 2;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            layoutPrincipal.Size = new Size(529, 396);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Margin = new Padding(0);
            layoutPrincipal.Padding = new Padding(0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.TabIndex = 0;
            pnlContenido.Size = new Size(529, 300);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Margin = new Padding(0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.TabIndex = 0;
            pnlContenido.AutoScroll = true;
            pnlContenido.AutoScrollMinSize = new Size(529, 300);
            pnlAvisoInactividad.Size = new Size(529, 96);
            pnlAvisoInactividad.Dock = DockStyle.Fill;
            pnlAvisoInactividad.Margin = new Padding(0);
            pnlAvisoInactividad.Name = "pnlAvisoInactividad";
            pnlAvisoInactividad.TabIndex = 1;
            pnlAvisoInactividad.TabStop = false;
            layoutPrincipal.Controls.Add(pnlContenido, 0, 0);
            layoutPrincipal.Controls.Add(pnlAvisoInactividad, 0, 1);
            Controls.Add(layoutPrincipal);
            Name = "frmUsuarioAe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmUsuarioAe";
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

        private CheckBox chkActivo;
        private Button btnCancelar;
        private Button btnOK;
        private Label label2;
        private TextBox txtUsuario;
        private Label label1;
        private ComboBox cboRoles;
        private TextBox textBox1;
        private Label label3;
        private Label label4;
        private ComboBox comboBox1;
    }
}