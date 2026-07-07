using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class FormaDePagoRepositorio
    {
        public List<FormaDePago> ObtenerTodos()
        {
            using (var context = new BombonesDbContext())
            {
                return context.FormasDePago
                    .AsNoTracking()
                    .ToList();
            }
        }
        public (List<FormaDePago> lista, int cantidadRegistros) ObtenerPagina(int paginaActual,
    int cantidadPorPagina, bool? filtroActivo = null,
    string? textoBuscar = null)
        {
            using (var context = new BombonesDbContext())
            {
                IQueryable<FormaDePago> query = context
                    .FormasDePago.AsNoTracking();
                if (filtroActivo is not null)
                {
                    query = query.Where(f => f.Activo == filtroActivo);
                }
                if (!string.IsNullOrWhiteSpace(textoBuscar))
                {
                    query = query.Where(f => f.Nombre.Contains(textoBuscar));
                }
                var cantidad = query.Count();
                var lista = query
                    .OrderBy(f => f.Nombre)
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
                IQueryable<FormaDePago> query = context.FormasDePago.AsNoTracking();
                if (filtroActivo.HasValue)
                {
                    query = query.Where(f => f.Activo == filtroActivo.Value);
                }
                if (!string.IsNullOrWhiteSpace(textoBuscar))
                {
                    query = query.Where(f => f.Nombre.Contains(textoBuscar));
                }
                return query
                    .Count(f => string
                        .Compare(f.Nombre, nombre) <= 0);
            }
        }

        public List<FormaDePago> FiltrarPorActivo(bool activo)
        {
            using (var context = new BombonesDbContext())
            {
                return context.FormasDePago
                    .AsNoTracking()
                    .Where(f => f.Activo == activo)
                    .ToList();
            }
        }
        public void Agregar(FormaDePago formaDePago)
        {
            using (var context = new BombonesDbContext())
            {
                context.FormasDePago.Add(formaDePago);

                context.SaveChanges();

            }
        }
        public void Editar(FormaDePago formaDePago)
        {
            using (var context = new BombonesDbContext())
            {

                var formaDePagoEnDb = context.FormasDePago.Find(formaDePago.FormaDePagoId);

                if (formaDePagoEnDb is null) throw new Exception("Forma de pago no encontrada");
                formaDePagoEnDb.Nombre = formaDePago.Nombre;
                formaDePagoEnDb.Activo = formaDePago.Activo;

                context.SaveChanges();

            }
        }
        public void Borrar(int id)
        {
            using (var context = new BombonesDbContext())
            {
                var formaDePagoEnDb = context.FormasDePago
                    .Find(id);
                if (formaDePagoEnDb is null) throw new Exception("Forma de pago no encontrada");
                context.FormasDePago.Remove(formaDePagoEnDb);
                context.SaveChanges();
            }
        }
        public FormaDePago? ObtenerPorId(int id)
        {
            using (var context = new BombonesDbContext())
            {
                return context.FormasDePago.AsNoTracking()
                    .FirstOrDefault(f => f.FormaDePagoId == id);
            }
        }

        public bool ExisteFormaDePago(FormaDePago formaDePago)
        {
            using (var context = new BombonesDbContext())
            {
                if (formaDePago.FormaDePagoId == 0)
                {
                    return context.FormasDePago.Any(f => f.Nombre == formaDePago.Nombre);
                }
                else
                {
                    return context.FormasDePago.Any(f => f.Nombre == formaDePago.Nombre &&
                            f.FormaDePagoId != formaDePago.FormaDePagoId);
                }
            }
        }

        public bool TieneRegistrosRelacionados(int formaDePagoId)
        {
            return false;
        }

    }
}
