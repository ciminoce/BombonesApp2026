using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class TipoBombonRepositorio
    {
        public List<TipoBombon> ObtenerTodos()
        {
            using (var context = new BombonesDbContext())
            {
                return context.TipoBombones
                    .AsNoTracking()
                    .ToList();
            }
        }
        public (List<TipoBombon> lista, int cantidadRegistros) ObtenerPagina(int paginaActual, int cantidadPorPagina)
        {
            using(var context=new BombonesDbContext() )
	        {
                var cantidad = context.TipoBombones.Count();
                var lista = context.TipoBombones
                    .AsNoTracking()
                    .OrderBy(tb => tb.Nombre)
                    .Skip(cantidadPorPagina*(paginaActual-1))
                    .Take(cantidadPorPagina)
                    .ToList();
                return (lista, cantidad);
	        }
        }
        public List<TipoBombon> FiltrarPorActivo(bool activo)
        {
            using (var context = new BombonesDbContext())
            {
                return context.TipoBombones
                    .AsNoTracking()
                    .Where(tb => tb.Activo == activo)
                    .ToList();
            }
        }
        public void Agregar(TipoBombon tipoBombon)
        {
            using (var context = new BombonesDbContext())
            {
                context.TipoBombones.Add(tipoBombon);

                context.SaveChanges();

            }
        }
        public void Editar(TipoBombon tipoBombon)
        {
            using (var context = new BombonesDbContext())
            {

                var tipoEnDb = context.TipoBombones.Find(tipoBombon.TipoBombonId);

                if (tipoEnDb is null) throw new Exception("Tipo de Bombon no encontrado");
                tipoEnDb.Nombre = tipoBombon.Nombre;
                tipoEnDb.Descripcion = tipoBombon.Descripcion;
                tipoEnDb.Activo = tipoBombon.Activo;

                context.SaveChanges();

            }
        }
        public void Borrar(int id)
        {
            using (var context = new BombonesDbContext())
            {
                var tipoEnDb = context.TipoBombones
                    .Find(id);
                if (tipoEnDb is null) throw new Exception("Tipo de Bombón no encontrado");
                context.TipoBombones.Remove(tipoEnDb);
                context.SaveChanges();
            }
        }
        public TipoBombon? ObtenerPorId(int id)
        {
            using (var context = new BombonesDbContext())
            {
                return context.TipoBombones.AsNoTracking()
                    .FirstOrDefault(tb => tb.TipoBombonId == id);
            }
        }

        public bool ExisteTipoBombon(TipoBombon tipoBombon)
        {
            using (var context = new BombonesDbContext())
            {
                if (tipoBombon.TipoBombonId == 0)
                {
                    return context.TipoBombones.Any(tb => tb.Nombre == tipoBombon.Nombre);
                }
                else
                {
                    return context.TipoBombones.Any(tb => tb.Nombre == tipoBombon.Nombre &&
                            tb.TipoBombonId != tipoBombon.TipoBombonId);
                }
            }
        }

        public bool TieneRegistrosRelacionados(int tipoId)
        {
            return false;
        }

        public int ObtenerPosicionAlfabetica(string nombre)
        {
            using (var context=new BombonesDbContext())
            {
                return context.TipoBombones
                    .Count(tb => string
                        .Compare(tb.Nombre, nombre) <= 0);
            }
        }
    }
}
