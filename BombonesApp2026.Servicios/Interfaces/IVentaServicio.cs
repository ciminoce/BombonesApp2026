using Bombones2026.Servicios.DTOs.Paginacion;
using BombonesApp2026.Servicios.DTOs.Venta;

namespace BombonesApp2026.Servicios.Interfaces
{
    public interface IVentaServicio
    {
        ResultadoPaginacionDto<VentaListDto> ObtenerPagina(int paginaSolicitada,
            int cantidadPorPagina, bool? filtroActivo, string? textoBuscar);
        List<VentaListDto> ObtenerTodos();
        VentaDetailDto? ObtenerPorId(int ventaId);
        int Agregar(VentaCreateDto ventaDto);
        void Facturar(int ventaId);
        void Anular(int ventaId);
    }
}
