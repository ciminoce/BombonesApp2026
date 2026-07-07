using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    //TODO:Ver registros relacionadados
    public class ProvinciaRepositorio
    {
        public List<Provincia> ObtenerTodos()
        {
            using (var context = new BombonesDbContext())
            {
                return context.Provincias
                    .AsNoTracking()
                    .ToList();
            }
        }
        public (List<Provincia> lista, int cantidadRegistros) ObtenerPagina(int paginaActual,
            int cantidadPorPagina, bool? filtroActivo = null,
            string? textoBuscar = null)
        {
            using (var context = new BombonesDbContext())
            {
                IQueryable<Provincia> query = context
                    .Provincias.AsNoTracking();
                if (!string.IsNullOrWhiteSpace(textoBuscar))
                {
                    query = query.Where(p => p.NombreProvincia.Contains(textoBuscar));
                }
                var cantidad = query.Count();
                var lista = query
                    .OrderBy(p => p.NombreProvincia)
                    .Skip(cantidadPorPagina * (paginaActual - 1))
                    .Take(cantidadPorPagina)
                    .ToList();
                return (lista, cantidad);
            }
        }
        public int ObtenerPosicionAlfabetica(string nombre,
                bool? filtroActivo = null, string? textoBuscar = null)
        {
            using (var context = new BombonesDbContext())
            {
                IQueryable<Provincia> query = context.Provincias.AsNoTracking();
                if (!string.IsNullOrWhiteSpace(textoBuscar))
                {
                    query = query.Where(p => p.NombreProvincia.Contains(textoBuscar));
                }
                return query
                    .Count(p => string
                        .Compare(p.NombreProvincia, nombre) <= 0);
            }
        }

        public void Agregar(Provincia provincia)
        {
            using (var context = new BombonesDbContext())
            {
                context.Provincias.Add(provincia);

                context.SaveChanges();

            }
        }
        public void Editar(Provincia provincia)
        {
            using (var context = new BombonesDbContext())
            {

                var provinciaEnDb = context.Provincias.Find(provincia.ProvinciaId);

                if (provinciaEnDb is null) throw new Exception("Provincia no encontrada");
                provinciaEnDb.NombreProvincia = provincia.NombreProvincia;

                context.SaveChanges();

            }
        }
        public void Borrar(int id)
        {
            using (var context = new BombonesDbContext())
            {
                var provinciaEnDb = context.Provincias
                    .Find(id);
                if (provinciaEnDb is null) throw new Exception("Provincia no encontrada");
                context.Provincias.Remove(provinciaEnDb);
                context.SaveChanges();
            }
        }
        public Provincia? ObtenerPorId(int id)
        {
            using (var context = new BombonesDbContext())
            {
                return context.Provincias.AsNoTracking()
                    .FirstOrDefault(p => p.ProvinciaId == id);
            }
        }

        public bool ExisteProvincia(Provincia provincia)
        {
            using (var context = new BombonesDbContext())
            {
                if (provincia.ProvinciaId == 0)
                {
                    return context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia);
                }
                else
                {
                    return context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia &&
                            p.ProvinciaId != provincia.ProvinciaId);
                }
            }
        }

        public bool TieneRegistrosRelacionados(int provinciaId)
        {
            return false;
        }

    }
}
