namespace BombonesApp2026.Windows
{
    partial class frmBombonAe
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
            cboTipoBombon = new ComboBox();
            btnCancelar = new Button();
            btnOK = new Button();
            txtNombreBombon = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            txtDescripcion = new TextBox();
            label4 = new Label();
            txtPrecio = new TextBox();
            label5 = new Label();
            nudStock = new NumericUpDown();
            chkTieneAzucar = new CheckBox();
            chkActivo = new CheckBox();
            label6 = new Label();
            nudPesoEnGramos = new NumericUpDown();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPesoEnGramos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            layoutPrincipal = new TableLayoutPanel();
            pnlContenido = new Panel();
            pnlAvisoInactividad = new Panel();
            layoutPrincipal.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // cboTipoBombon
            // 
            cboTipoBombon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoBombon.FormattingEnabled = true;
            cboTipoBombon.Location = new Point(140, 161);
            cboTipoBombon.Name = "cboTipoBombon";
            cboTipoBombon.Size = new Size(334, 23);
            cboTipoBombon.TabIndex = 11;
            cboTipoBombon.SelectedIndexChanged += cboTipoBombon_SelectedIndexChanged;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = Properties.Resources.cancel_24px;
            btnCancelar.Location = new Point(399, 325);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 60);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnOK
            // 
            btnOK.Image = Properties.Resources.ok_24px;
            btnOK.Location = new Point(31, 325);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 60);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // txtNombreBombon
            // 
            txtNombreBombon.Location = new Point(140, 28);
            txtNombreBombon.MaxLength = 100;
            txtNombreBombon.Name = "txtNombreBombon";
            txtNombreBombon.Size = new Size(334, 23);
            txtNombreBombon.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 31);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 6;
            label2.Text = "Nombre Bombón:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 164);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 7;
            label1.Text = "Tipo de Bombón:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 60);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 6;
            label3.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(140, 57);
            txtDescripcion.MaxLength = 300;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(334, 98);
            txtDescripcion.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 195);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 6;
            label4.Text = "Precio:";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(140, 190);
            txtPrecio.MaxLength = 100;
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(134, 23);
            txtPrecio.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 225);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 6;
            label5.Text = "Stock:";
            // 
            // nudStock
            // 
            nudStock.Location = new Point(140, 221);
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(134, 23);
            nudStock.TabIndex = 12;
            // 
            // chkTieneAzucar
            // 
            chkTieneAzucar.AutoSize = true;
            chkTieneAzucar.CheckAlign = ContentAlignment.MiddleRight;
            chkTieneAzucar.Location = new Point(140, 287);
            chkTieneAzucar.Name = "chkTieneAzucar";
            chkTieneAzucar.Size = new Size(101, 19);
            chkTieneAzucar.TabIndex = 13;
            chkTieneAzucar.Text = "¿Tiene azucar?";
            chkTieneAzucar.UseVisualStyleBackColor = true;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.CheckAlign = ContentAlignment.MiddleRight;
            chkActivo.Location = new Point(373, 287);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(60, 19);
            chkActivo.TabIndex = 13;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(31, 254);
            label6.Name = "label6";
            label6.Size = new Size(81, 15);
            label6.TabIndex = 6;
            label6.Text = "Peso (en grs.):";
            // 
            // nudPesoEnGramos
            // 
            nudPesoEnGramos.Location = new Point(140, 250);
            nudPesoEnGramos.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            nudPesoEnGramos.Name = "nudPesoEnGramos";
            nudPesoEnGramos.Size = new Size(134, 23);
            nudPesoEnGramos.TabIndex = 12;
            nudPesoEnGramos.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmBombonAe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 503);
            pnlContenido.Controls.Add(chkActivo);
            pnlContenido.Controls.Add(chkTieneAzucar);
            pnlContenido.Controls.Add(nudPesoEnGramos);
            pnlContenido.Controls.Add(nudStock);
            pnlContenido.Controls.Add(cboTipoBombon);
            pnlContenido.Controls.Add(btnCancelar);
            pnlContenido.Controls.Add(btnOK);
            pnlContenido.Controls.Add(txtDescripcion);
            pnlContenido.Controls.Add(label6);
            pnlContenido.Controls.Add(label3);
            pnlContenido.Controls.Add(label5);
            pnlContenido.Controls.Add(txtPrecio);
            pnlContenido.Controls.Add(label4);
            pnlContenido.Controls.Add(txtNombreBombon);
            pnlContenido.Controls.Add(label2);
            pnlContenido.Controls.Add(label1);
            // Filas independientes: contenido y aviso de inactividad.
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowCount = 2;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            layoutPrincipal.Size = new Size(511, 503);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Margin = new Padding(0);
            layoutPrincipal.Padding = new Padding(0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.TabIndex = 0;
            pnlContenido.Size = new Size(511, 407);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Margin = new Padding(0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.TabIndex = 0;
            pnlContenido.AutoScroll = true;
            pnlContenido.AutoScrollMinSize = new Size(511, 407);
            pnlAvisoInactividad.Size = new Size(511, 96);
            pnlAvisoInactividad.Dock = DockStyle.Fill;
            pnlAvisoInactividad.Margin = new Padding(0);
            pnlAvisoInactividad.Name = "pnlAvisoInactividad";
            pnlAvisoInactividad.TabIndex = 1;
            pnlAvisoInactividad.TabStop = false;
            layoutPrincipal.Controls.Add(pnlContenido, 0, 0);
            layoutPrincipal.Controls.Add(pnlAvisoInactividad, 0, 1);
            Controls.Add(layoutPrincipal);
            MinimumSize = new Size(527, 542);
            Name = "frmBombonAe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmBombonAe";
            Load += frmBombonAe_Load;
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPesoEnGramos).EndInit();
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

        private ComboBox cboTipoBombon;
        private Button btnCancelar;
        private Button btnOK;
        private TextBox txtNombreBombon;
        private Label label2;
        private Label label1;
        private Label label3;
        private TextBox txtDescripcion;
        private Label label4;
        private TextBox txtPrecio;
        private Label label5;
        private NumericUpDown nudStock;
        private CheckBox chkTieneAzucar;
        private CheckBox chkActivo;
        private Label label6;
        private NumericUpDown nudPesoEnGramos;
        private ErrorProvider errorProvider1;
    }
}