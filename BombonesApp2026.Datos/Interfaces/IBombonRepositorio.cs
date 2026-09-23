using BombonesApp2026.Entidades.Entidades;

namespace BombonesApp2026.Datos.Interfaces
{
    public interface IBombonRepositorio
    {
        void Agregar(Bombon Bombon);
        void Borrar(int ProductoId);
        void Editar(Bombon bombon);
        bool ExisteBombon(Bombon Bombon);
        (List<Bombon> lista, int cantidadRegistros) ObtenerPagina(int paginaActual, int cantidadPorPagina, bool? filtroActivo = null, string? textoBuscar = null);
        Bombon? ObtenerPorId(int ProductoId);
        int ObtenerPosicionAlfabetica(string nombre, bool? filtroActivo = null, string? textoBuscar = null);
        List<Bombon> ObtenerTodos();
    }
}