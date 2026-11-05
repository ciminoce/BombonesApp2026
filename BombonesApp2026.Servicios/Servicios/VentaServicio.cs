using Bombones2026.Servicios.DTOs.Paginacion;
using BombonesApp2026.Datos;
using BombonesApp2026.Datos.Interfaces;
using BombonesApp2026.Entidades.Entidades;
using BombonesApp2026.Entidades.Enum;
using BombonesApp2026.Servicios.DTOs.DetalleVenta;
using BombonesApp2026.Servicios.DTOs.Venta;
using BombonesApp2026.Servicios.Excepciones;
using BombonesApp2026.Servicios.Interfaces;
using BombonesApp2026.Servicios.Mapeadores;

namespace BombonesApp2026.Servicios.Servicios
{
    public class VentaServicio : IVentaServicio
    {
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IFormaDePagoRepositorio _formaDePagoRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public VentaServicio(IVentaRepositorio ventaRepositorio,
            IClienteRepositorio clienteRepositorio,
            IFormaDePagoRepositorio formaDePagoRepositorio,
            IProductoRepositorio productoRepositorio,
            IUnitOfWork unitOfWork)
        {
            _ventaRepositorio = ventaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _formaDePagoRepositorio = formaDePagoRepositorio;
            _productoRepositorio = productoRepositorio;
            _unitOfWork = unitOfWork;
        }

        public List<VentaListDto> ObtenerTodos()
        {
            try
            {
                return _ventaRepositorio.ObtenerTodos()
                    .Select(v => v.ToListDto()).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar obtener la lista de ventas {ex.Message}");
            }
        }
        public ResultadoPaginacionDto<VentaListDto> ObtenerPagina(int paginaActual,
            int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null)
        {
            try
            {
                var resultado = _ventaRepositorio.ObtenerPagina(paginaActual,
                    cantidadPorPagina, filtroActivo, textoBuscar);
                var listaDto = resultado.lista
                        .Select(b => b.ToListDto()).ToList();
                return new ResultadoPaginacionDto<VentaListDto>
                {
                    Items = listaDto,
                    TotalRegistros = resultado.cantidadRegistros,
                    CantidadPorPagina = cantidadPorPagina,
                    PaginaActual = paginaActual
                };
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar obtener la página de ventas {ex.Message}");
            }
        }

        public VentaDetailDto? ObtenerPorId(int ventaId)
        {
            try
            {
                var venta = _ventaRepositorio.ObtenerPorId(ventaId);
                if (venta is null)
                    throw new KeyNotFoundException($"La venta ID {ventaId} no fue encontrada");
                return venta.ToDetailDto();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar obtener el detalle de Venta {ex.Message}");
            }
        }

        public int Agregar(VentaCreateDto ventaDto)
        {
            var errores = new List<string>();
            var productosAgregar = new List<(Producto producto,
                int cantidad)>();
            errores.AddRange(ValidarVentaDto(ventaDto));
            errores.AddRange(ValidarDetalleVentaDto(ventaDto.Detalle,
                productosAgregar));
            if (errores.Any())
            {
                throw new ValidarVentaExcepcion(errores);
            }
            var venta = new Venta(ventaDto.ClienteId, ventaDto.FormaDePagoId);
            //foreach (var productoDetalle in ventaDto.Detalle)
            //{
            //    var producto = _productoRepositorio
            //    .ObtenerPorId(productoDetalle.ProductoId, true);
            //    venta.AgregarDetalle(productoDetalle.ProductoId,
            //        productoDetalle.Cantidad,
            //        productoDetalle.Precio);
            //    producto!.Stock -= productoDetalle.Cantidad;

            //}
            foreach (var item in productosAgregar)
            {
                venta.AgregarDetalle(item.producto.ProductoId,
                       item.cantidad,
                       item.producto.Precio);
                item.producto.Stock -= item.cantidad;
            }
            try
            {
                if (!venta.TieneDetalles())
                {
                    throw new InvalidOperationException("La venta no tiene detalles");
                }
                _ventaRepositorio.Agregar(venta);
                _unitOfWork.Guardar();
                return venta.VentaId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar guardar una venta {ex.Message}", ex);
            }
        }

        private IEnumerable<string> ValidarDetalleVentaDto(List<DetalleVentaDto> detalle, List<(Producto, int)> productosAgregar)
        {
            var errores = new List<string>();
            foreach (var productoDetalle in detalle)
            {
                if (productoDetalle.ProductoId <= 0)
                {
                    errores.Add("El ID del producto debe ser mayor a cero");
                }
                //var producto = _productoRepositorio
                //    .ObtenerPorId(productoDetalle.ProductoId, false);
                var producto = _productoRepositorio
                    .ObtenerPorId(productoDetalle.ProductoId, true);

                if (producto is null)
                {
                    errores.Add($"El producto ID {productoDetalle.ProductoId}" +
                        $"no existe");
                }
                else if (!producto.Activo)
                {
                    errores.Add($"El producto ID {productoDetalle.ProductoId}" +
                        $"no se encuentra activo");

                }
                else if (producto.Stock < productoDetalle.Cantidad)
                {
                    errores.Add($"El producto {producto.Nombre} no tiene stock suficiente");
                }
                productosAgregar.Add((producto!, productoDetalle.Cantidad));
            }
            return errores;
        }

        private IEnumerable<string> ValidarVentaDto(VentaCreateDto ventaDto)
        {
            var errores = new List<string>();
            if (ventaDto is null)
            {
                errores.Add("La venta es nula");
            }
            if (ventaDto!.ClienteId < 0)
            {
                errores.Add($"El ID del cliente " +
                    $"no puede ser negativo {ventaDto.ClienteId}");
            }
            var cliente = _clienteRepositorio
                .ObtenerPorId(ventaDto.ClienteId, false);
            if (cliente is null)
            {
                errores.Add($"El cliente Id {ventaDto.ClienteId}" +
                    $"no existe");
            }
            if (ventaDto.FormaDePagoId <= 0)
            {
                errores.Add("El ID de la forma de pago debe ser positivo");
            }
            var formaDePago = _formaDePagoRepositorio
                .ObtenerPorId(ventaDto.FormaDePagoId, false);
            if (formaDePago is null)
            {
                errores.Add($"La forma de Pago ID {ventaDto.FormaDePagoId}" +
                    $"no existe");
            }
            if (ventaDto.Detalle is null ||
                ventaDto.Detalle.Count == 0)
            {
                errores.Add("La venta no tiene detalles");
            }
            return errores;
        }

        public void Facturar(int ventaId)
        {
            if (ventaId <= 0)
            {
                throw new ArgumentOutOfRangeException("El ID de la venta debe ser positivo");
            }
            var venta = _ventaRepositorio.ObtenerPorId(ventaId, true);
            if(venta is null)
            {
                throw new ArgumentNullException($"La Venta ID {ventaId} no existe");
            }
            if (venta.EstadoVenta != EstadoVenta.Pendiente)
            {
                throw new InvalidOperationException("La venta no está pendiente de ser facturada");
            }
            try
            {
                venta.Facturar();
                _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar facturar una venta {ex.Message}",ex);
            }
        }

        public void Anular(int ventaId)
        {
            if (ventaId <= 0)
            {
                throw new ArgumentOutOfRangeException("El ID de la venta debe ser positivo");
            }
            var venta = _ventaRepositorio.ObtenerPorId(ventaId, true);
            if (venta is null)
            {
                throw new ArgumentNullException($"La Venta ID {ventaId} no existe");
            }
            if (venta.EstadoVenta != EstadoVenta.Pendiente)
            {
                throw new InvalidOperationException("La venta no está pendiente de ser facturada");
            }
            try
            {
                venta.Anular();
                foreach (var itemVenta in venta.Detalles)
                {
                    var producto = _productoRepositorio.ObtenerPorId(
                        itemVenta.ProductoId, true);
                    if(producto is null)
                    {
                        throw new KeyNotFoundException("Producto no encontrado");
                    }
                    producto.Stock += itemVenta.Cantidad;
                }
                _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar anular una venta {ex.Message}", ex);
            }

        }
    }
}
