using Bombones2026.Servicios.DTOs.Ciudad;
using Bombones2026.Servicios.Servicios;
using System.ComponentModel;

namespace BombonesApp2026.Windows
{
    public partial class frmCiudades : Form
    {
        private readonly CiudadServicio _ciudadServicio;
        private List<CiudadListDto>? _listaCiudades;
        private BindingSource _bindingSource = new BindingSource();
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
            var bindingList = new BindingList<CiudadListDto>(lista);
            _bindingSource.DataSource = bindingList;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = lista.Count.ToString();
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
                    int nuevoId=_ciudadServicio.Agregar(ciudadCreateDto);

                    _listaCiudades = _ciudadServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaCiudades);
                    var nuevaCiudad = _listaCiudades.FirstOrDefault(c => c.CiudadId == nuevoId);
                    if (nuevaCiudad is null) return;
                    _bindingSource.Position = _bindingSource.IndexOf(nuevaCiudad);
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
            if (_bindingSource.Current==null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            CiudadListDto ciudadDto = (CiudadListDto)_bindingSource.Current!;
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
            if (_bindingSource.Current==null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            CiudadListDto ciudadDto = (CiudadListDto)_bindingSource.Current!;
            int posicion = _bindingSource.Position;
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
                    _bindingSource.Position= posicion;
                    MessageBox.Show("Ciudad editada",
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
