using Bombones2026.Servicios.DTOs.FormaDePago;

namespace BombonesApp2026.Windows
{
    public partial class frmFormaDePagoAe : Form
    {
        private FormaDePagoEditDto? formaDePagoDto;
        public frmFormaDePagoAe()
        {
            InitializeComponent();
        }

        public FormaDePagoEditDto? GetFormaDePago()
        {
            return formaDePagoDto;
        }

        public void SetFormaDePago(FormaDePagoEditDto formaDePagoEditDto)
        {
            formaDePagoDto = formaDePagoEditDto;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (formaDePagoDto is null)
                {
                    formaDePagoDto = new FormaDePagoEditDto();
                }
                formaDePagoDto.Nombre = txtFormaDePago.Text;
                formaDePagoDto.Activo = chkActivo.Checked;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtFormaDePago.Text))
            {
                valido = false;
                errorProvider1.SetError(txtFormaDePago, "La forma de pago es obligatoria");
            }
            return valido;
        }

    }
}
