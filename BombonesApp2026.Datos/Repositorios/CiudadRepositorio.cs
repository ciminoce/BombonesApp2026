using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class CiudadRepositorio
    {
        public List<Ciudad> ObtenerTodos()
        {
            using (var context=new BombonesDbContext())
            {
                return context.Ciudades
                    .Include(c=>c.Provincia)
                    .ToList();
            }
        }
        public void Agregar(Ciudad ciudad)
        {
            using (var context=new BombonesDbContext())
            {
                context.Ciudades.Add(ciudad);
                context.SaveChanges();
            }
        }

        public bool ExisteCiudad(Ciudad ciudad)
        {
            using (var context=new BombonesDbContext())
            {
                if (ciudad.CiudadId == 0)
                {
                    return context.Ciudades.Any(c => c.Nombre == ciudad.Nombre
                        && c.ProvinciaId == ciudad.ProvinciaId);
                }
                else
                {
                    return context.Ciudades.Any(c => c.Nombre == ciudad.Nombre
                        && c.ProvinciaId == ciudad.ProvinciaId 
                        && c.CiudadId!=ciudad.CiudadId);

                }
            }
        }
        public void Borrar(int ciudadId)
        {
            using (var context=new BombonesDbContext())
            {
                var ciudadEnDb = context.Ciudades.Find(ciudadId);
                if (ciudadEnDb is null) throw new KeyNotFoundException($"No se encuentra una ciudad con ID: {ciudadId}");
                context.Ciudades.Remove(ciudadEnDb);
                context.SaveChanges();
            }
        }

        public Ciudad? ObtenerPorId(int ciudadId)
        {
            using (var context=new BombonesDbContext() )
            {
                return context.Ciudades
                    .FirstOrDefault(c => c.CiudadId == ciudadId);
            }
        }

        public void Editar(Ciudad ciudad)
        {
            using (var context = new BombonesDbContext())
            {

                var ciudadEnDb = context.Ciudades.Find(ciudad.CiudadId);

                if (ciudadEnDb is null) throw new Exception("Ciudad no encontrada");
                ciudadEnDb.Nombre = ciudad.Nombre;
                ciudadEnDb.ProvinciaId = ciudad.ProvinciaId;

                context.SaveChanges();

            }
        }
    }
}
