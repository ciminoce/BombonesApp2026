using Bombones2026.Servicios.DTOs.Ciudad;
using Bombones2026.Servicios.DTOs.FormaDePago;
using Bombones2026.Servicios.Servicios;
using System.ComponentModel;

namespace BombonesApp2026.Windows
{
    public partial class frmFormasDePago : Form
    {
        private readonly FormaDePagoServicio _formaDePagoServicio;
        private List<FormaDePagoListDto>? _listaformaDePago;
        private BindingSource _bindingSource = new BindingSource();
        public frmFormasDePago()
        {
            InitializeComponent();
            _formaDePagoServicio = new FormaDePagoServicio();
        }
        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void MostrarDatosEnGrilla(List<FormaDePagoListDto> lista)
        {
            var bindingList = new BindingList<FormaDePagoListDto>(lista);
            _bindingSource.DataSource = bindingList;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = lista.Count.ToString();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (frmFormaDePagoAe frm = new frmFormaDePagoAe() { Text = "Nueva Forma de Pago" })
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                FormaDePagoEditDto? formaDePagoEditDto = frm.GetFormaDePago();
                if (formaDePagoEditDto == null) return;
                try
                {
                    FormaDePagoCreateDto formaDePagoCreateDto = new FormaDePagoCreateDto
                    {
                        Nombre = formaDePagoEditDto.Nombre,
                    };
                    int nuevoId = _formaDePagoServicio.Agregar(formaDePagoCreateDto);

                     _listaformaDePago= _formaDePagoServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaformaDePago);
                    var nuevaForma = _listaformaDePago.FirstOrDefault(f => f.FormaDePagoId == nuevoId);
                    if (nuevaForma is null) return;
                    _bindingSource.Position = _bindingSource.IndexOf(nuevaForma);
                    MessageBox.Show("Forma de Pago Agregada",
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
            FormaDePagoListDto formaDePagoDto = (FormaDePagoListDto)r.Tag!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar la forma de pago {formaDePagoDto.Nombre}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _formaDePagoServicio.Borrar(formaDePagoDto.FormaDePagoId);
                _listaformaDePago = _formaDePagoServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaformaDePago);
                MessageBox.Show("Forma de Pago eliminada",
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
            FormaDePagoListDto formaDePagoDto = (FormaDePagoListDto)r.Tag!;
            FormaDePagoEditDto? formaDePagoEditDto = _formaDePagoServicio.ObtenerParaEditar(formaDePagoDto.FormaDePagoId);
            if (formaDePagoEditDto is null) return;
            using (frmFormaDePagoAe frm = new frmFormaDePagoAe() { Text = "Editar Forma de Pago       " })
            {
                frm.SetFormaDePago(formaDePagoEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                formaDePagoEditDto = frm.GetFormaDePago();
                try
                {
                    _formaDePagoServicio.Editar(formaDePagoEditDto);
                    _listaformaDePago = _formaDePagoServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaformaDePago);
                    MessageBox.Show("Forma de Pago editada",
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
            _listaformaDePago = _formaDePagoServicio.FiltrarPorActivo(true);
            MostrarDatosEnGrilla(_listaformaDePago);
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
            _listaformaDePago = _formaDePagoServicio.FiltrarPorActivo(false);
            MostrarDatosEnGrilla(_listaformaDePago);
            ManejarBotones(true);

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            _listaformaDePago = _formaDePagoServicio.ObtenerTodos();
            MostrarDatosEnGrilla(_listaformaDePago);
            ManejarBotones(false);
        }

        private void frmFormasDePago_Load(object sender, EventArgs e)
        {
            try
            {
                _listaformaDePago = _formaDePagoServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaformaDePago);

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
