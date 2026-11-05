using BombonesApp2026.Datos.Interfaces;
using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class ClienteRepositorio :Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(BombonesDbContext context) : base(context)
        {
        }

        public override List<Cliente> ObtenerTodos()
        {
            return _context.Clientes
                .Include(c => c.Ciudad)
                .ThenInclude(ci => ci.Provincia)
                .OrderBy(c=>c.ClienteId==0?0:1)// Si es el Consumidor Final (ID 0), va primero (0); los demás van después (1)
                .ThenBy(c => c.Apellido).ThenBy(c => c.Nombre)// El resto se ordena alfabéticamente por nombre
                .ToList();
        }
        public (List<Cliente> lista, int cantidadRegistros) ObtenerPagina(int paginaActual,
    int cantidadPorPagina, bool? filtroActivo = null,
    string? textoBuscar = null)
        {
            IQueryable<Cliente> query = _context
                .Clientes
                .Include(c => c.Ciudad)
                .ThenInclude(ci => ci.Provincia)
                .AsNoTracking();
            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                query = query.Where(c => c.Nombre.Contains(textoBuscar));
            }
            var cantidad = query.Count();
            var lista = query
                .OrderBy(c => c.Apellido).ThenBy(c => c.Nombre)
                .Skip(cantidadPorPagina * (paginaActual - 1))
                .Take(cantidadPorPagina)
                .ToList();
            return (lista, cantidad);
        }
        public int ObtenerPosicionAlfabetica(string nombre,
                bool? filtroActivo = null, string? textoBuscar = null)
        {
            IQueryable<Cliente> query = _context.Clientes.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                query = query.Where(c => c.Nombre.Contains(textoBuscar));
            }
            return query
                .Count(c => string
                    .Compare(c.Nombre, nombre) <= 0);
        }



        public Cliente? ObtenerPorId(int clienteId, bool tracked=false)
        {
            IQueryable<Cliente> query = _context.Clientes
                .Include(c => c.Ciudad)
                .ThenInclude(ci => ci.Provincia);
            if (tracked)
            {
                return query.FirstOrDefault(c => c.ClienteId == clienteId);
            }
            return query.AsNoTracking()
                .FirstOrDefault(c => c.ClienteId == clienteId);
        }


        public bool ExisteCliente(Cliente cliente)
        {
            return _context.Clientes
                .Any(c => c.Documento == cliente.Documento 
                && c.ClienteId!=cliente.ClienteId);
        }

        public bool TieneRegistrosRelacionados(int clienteId)
        {
            return _context.Ventas.Any(v => v.ClienteId == clienteId);
        }

    }
}
