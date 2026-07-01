using Bombones2026.Servicios.DTOs.Transporte;
using Bombones2026.Servicios.Servicios;
using System.ComponentModel;

namespace BombonesApp2026.Windows
{
    //TODO:Ver filtros
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
            _bindingSource.DataSource = listaTransporte;
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
                        Email = transporteEditDto.Email,
                        ProvinciaId = transporteEditDto.ProvinciaId,
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

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current == null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            TransporteListDto transporteDto = (TransporteListDto)_bindingSource.Current!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar el transporte {transporteDto.NombreEmpresa}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _transporteServicio.Borrar(transporteDto.TransporteId);
                _listaTransporte = _transporteServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaTransporte);
                MessageBox.Show("Tipo de Bombón eliminado",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }


        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current == null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TransporteListDto transporteDto = (TransporteListDto)_bindingSource.Current!;
            int posicion = _bindingSource.Position;
            TransporteEditDto? transporteEditDto = _transporteServicio.ObtenerParaEditar(transporteDto.TransporteId);
            if (transporteEditDto is null) return;
            using (frmTransporteAe frm = new frmTransporteAe() { Text = "Editar Transporte " })
            {
                frm.SetTransporte(transporteEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                transporteEditDto = frm.GetTransporte();
                try
                {
                    _transporteServicio.Editar(transporteEditDto!);
                    _listaTransporte = _transporteServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaTransporte);
                    _bindingSource.Position = posicion;
                    MessageBox.Show("Transporte editado",
                        "Mensaje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,
                         "Error",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error);

                }
            }


        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
