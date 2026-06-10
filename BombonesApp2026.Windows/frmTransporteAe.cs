using Bombones2026.Servicios.DTOs.Provincia;
using Bombones2026.Servicios.DTOs.Transporte;
using Bombones2026.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BombonesApp2026.Windows
{
    public partial class frmTransporteAe : Form
    {
        private TransporteEditDto? transporteDto;
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
            if(transporteDto is null)
            {
                chkActivo.Checked = true;
                chkActivo.Enabled = false;
            }
            else
            {
                txtTransporte.Text = transporteDto.NombreEmpresa;
                txtTelefono.Text = transporteDto.Telefono;
                txtEmail.Text = transporteDto.Email;
                cboProvincias.SelectedValue = transporteDto.ProvinciaId;
                chkActivo.Checked = transporteDto.Activo;
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
            cboProvincias.DataSource= listaProvincias;
            cboProvincias.DisplayMember = "Nombre";
            cboProvincias.ValueMember = "ProvinciaId";
            cboProvincias.SelectedIndex = 0;
        }

        public TransporteEditDto? GetTransporte()
        {
            return transporteDto;
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
                if(transporteDto is null)
                {
                    transporteDto = new TransporteEditDto();
                }
                transporteDto.NombreEmpresa = txtTransporte.Text;
                transporteDto.Telefono = txtTelefono.Text;
                transporteDto.Email = txtEmail.Text;
                transporteDto.ProvinciaId =(int) cboProvincias.SelectedValue!;

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
    }
}
