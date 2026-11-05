using Bombones2026.Servicios.DTOs.Paginacion;
using BombonesApp2026.Servicios.DTOs.Venta;
using BombonesApp2026.Servicios.Interfaces;
using BombonesApp2026.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace BombonesApp2026.Windows
{
    public partial class frmVentas : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVentaServicio _ventaServicio;
        private BindingSource _bindingSource = new BindingSource();
        private ServicioMensajes _servicioMensajes = new();
        private EstadoNavegacion _estadoNavegacion = new(10);

        public frmVentas(IServiceProvider serviceProvider,
            IVentaServicio ventaServicio)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ventaServicio = ventaServicio;
            dgvDatos.DataSource = _bindingSource;
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void RecargarGrilla()
        {
            try
            {
                var paginaSolicitada = _estadoNavegacion.PaginaActual;
                var resultado = _ventaServicio
                    .ObtenerPagina(paginaSolicitada,
                    _estadoNavegacion.CantidadPorPagina,
                    _estadoNavegacion.FiltroActivo,
                    _estadoNavegacion.TextoBuscar);
                _estadoNavegacion.Actualizar(resultado.TotalRegistros);
                if (paginaSolicitada != _estadoNavegacion.PaginaActual)
                {
                    resultado = _ventaServicio
                        .ObtenerPagina(_estadoNavegacion.PaginaActual,
                        _estadoNavegacion.CantidadPorPagina,
                        _estadoNavegacion.FiltroActivo,
                        _estadoNavegacion.TextoBuscar);
                    _estadoNavegacion.Actualizar(resultado.TotalRegistros);
                }
                MostrarDatosEnGrilla(resultado);
            }
            catch (Exception ex)
            {

                _servicioMensajes.Error(ex.Message);
            }
        }


        private void MostrarDatosEnGrilla(ResultadoPaginacionDto<VentaListDto> resultado)
        {

            _bindingSource.DataSource = resultado.Items;
            dgvDatos.DataSource = _bindingSource;

            lblCantidad.Text = _estadoNavegacion.TextoRegistros();
            lblPaginas.Text = _estadoNavegacion.TextoPaginas();

            btnPrimero.Enabled = resultado.TieneRegistrosAnteriores;
            btnAnterior.Enabled = resultado.TieneRegistrosAnteriores;
            btnSiguiente.Enabled = resultado.TieneRegistrosSiguientes;
            btnUltimo.Enabled = resultado.TieneRegistrosSiguientes;

        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                RecargarGrilla();

            }
            catch (Exception ex)
            {

                _servicioMensajes.Error(ex.Message);
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            _estadoNavegacion.IrPrimera();
            RecargarGrilla();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _estadoNavegacion.IrAnterior();
            RecargarGrilla();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _estadoNavegacion.IrSiguiente();
            RecargarGrilla();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            _estadoNavegacion.IrUltima();
            RecargarGrilla();
        }

        private void tsbDetalles_Click(object sender, EventArgs e)
        {
            VentaListDto? ventaDto = _bindingSource.ObtenerSeleccionado<VentaListDto>(_servicioMensajes);
            if (ventaDto is null) return;
            VentaDetailDto? ventaDetailDto = _ventaServicio.ObtenerPorId(ventaDto.VentaId);
            if (ventaDetailDto is null) return;
            using (frmDetalleVenta frm = _serviceProvider.GetRequiredService<frmDetalleVenta>())
            {
                frm.Text = "Detalles de Caja";
                frm.SetVenta(ventaDetailDto);
                frm.MostrarConInactividad(this);
            }

        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (frmVentaAe frm = _serviceProvider.GetRequiredService<frmVentaAe>())
            {
                frm.Text = "Nueva Venta";
                if (frm.MostrarConInactividad(this) != DialogResult.OK) return;
                VentaCreateDto? ventaDto = frm.GetVenta();
                if (ventaDto == null) return;
                try
                {

                    int nuevoId = _ventaServicio.Agregar(ventaDto);
                    RecargarGrilla();
                    _estadoNavegacion.IrUltima();
                    _servicioMensajes.Informacion("Registro agregado");

                }
                catch (Exception ex)
                {
                    _servicioMensajes.Error(ex.Message);
                }
            }

        }

        private void tsbAnular_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            var ventaDto = _bindingSource.ObtenerSeleccionado<VentaListDto>(
    _servicioMensajes);
            if (ventaDto is null) return;
            if (ventaDto.EstadoVenta != Entidades.Enum.EstadoVenta.Pendiente)
            {
                _servicioMensajes.Advertencia("No se puede anular una venta ya facturada o anulada");
                return;
            }
            if (!_servicioMensajes.Confirmar($"¿Desea anular la venta {ventaDto.VentaId}" +
                $"por ${ventaDto.Total}?")) return;
            try
            {
                _ventaServicio.Anular(ventaDto.VentaId);
                _servicioMensajes.Informacion("Venta anulada");
                RecargarGrilla();
            }
            catch (Exception ex)
            {

                _servicioMensajes.Error(ex.Message);
            }

        }

        private void tsbFacturar_Click(object sender, EventArgs e)
        {
            var ventaDto = _bindingSource.ObtenerSeleccionado<VentaListDto>(
                _servicioMensajes);
            if (ventaDto is null) return;
            if (ventaDto.EstadoVenta != Entidades.Enum.EstadoVenta.Pendiente)
            {
                _servicioMensajes.Advertencia("No se puede facturar una venta ya facturada o anulada");
                return;
            }
            if (!_servicioMensajes.Confirmar($"¿Desea facturar la venta {ventaDto.VentaId}" +
                $"por ${ventaDto.Total}?")) return;
            try
            {
                _ventaServicio.Facturar(ventaDto.VentaId);
                _servicioMensajes.Informacion("Venta facturada");
                RecargarGrilla();
            }
            catch (Exception ex)
            {

                _servicioMensajes.Error(ex.Message);
            }
        }
=======

        }
>>>>>>> e2c6ebfbf918253cf98422ea07e6341e65f0964c
    }
}
