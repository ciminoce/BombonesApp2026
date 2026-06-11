using Bombones2026.Servicios.DTOs.TipoBombon;
using Bombones2026.Servicios.Servicios;
using System.ComponentModel;

namespace BombonesApp2026.Windows
{
    public partial class frmTiposDeBombones : Form
    {
        private readonly TipoBombonServicio _tipoServicio;
        private List<TipoBombonListDto>? _listaTipos;
        private BindingSource _bindingSource = new BindingSource();
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
            RecargarGrilla();
        }
        private void RecargarGrilla()
        {
            try
            {
                _listaTipos = _tipoServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaTipos);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void MostrarDatosEnGrilla(List<TipoBombonListDto> lista)
        {
            var bindingList = new BindingList<TipoBombonListDto>(lista);
            _bindingSource.DataSource = bindingList;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = lista.Count.ToString();
        }


        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaTipos = _tipoServicio.FiltrarPorActivo(true);
            MostrarDatosEnGrilla(_listaTipos);
            ManejarBotones(true);
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
            _listaTipos = _tipoServicio.FiltrarPorActivo(false);
            MostrarDatosEnGrilla(_listaTipos);
            ManejarBotones(true);

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();
            filtroOn=false;
            ManejarBotones(false);

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
                    int nuevoId=_tipoServicio.Agregar(tipoCreateDto);
                    _listaTipos = _tipoServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaTipos);
                    var nuevoTipo = _listaTipos.FirstOrDefault(tb => tb.TipoBombonId == nuevoId);
                    if (nuevoTipo is null) return;
                    _bindingSource.Position=_bindingSource.IndexOf(nuevoTipo);
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
            if (_bindingSource.Current == null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            TipoBombonListDto tipoBombonDto = (TipoBombonListDto)_bindingSource.Current!;
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
            if (_bindingSource.Current==null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            TipoBombonListDto tipoBombonDto = (TipoBombonListDto)_bindingSource.Current!;
            int posicion = _bindingSource.Position;
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
                    _bindingSource.Position= posicion;
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
