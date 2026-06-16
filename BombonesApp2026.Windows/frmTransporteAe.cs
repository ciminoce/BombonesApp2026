using Bombones2026.Servicios.DTOs.Provincia;
using Bombones2026.Servicios.DTOs.Transporte;
using Bombones2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    public partial class frmTransporteAe : Form
    {
        private TransporteEditDto? _transporteDto;
        private readonly ProvinciaServicio _provinciaServicio;
        public frmTransporteAe()
        {
            InitializeComponent();
            _provinciaServicio = new ProvinciaServicio();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarDatosEnCombo(ref cboProvincias);
            if (_transporteDto is null)
            {
                chkActivo.Checked = true;
                chkActivo.Enabled = false;
            }
            else
            {
                txtTransporte.Text = _transporteDto.NombreEmpresa;
                txtTelefono.Text = _transporteDto.Telefono;
                txtEmail.Text = _transporteDto.Email;
                cboProvincias.SelectedValue = _transporteDto.ProvinciaId;
                chkActivo.Checked = _transporteDto.Activo;
            }

        }

        private void CargarDatosEnCombo(ref ComboBox cboProvincias)
        {
            var listaProvincias = _provinciaServicio.ObtenerTodos();
            var defaultProvincia = new ProvinciaListDto()
            {
                ProvinciaId = 0,
                Nombre = "Seleccione Provincia"
            };
            listaProvincias.Insert(0, defaultProvincia);
            cboProvincias.DataSource = listaProvincias;
            cboProvincias.DisplayMember = "Nombre";
            cboProvincias.ValueMember = "ProvinciaId";
            cboProvincias.SelectedIndex = 0;
        }

        public TransporteEditDto? GetTransporte()
        {
            return _transporteDto;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (_transporteDto is null)
                {
                    _transporteDto = new TransporteEditDto();
                }
                _transporteDto.NombreEmpresa = txtTransporte.Text;
                _transporteDto.Telefono = txtTelefono.Text;
                _transporteDto.Email = txtEmail.Text;
                _transporteDto.ProvinciaId = (int)cboProvincias.SelectedValue!;
                _transporteDto.Activo = chkActivo.Checked;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrWhiteSpace(txtTransporte.Text))
            {
                valido = false;
                errorProvider1.SetError(txtTransporte, "El transporte es requerido");
            }
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                valido = false;
                errorProvider1.SetError(txtTelefono, "El teléfono es requerido");
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                valido = false;
                errorProvider1.SetError(txtEmail, "El Email es requerido");
            }
            if (cboProvincias.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboProvincias, "Debe seleccionar una provincia");
            }
            return valido;
        }

        public void SetTransporte(TransporteEditDto transporteEditDto)
        {
            _transporteDto = transporteEditDto;
        }
    }
}
