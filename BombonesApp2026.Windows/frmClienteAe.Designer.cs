namespace BombonesApp2026.Windows
{
    partial class frmClienteAe
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
            btnCancelar = new Button();
            btnOK = new Button();
            groupBox2 = new GroupBox();
            label1 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            txtApellido = new TextBox();
            label3 = new Label();
            txtDocumento = new TextBox();
            groupBox1 = new GroupBox();
            textBox1 = new TextBox();
            txtEmail = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtTelefono = new TextBox();
            label6 = new Label();
            label7 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            txtLocalidad = new TextBox();
            label9 = new Label();
            txtProvincia = new TextBox();
            textBox3 = new TextBox();
            label10 = new Label();
            errorProvider1 = new ErrorProvider(components);
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Image = Properties.Resources.cancel_24px;
            btnCancelar.Location = new Point(653, 416);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 60);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Image = Properties.Resources.ok_24px;
            btnOK.Location = new Point(77, 416);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 60);
            btnOK.TabIndex = 5;
            btnOK.Text = "OK";
            btnOK.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDocumento);
            groupBox2.Controls.Add(txtApellido);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(txtNombre);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(32, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(565, 125);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = " Datos del Cliente";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 24);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(96, 21);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(402, 23);
            txtNombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 53);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(96, 50);
            txtApellido.MaxLength = 50;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(402, 23);
            txtApellido.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 85);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 0;
            label3.Text = "Documento:";
            // 
            // txtDocumento
            // 
            txtDocumento.Location = new Point(96, 82);
            txtDocumento.MaxLength = 20;
            txtDocumento.Name = "txtDocumento";
            txtDocumento.Size = new Size(402, 23);
            txtDocumento.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(txtProvincia);
            groupBox1.Controls.Add(txtLocalidad);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(label6);
            groupBox1.Location = new Point(32, 162);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(799, 209);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = " Datos Optativos del Cliente";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(96, 82);
            textBox1.MaxLength = 20;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(402, 23);
            textBox1.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(96, 50);
            txtEmail.MaxLength = 50;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(402, 23);
            txtEmail.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 85);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 0;
            label4.Text = "Calle:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 53);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 0;
            label5.Text = "Email:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(96, 21);
            txtTelefono.MaxLength = 20;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(402, 23);
            txtTelefono.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 24);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 0;
            label6.Text = "Teléfono:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(520, 85);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 0;
            label7.Text = "Número:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(602, 82);
            textBox2.MaxLength = 20;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(141, 23);
            textBox2.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 124);
            label8.Name = "label8";
            label8.Size = new Size(61, 15);
            label8.TabIndex = 0;
            label8.Text = "Localidad:";
            // 
            // txtLocalidad
            // 
            txtLocalidad.Location = new Point(96, 121);
            txtLocalidad.MaxLength = 20;
            txtLocalidad.Name = "txtLocalidad";
            txtLocalidad.Size = new Size(402, 23);
            txtLocalidad.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(17, 162);
            label9.Name = "label9";
            label9.Size = new Size(59, 15);
            label9.TabIndex = 0;
            label9.Text = "Provincia:";
            // 
            // txtProvincia
            // 
            txtProvincia.Location = new Point(96, 159);
            txtProvincia.MaxLength = 20;
            txtProvincia.Name = "txtProvincia";
            txtProvincia.Size = new Size(402, 23);
            txtProvincia.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(602, 159);
            textBox3.MaxLength = 20;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(141, 23);
            textBox3.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(526, 162);
            label10.Name = "label10";
            label10.Size = new Size(70, 15);
            label10.TabIndex = 0;
            label10.Text = "Cod. Postal:";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmClienteAe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(890, 527);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(btnCancelar);
            Controls.Add(btnOK);
            Name = "frmClienteAe";
            Text = "frmClienteAe";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancelar;
        private Button btnOK;
        private GroupBox groupBox2;
        private TextBox txtDocumento;
        private TextBox txtApellido;
        private Label label3;
        private Label label2;
        private TextBox txtNombre;
        private Label label1;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private TextBox txtEmail;
        private Label label4;
        private Label label5;
        private TextBox txtTelefono;
        private Label label6;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox txtProvincia;
        private TextBox txtLocalidad;
        private Label label9;
        private Label label8;
        private Label label10;
        private Label label7;
        private ErrorProvider errorProvider1;
    }
}