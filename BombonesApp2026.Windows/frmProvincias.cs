using Bombones2026.Servicios.DTOs.Provincia;
using Bombones2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    public partial class frmProvincias : Form
    {
        private readonly ProvinciaServicio _provinciaServicio;
        private List<ProvinciaListDto>? listaProvincias;
        public frmProvincias()
        {
            InitializeComponent();
            _provinciaServicio = new ProvinciaServicio();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MostrarDatosEnGrilla(List<ProvinciaListDto> listaProvincias)
        {
            LimpiarGrilla(dgvDatos);
            foreach (var provincia in listaProvincias)
            {
                DataGridViewRow r = ConstruirFila(dgvDatos);
                SetearFila(r, provincia);
                AgregarFila(r, dgvDatos);
            }
            lblCantidad.Text = listaProvincias.Count.ToString();
        }

        private void AgregarFila(DataGridViewRow r, DataGridView dgv)
        {
            dgv.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, ProvinciaListDto provincia)
        {
            r.Cells[0].Value = provincia.ProvinciaId;
            r.Cells[1].Value = provincia.Nombre;

            r.Tag = provincia;
        }

        private DataGridViewRow ConstruirFila(DataGridView dgv)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgv);
            return r;

        }

        private void LimpiarGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (frmProvinciaAe frm = new frmProvinciaAe() { Text = "Nueva Provincia" })
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                ProvinciaEditDto? provinciaEditDto = frm.GetProvincia();
                if (provinciaEditDto == null) return;
                try
                {
                    ProvinciaCreateDto provinciaCreateDto = new ProvinciaCreateDto
                    {
                        Nombre = provinciaEditDto.Nombre,
                    };
                    _provinciaServicio.Agregar(provinciaCreateDto);
                    listaProvincias = _provinciaServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(listaProvincias);
                    MessageBox.Show("Provincia Agregada",
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
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ProvinciaListDto provinciaDto = (ProvinciaListDto)r.Tag!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar la provincia {provinciaDto.Nombre}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _provinciaServicio.Borrar(provinciaDto.ProvinciaId);
                listaProvincias = _provinciaServicio.ObtenerTodos();
                MostrarDatosEnGrilla(listaProvincias);
                MessageBox.Show("Provincia eliminada",
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
            ProvinciaListDto provinciaDto = (ProvinciaListDto)r.Tag!;
            ProvinciaEditDto? provinciaEditDto = _provinciaServicio.ObtenerParaEditar(provinciaDto.ProvinciaId);
            if (provinciaEditDto is null) return;
            using (frmProvinciaAe frm = new frmProvinciaAe() { Text = "Editar Provincia" })
            {
                frm.SetProvincia(provinciaEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                provinciaEditDto = frm.GetProvincia();
                try
                {
                    _provinciaServicio.Editar(provinciaEditDto);
                    listaProvincias = _provinciaServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(listaProvincias);
                    MessageBox.Show("Provincia editada",
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

        private void frmProvincias_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            try
            {
                listaProvincias = _provinciaServicio.ObtenerTodos();
                MostrarDatosEnGrilla(listaProvincias);

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
