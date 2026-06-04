using Bombones2026.Servicios.DTOs.Ciudad;
using Bombones2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    public partial class frmCiudades : Form
    {
        private readonly CiudadServicio _ciudadServicio;
        private List<CiudadListDto>? _listaCiudades;
        public frmCiudades()
        {
            InitializeComponent();
            _ciudadServicio = new CiudadServicio();
        }

        private void frmCiudades_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                _listaCiudades = _ciudadServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaCiudades);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void MostrarDatosEnGrilla(List<CiudadListDto> lista)
        {
            LimpiarGrilla(dgvDatos);
            foreach (var item in lista)
            {
                var r = ConstruirFila(dgvDatos);
                SetearFila(r, item);
                AgregarFila(r, dgvDatos);
            }
            lblCantidad.Text = lista.Count.ToString();
        }

        private void LimpiarGrilla(DataGridView dgvDatos)
        {
            dgvDatos.Rows.Clear();
        }

        private void SetearFila(DataGridViewRow r, CiudadListDto obj)
        {
            r.Cells[0].Value = obj.CiudadId;
            r.Cells[1].Value = obj.Ciudad;
            r.Cells[2].Value = obj.Provincia;

            r.Tag = obj;
        }

        private void AgregarFila(DataGridViewRow r, DataGridView dgvDatos)
        {
            dgvDatos.Rows.Add(r);
        }

        private DataGridViewRow ConstruirFila(DataGridView dgvDatos)
        {
            var r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (frmCiudadAe frm = new frmCiudadAe() { Text = "Nueva Ciudad" })
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                try
                {
                    CiudadEditDto? ciudad = frm.GetCiudad();
                    if (ciudad is null) return;
                    CiudadCreateDto ciudadCreateDto = new CiudadCreateDto
                    {
                        Nombre = ciudad.Nombre,
                        ProvinciaId = ciudad.ProvinciaId
                    };
                    _ciudadServicio.Agregar(ciudadCreateDto);
                    _listaCiudades = _ciudadServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaCiudades);
                    MessageBox.Show("Registro agregado", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            CiudadListDto ciudadDto = (CiudadListDto)r.Tag!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar la ciudad {ciudadDto.Ciudad}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _ciudadServicio.Borrar(ciudadDto.CiudadId);
                _listaCiudades = _ciudadServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaCiudades);
                MessageBox.Show("Ciudad eliminada",
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
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            CiudadListDto ciudadDto = (CiudadListDto)r.Tag!;
            CiudadEditDto? ciudadEditDto = _ciudadServicio.ObtenerParaEditar(ciudadDto.CiudadId);
            if (ciudadEditDto is null) return;
            using (frmCiudadAe frm = new frmCiudadAe() { Text = "Editar Ciudad " })
            {
                frm.SetCiudad(ciudadEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                ciudadEditDto = frm.GetCiudad();
                try
                {
                    _ciudadServicio.Editar(ciudadEditDto!);
                    _listaCiudades = _ciudadServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaCiudades);
                    MessageBox.Show("Ciudd editada",
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
    }
}
