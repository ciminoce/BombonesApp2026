using Bombones2026.Servicios.DTOs.Rol;
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
    public partial class frmTransportes : Form
    {
        private readonly TransporteServicio _transporteServicio;
        private List<TransporteListDto>? _listaTransporte;
        private BindingSource _bindingSource = new BindingSource();
        public frmTransportes()
        {
            InitializeComponent();
            _transporteServicio = new TransporteServicio();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmTransportes_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                _listaTransporte = _transporteServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaTransporte);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDatosEnGrilla(List<TransporteListDto> listaTransporte)
        {
            var bindingList = new BindingList<TransporteListDto>(listaTransporte);
            _bindingSource.DataSource = bindingList;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = listaTransporte.Count.ToString();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (frmTransporteAe frm = new frmTransporteAe() { Text = "Nuevo Transporte" })
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                TransporteEditDto? transporteEditDto = frm.GetTransporte();
                if (transporteEditDto == null) return;
                try
                {
                    TransporteCreateDto transporteCreateDto = new TransporteCreateDto
                    {
                        NombreEmpresa = transporteEditDto.NombreEmpresa,
                        Telefono = transporteEditDto.Telefono,
                        Email=transporteEditDto.Email,
                        ProvinciaId=transporteEditDto.ProvinciaId,
                    };
                    var nuevoId = _transporteServicio.Agregar(transporteCreateDto);
                    _listaTransporte = _transporteServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaTransporte);
                    var nuevoTransporte = _listaTransporte.FirstOrDefault(t => t.TransporteId == nuevoId);
                    if (nuevoTransporte is null) return;
                    _bindingSource.Position = _bindingSource.IndexOf(nuevoTransporte);
                    MessageBox.Show("Transporte Agregado",
                        "Mensaje", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                }
            }

        }
    }
}
