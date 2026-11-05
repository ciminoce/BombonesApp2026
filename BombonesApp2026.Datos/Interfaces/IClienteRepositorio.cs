using BombonesApp2026.Entidades.Entidades;

namespace BombonesApp2026.Datos.Interfaces
{
    public interface IClienteRepositorio:IRepositorio<Cliente>
    {
        bool TieneRegistrosRelacionados(int clienteId);
        Cliente? ObtenerPorId(int clienteId, bool tracked = false);
        bool ExisteCliente(Cliente cliente);
        (List<Cliente> lista, int cantidadRegistros) ObtenerPagina(int paginaActual, int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null);
        int ObtenerPosicionAlfabetica(string nombre, bool? filtroActivo = null, string? textoBuscar = null);
    }
}