using Bombones2026.Servicios.DTOs.Paginacion;
using Bombones2026.Servicios.DTOs.TipoBombon;
using Bombones2026.Servicios.Servicios;

namespace BombonesApp2026.Windows
{
    public partial class frmTiposDeBombones : Form
    {
        private readonly TipoBombonServicio _tipoServicio;
        //private List<TipoBombonListDto>? _listaTipos;
        private BindingSource _bindingSource = new BindingSource();
        //para filtrar
        private bool? filtroActivo = null;

        //para paginar
        private int paginaActual = 1;
        private int cantidadPorPagina = 10;
        private int totalRegistros = 0;
        private int totalPaginas = 0;
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
                var resultado = _tipoServicio.ObtenerPagina(paginaActual,
                    cantidadPorPagina,
                    filtroActivo);
                MostrarDatosEnGrilla(resultado);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void MostrarDatosEnGrilla(ResultadoPaginacionDto<TipoBombonListDto> resultado)
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


        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtroActivo = true;
            tsbFiltrar.BackColor = Color.Orange;
            paginaActual = 1;
            RecargarGrilla();
        }


        private void noActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtroActivo = false;
            tsbFiltrar.BackColor = Color.Orange;
            paginaActual = 1;
            RecargarGrilla();

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            filtroActivo = null;
            tsbFiltrar.BackColor = SystemColors.Control;
            paginaActual = 1;
            RecargarGrilla();

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
                    int nuevoId = _tipoServicio.Agregar(tipoCreateDto);
                    if (filtroActivo is null || filtroActivo == true)
                    {
                        paginaActual = _tipoServicio
                            .ObtenerPaginaRegistro(tipoCreateDto.Nombre, cantidadPorPagina,
                            filtroActivo);

                    }
                    RecargarGrilla();
                    if (filtroActivo is null || filtroActivo == true)
                    {
                        var nuevoTipo = _bindingSource.List
                    .Cast<TipoBombonListDto>()
                    .FirstOrDefault(tb => tb.TipoBombonId == nuevoId);
                        if (nuevoTipo is null) return;
                        _bindingSource.Position = _bindingSource.IndexOf(nuevoTipo);
                        MessageBox.Show("Tipo de Bombón Agregado",
                            "Mensaje", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show($"Tipo de Bombón {tipoEditDto.Nombre} agregado.\nNo se muestra por condición de filtrado",
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
            TipoBombonListDto tipoBombonDto = (TipoBombonListDto)_bindingSource.Current!;
            DialogResult dr = MessageBox.Show($"¿Desea borrar el tipo de bombón {tipoBombonDto.Nombre}?",
                "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) return;
            try
            {
                _tipoServicio.Borrar(tipoBombonDto.TipoBombonId);
                if (dgvDatos.Rows.Count == 1 && paginaActual > 1)
                {
                    paginaActual--;
                }
                RecargarGrilla();
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
            TipoBombonListDto tipoBombonDto = (TipoBombonListDto)_bindingSource.Current!;
            TipoBombonEditDto? tipoBombonEditDto = _tipoServicio.ObtenerParaEditar(tipoBombonDto.TipoBombonId);
            if (tipoBombonEditDto is null) return;
            using (frmTipoDeBombonesAe frm = new frmTipoDeBombonesAe() { Text = "Editar Tipo de Bombón " })
            {
                frm.SetTipo(tipoBombonEditDto);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Cancel) return;
                tipoBombonEditDto = frm.GetTipo();
                if (tipoBombonEditDto is null) return;
                try
                {
                    _tipoServicio.Editar(tipoBombonEditDto);
                    int editadoId = tipoBombonEditDto.TipoBombonId;
                    if (filtroActivo is null || filtroActivo == tipoBombonEditDto.Activo)
                    {
                        paginaActual = _tipoServicio.ObtenerPaginaRegistro(tipoBombonEditDto.Nombre,
                            cantidadPorPagina);
                    }
                    RecargarGrilla();
                    if (filtroActivo is null || filtroActivo == tipoBombonEditDto.Activo)
                    {

                        var editadoTipo = _bindingSource.List
                            .Cast<TipoBombonListDto>()
                            .FirstOrDefault(tb => tb.TipoBombonId == editadoId);

                        _bindingSource.Position = _bindingSource.IndexOf(editadoTipo);
                        MessageBox.Show("Tipo de Bombón editado",
                            "Mensaje",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Tipo de Bombón {tipoBombonEditDto.Nombre} editado.\nNo se muestra por condición de filtrado",
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
    }
}
