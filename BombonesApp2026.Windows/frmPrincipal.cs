namespace BombonesApp2026.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            using (frmRoles frm = new frmRoles() { Text = "Listado de Roles" })
            {
                frm.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTiposBombones_Click(object sender, EventArgs e)
        {
            using (frmTiposDeBombones frm = new frmTiposDeBombones() { Text = "Listado de Tipos de Bombones" })
            {
                frm.ShowDialog();
            }

        }

        private void btnFormaDePago_Click(object sender, EventArgs e)
        {
            using (frmFormasDePago frm = new frmFormasDePago() { Text = "Listado de Formas de Pago" })
            {
                frm.ShowDialog();
            }

        }

        private void btnProvincias_Click(object sender, EventArgs e)
        {
            using (frmProvincias frm = new frmProvincias() { Text = "Listado de Provincias" })
            {
                frm.ShowDialog();
            }
        }
    }
}
