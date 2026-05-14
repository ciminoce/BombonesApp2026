using Bombones2026.Servicios;
using Bombones2026.Servicios.DTOs.Rol;

namespace BombonesApp2026.Windows
{
    public partial class frmRoles : Form
    {
        private readonly RolServicio _rolServicio;
        private List<RolListDto>? listaRoles;
        public frmRoles()
        {
            InitializeComponent();
            _rolServicio = new RolServicio();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            listaRoles = _rolServicio.GetLista();
            MostrarDatosEnGrilla(listaRoles);
        }

        private void MostrarDatosEnGrilla(List<RolListDto> listaRoles)
        {
            LimpiarGrilla(dgvRoles);
            foreach (var rol in listaRoles)
            {
                DataGridViewRow r = ConstruirFila(dgvRoles);
                SetearFila(r, rol);
                AgregarFila(r, dgvRoles);
            }
            lblCantidad.Text = listaRoles.Count.ToString();
        }

        private void AgregarFila(DataGridViewRow r, DataGridView dgv)
        {
            dgv.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, RolListDto rol)
        {
            r.Cells[0].Value = rol.RolId;
            r.Cells[1].Value = rol.Nombre;
            r.Cells[2].Value = rol.Activo;

            r.Tag = rol;
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
            using (frmRolesAe frm = new frmRolesAe() { Text = "Nuevo Rol" })
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                RolEditDto? rolEditDto = frm.GetRol();
                if (rolEditDto == null) return;
                try
                {
                    RolCreateDto rolCreateDto = new RolCreateDto
                    {
                        Nombre = rolEditDto.Nombre,
                        Descripcion = rolEditDto.Descripcion,
                    };
                    _rolServicio.Agregar(rolCreateDto);
                    listaRoles = _rolServicio.GetLista();
                    MostrarDatosEnGrilla(listaRoles);
                    MessageBox.Show("Rol Agregado",
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
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var r = dgvRoles.SelectedRows[0];
            RolListDto rolDto = (RolListDto)r.Tag!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar el rol {rolDto.Nombre}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _rolServicio.Borrar(rolDto.RolId);
                listaRoles = _rolServicio.GetLista();
                MostrarDatosEnGrilla(listaRoles);
                MessageBox.Show("Rol eliminado",
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
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var r = dgvRoles.SelectedRows[0];
            RolListDto rolDto = (RolListDto)r.Tag!;
            RolEditDto? rolEditDto = _rolServicio.GetForUpdate(rolDto.RolId);
            if (rolEditDto is null) return;
            using (frmRolesAe frm = new frmRolesAe() { Text = "Editar Rol" })
            {
                frm.SetRol(rolEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                rolEditDto = frm.GetRol();
                try
                {
                    _rolServicio.Editar(rolEditDto);
                    listaRoles = _rolServicio.GetLista();
                    MostrarDatosEnGrilla(listaRoles);
                    MessageBox.Show("Rol agregado",
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

        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaRoles = _rolServicio.GetPorActivo(true);
            MostrarDatosEnGrilla(listaRoles);
            ManejarBotones(true);
        }

        private void ManejarBotones(bool filtrado)
        {
            tsbNuevo.Enabled = !filtrado;
            tsbBorrar.Enabled = !filtrado;
            tsbEditar.Enabled = !filtrado;

            tsbFiltrar.BackColor = filtrado ? Color.Orange : SystemColors.Control;
        }

        private void noActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaRoles = _rolServicio.GetPorActivo(false);
            MostrarDatosEnGrilla(listaRoles);
            ManejarBotones(true);

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            listaRoles = _rolServicio.GetLista();
            MostrarDatosEnGrilla(listaRoles);
            ManejarBotones(false);
        }
    }
}
