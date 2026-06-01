using Bombones2026.Servicios.DTOs.TipoBombon;
using Bombones2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    public partial class frmTiposDeBombones : Form
    {
        private readonly TipoBombonServicio _tipoServicio;
        private List<TipoBombonListDto>? _listaTipos;
        private bool filtroOn = false;

        public frmTiposDeBombones()
        {
            InitializeComponent();
            _tipoServicio = new TipoBombonServicio();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmTiposDeBombones_Load(object sender, EventArgs e)
        {
            try
            {
                _listaTipos = _tipoServicio.ObtenerTodos();
                filtroOn = true;
                MostrarDatosEnGrilla(_listaTipos);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                            "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }
        }

        private void MostrarDatosEnGrilla(List<TipoBombonListDto> listaTipos)
        {
            dgvDatos.Rows.Clear();
            foreach (var tipo in listaTipos)
            {
                var r = ConstruirFila(dgvDatos);
                SetearFila(r, tipo);
                AgregarFila(r, dgvDatos);
            }
            lblCantidad.Text = listaTipos.Count.ToString();
        }

        private void SetearFila(DataGridViewRow r, TipoBombonListDto tipo)
        {
            r.Cells[0].Value = tipo.TipoBombonId;
            r.Cells[1].Value = tipo.Nombre;
            r.Cells[2].Value = tipo.Descripcion;
            r.Cells[3].Value = tipo.Activo;

            r.Tag = tipo;
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

        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _listaTipos = _tipoServicio.FiltrarPorActivo(true);
                MostrarDatosEnGrilla(_listaTipos);
                ManejarBotones(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                            "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }
        }

        private void ManejarBotones(bool v)
        {
            tsbNuevo.Enabled = v;
            tsbEditar.Enabled = v;
            tsbBorrar.Enabled = v;

            tsbFiltrar.BackColor = filtroOn ? Color.Orange : SystemColors.Control;


        }

        private void noActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _listaTipos = _tipoServicio.FiltrarPorActivo(false);
                MostrarDatosEnGrilla(_listaTipos);
                ManejarBotones(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                            "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                _listaTipos = _tipoServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaTipos);
                filtroOn = false;
                ManejarBotones(true);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,
                            "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }

        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (frmTipoDeBombonesAe frm = new frmTipoDeBombonesAe() { Text = "Nuevo Tipo de Bombón" })
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                TipoBombonEditDto? tipoEditDto = frm.GetTipo();
                if (tipoEditDto == null) return;
                try
                {
                    TipoBombonCreateDto tipoCreateDto = new TipoBombonCreateDto
                    {
                        Nombre = tipoEditDto.Nombre,
                        Descripcion = tipoEditDto.Descripcion,
                    };
                    _tipoServicio.Agregar(tipoCreateDto);
                    _listaTipos = _tipoServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaTipos);
                    MessageBox.Show("Tipo de Bombón Agregado",
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
            TipoBombonListDto tipoBombonDto = (TipoBombonListDto)r.Tag!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar el tipo de bombón {tipoBombonDto.Nombre}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _tipoServicio.Borrar(tipoBombonDto.TipoBombonId);
                _listaTipos = _tipoServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaTipos);
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
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            TipoBombonListDto tipoBombonDto = (TipoBombonListDto)r.Tag!;
            TipoBombonEditDto? tipoBombonEditDto = _tipoServicio.ObtenerParaEditar(tipoBombonDto.TipoBombonId);
            if (tipoBombonEditDto is null) return;
            using (frmTipoDeBombonesAe frm = new frmTipoDeBombonesAe() { Text = "Editar Tipo de Bombón " })
            {
                frm.SetTipo(tipoBombonEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                tipoBombonEditDto = frm.GetTipo();
                try
                {
                    _tipoServicio.Editar(tipoBombonEditDto);
                    _listaTipos = _tipoServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaTipos);
                    MessageBox.Show("Tipo de Bombón editado",
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
