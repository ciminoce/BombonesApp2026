using Bombones2026.Servicios.DTOs.Paginacion;
using Bombones2026.Servicios.DTOs.Transporte;
using Bombones2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    //TODO:Ver filtros
    public partial class frmTransportes : Form
    {
        private readonly TransporteServicio _transporteServicio;

        private BindingSource _bindingSource = new BindingSource();
        //para paginar
        private int paginaActual = 1;
        private int cantidadPorPagina = 10;
        private int totalRegistros = 0;
        private int totalPaginas = 0;
        //para filtrar
        private bool? filtroActivo = null;
        private string? textoBuscar = null;
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
                var resultado = _transporteServicio.ObtenerPagina(paginaActual, cantidadPorPagina,
                    filtroActivo, textoBuscar);
                MostrarDatosEnGrilla(resultado);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDatosEnGrilla(ResultadoPaginacionDto<TransporteListDto> resultado)
        {
            totalPaginas = resultado.TotalPaginas;
            totalRegistros = resultado.TotalRegistros;
            int desde = 1 + (paginaActual - 1) * cantidadPorPagina;
            int hasta = desde + cantidadPorPagina;
            if (hasta > totalRegistros)
            {
                hasta = totalRegistros;
            }
            _bindingSource.DataSource = resultado.Items;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = $"{desde} a {hasta} de {totalRegistros}";
            lblPaginas.Text = $"{paginaActual} de {totalPaginas}";

            btnPrimero.Enabled = resultado.TieneRegistrosAnteriores;
            btnAnterior.Enabled = resultado.TieneRegistrosAnteriores;
            btnSiguiente.Enabled = resultado.TieneRegistrosSiguientes;
            btnUltimo.Enabled = resultado.TieneRegistrosSiguientes;

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
                    int nuevoId = _transporteServicio.Agregar(transporteCreateDto);
                    if (filtroActivo is null || filtroActivo == true &&
                        string.IsNullOrWhiteSpace(txtBuscar.Text) ||
                        transporteCreateDto.NombreEmpresa.Contains(txtBuscar.Text))
                    {
                        paginaActual = _transporteServicio
                            .ObtenerPaginaRegistro(transporteCreateDto.NombreEmpresa, cantidadPorPagina,
                            filtroActivo, textoBuscar);

                    }
                    RecargarGrilla();
                    bool sePuedeVer = (filtroActivo is null || filtroActivo == true) &&
                        string.IsNullOrWhiteSpace(txtBuscar.Text) ||
                        transporteCreateDto.NombreEmpresa.ToLower().Contains(txtBuscar.Text.ToLower());
                    if (sePuedeVer)
                    {
                        var nuevoTransporte = _bindingSource.List
                            .Cast<TransporteListDto>()
                            .FirstOrDefault(t => t.TransporteId == nuevoId);
                        if (nuevoTransporte is null) return;
                        _bindingSource.Position = _bindingSource.IndexOf(nuevoTransporte);
                        MessageBox.Show("Transporte Agregado",
                            "Mensaje", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show($"Transporte {transporteCreateDto.NombreEmpresa} agregado.\nNo se muestra por condición de filtrado o búsqueda",
                            "Confirmación", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
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
                if (dgvDatos.Rows.Count == 1 && paginaActual > 1)
                {
                    paginaActual--;
                }
                RecargarGrilla();
                MessageBox.Show("Transporte eliminado",
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
                if (transporteEditDto is null) return;
                try
                {
                    _transporteServicio.Editar(transporteEditDto);
                    int editadoId = transporteEditDto.TransporteId;
                    bool sePuedeVer = (filtroActivo is null || filtroActivo == true) &&
                        string.IsNullOrWhiteSpace(txtBuscar.Text) ||
                        transporteEditDto.NombreEmpresa.ToLower().Contains(txtBuscar.Text.ToLower());

                    if (sePuedeVer)
                    {
                        paginaActual = _transporteServicio.ObtenerPaginaRegistro(transporteEditDto.NombreEmpresa,
                            cantidadPorPagina, filtroActivo, textoBuscar);
                    }
                    RecargarGrilla();
                    if (sePuedeVer)
                    {

                        var editadoTipo = _bindingSource.List
                            .Cast<TransporteListDto>()
                            .FirstOrDefault(tb => tb.TransporteId == editadoId);

                        _bindingSource.Position = _bindingSource.IndexOf(editadoTipo);
                        MessageBox.Show("Transporte editado",
                            "Mensaje",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Transporte {transporteEditDto.NombreEmpresa} editado.\nNo se muestra por condición de filtrado o búsqueda",
                            "Confirmación",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

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

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            RecargarGrilla();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual++;
            if (paginaActual > totalPaginas)
            {
                paginaActual = totalPaginas;
            }
            RecargarGrilla();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            paginaActual--;
            if (paginaActual <= 0)
            {
                paginaActual = 1;
            }
            RecargarGrilla();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = totalPaginas;
            RecargarGrilla();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Debe poner un texto para efectuar la búsqueda",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            textoBuscar = txtBuscar.Text;
            tsbBuscar.BackColor = Color.Orange;
            paginaActual = 1;
            RecargarGrilla();
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            filtroActivo = null;
            textoBuscar = null;
            txtBuscar.Clear();
            tsbBuscar.BackColor = SystemColors.Control;
            tsbFiltrar.BackColor = SystemColors.Control;
            paginaActual = 1;
            RecargarGrilla();

        }

        private void activoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtroActivo = true;
            paginaActual = 1;
            tsbFiltrar.BackColor = Color.Orange;
            RecargarGrilla();
        }

        private void noActivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtroActivo = false;
            paginaActual = 1;
            tsbFiltrar.BackColor = Color.Orange;
            RecargarGrilla();

        }
    }
}
