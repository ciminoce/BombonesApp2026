using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BombonesApp2026.Datos.Repositorios
{
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
                provinciaEnDb.Nombre = provincia.Nombre;

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
                    return context.Provincias.Any(p => p.Nombre == provincia.Nombre);
                }
                else
                {
                    return context.Provincias.Any(p => p.Nombre == provincia.Nombre &&
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
