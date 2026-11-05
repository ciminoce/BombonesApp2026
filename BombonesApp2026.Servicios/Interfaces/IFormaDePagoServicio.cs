using Bombones2026.Servicios.DTOs.FormaDePago;
using Bombones2026.Servicios.DTOs.Paginacion;
using BombonesApp2026.Entidades.Enum;

namespace BombonesApp2026.Servicios.Interfaces
{
    public interface IFormaDePagoServicio
    {
        int Agregar(FormaDePagoCreateDto? formaDePagoDto);
        void Borrar(int formaDePagoId);
        void Editar(FormaDePagoEditDto? formaDePagoDto);
        List<FormaDePagoListDto> ObtenerDatosCombo(FormaDePagoDefault formaDePagoDefault);

        List<FormaDePagoListDto> FiltrarPorActivo(bool activo);
        ResultadoPaginacionDto<FormaDePagoListDto> ObtenerPagina(int paginaActual, int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null);
        int ObtenerPaginaRegistro(string nombre, int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null);
        FormaDePagoEditDto ObtenerParaEditar(int id);
        List<FormaDePagoListDto> ObtenerTodos();
        bool TieneRegistrosRelacionados(int formaDePagoId);
    }
}