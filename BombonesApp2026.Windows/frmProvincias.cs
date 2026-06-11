using Bombones2026.Servicios.DTOs.Provincia;
using Bombones2026.Servicios.Servicios;
using System.ComponentModel;

namespace BombonesApp2026.Windows
{
    //TODO: Buscar Provincia
    public partial class frmProvincias : Form
    {
        private readonly ProvinciaServicio _provinciaServicio;
        private List<ProvinciaListDto>? _listaProvincias;
        private BindingSource _bindingSource = new BindingSource();
        public frmProvincias()
        {
            InitializeComponent();
            _provinciaServicio = new ProvinciaServicio();
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MostrarDatosEnGrilla(List<ProvinciaListDto> lista)
        {
            var bindingList = new BindingList<ProvinciaListDto>(lista);
            _bindingSource.DataSource = bindingList;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = lista.Count.ToString();
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
                    int nuevoId=_provinciaServicio.Agregar(provinciaCreateDto);
                    _listaProvincias = _provinciaServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaProvincias);
                    var nuevaPcia = _listaProvincias.FirstOrDefault(p => p.ProvinciaId == nuevoId);
                    if (nuevaPcia is null) return;
                    _bindingSource.Position = _bindingSource.IndexOf(nuevaPcia);
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
            if (_bindingSource.Current==null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            ProvinciaListDto provinciaDto = (ProvinciaListDto)_bindingSource.Current!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar la provincia {provinciaDto.Nombre}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _provinciaServicio.Borrar(provinciaDto.ProvinciaId);
                _listaProvincias = _provinciaServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaProvincias);
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
            if (_bindingSource.Current == null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            ProvinciaListDto provinciaDto = (ProvinciaListDto)_bindingSource.Current!;
            int posicion = _bindingSource.Position;
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
                    _listaProvincias = _provinciaServicio.ObtenerTodos();
                    MostrarDatosEnGrilla(_listaProvincias);
                    _bindingSource.Position= posicion;
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
                _listaProvincias = _provinciaServicio.ObtenerTodos();
                MostrarDatosEnGrilla(_listaProvincias);

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
