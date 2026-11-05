using Bombones2026.Servicios.DTOs.Paginacion;
using BombonesApp2026.Entidades.Enum;
using BombonesApp2026.Servicios.DTOs.Cliente;

namespace BombonesApp2026.Servicios.Interfaces
{
    public interface IClienteServicio
    {
        int Agregar(ClienteCreateDto clienteDto);
        void Borrar(int clienteId);
        void Editar(ClienteEditDto clienteDto);
        List<ClienteListDto> ObtenerDatosCombo(ClienteDefault clienteDfault);
        ResultadoPaginacionDto<ClienteListDto> ObtenerPagina(int paginaActual, int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null);
        int ObtenerPaginaRegistro(string documento, int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null);
        ClienteEditDto? ObtenerParaEditar(int clienteId);
        List<ClienteListDto> ObtenerTodos();
        bool TieneRegistrosRelacionados(int clienteId);
    }
}