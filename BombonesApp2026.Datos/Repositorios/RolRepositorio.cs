using BombonesApp2026.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BombonesApp2026.Datos.Repositorios
{
    public class RolRepositorio
    {
        //TODO: Ojo explicar el no seguiento en las consultas
        public List<Rol> ObtenerTodos()
        {
            using (var context = new BombonesDbContext())
            {
                //var lista = context.Roles
                //    .AsNoTracking()
                //    .ToList();
                //return lista;
                return context.Roles
                    .AsNoTracking()
                    .ToList();
            }
        }
        public List<Rol> FiltrarPorActivo(bool activo)
        {
            using (var context = new BombonesDbContext())
            {
                return context.Roles
                    .AsNoTracking()
                    .Where(r => r.Activo == activo)
                    .ToList();
            }
        }
        public void Agregar(Rol rol)
        {
            using (var context = new BombonesDbContext())
            {
                Debug.WriteLine($"Estado antes de agregar {context.Entry(rol).State}");
                context.Roles.Add(rol);
                Debug.WriteLine($"Estado luego de agregar {context.Entry(rol).State}");

                context.SaveChanges();
                Debug.WriteLine($"Estado luego de guardar {context.Entry(rol).State}");

            }
        }
        public void Editar(Rol rol)
        {
            using (var context = new BombonesDbContext())
            {
                //context.Entry(rol).State = EntityState.Modified;
                Debug.WriteLine($"Estado antes de modificar {context.Entry(rol).State}");

                var rolEnDb = context.Roles.Find(rol.RolId);
                Debug.WriteLine($"Estado antes de modificar y luego de buscar {context.Entry(rolEnDb).State}");

                if (rolEnDb is null) throw new Exception("Rol no encontrado");
                rolEnDb.Nombre = rol.Nombre;
                rolEnDb.Descripcion = rol.Descripcion;
                rolEnDb.Activo = rol.Activo;
                Debug.WriteLine($"Estado antes de persistir {context.Entry(rolEnDb).State}");

                context.SaveChanges();
                Debug.WriteLine($"Estado luego de persistir {context.Entry(rolEnDb).State}");

            }
        }
        public void Borrar(int id)
        {
            using (var context = new BombonesDbContext())
            {
                var rolEnDb = context.Roles
                    .Find(id);
                if (rolEnDb is null) throw new Exception("Rol no encontrado");
                context.Roles.Remove(rolEnDb);
                context.SaveChanges();
            }
        }
        public Rol? ObtenerPorId(int id)
        {
            using (var context = new BombonesDbContext())
            {
                return context.Roles.AsNoTracking()
                    .FirstOrDefault(r => r.RolId == id);
            }
        }

        public bool ExisteRol(Rol rol)
        {
            using (var context = new BombonesDbContext())
            {
                if (rol.RolId == 0)
                {
                    return context.Roles.Any(r => r.Nombre == rol.Nombre);
                }
                else
                {
                    return context.Roles.Any(r => r.Nombre == rol.Nombre &&
                            r.RolId != rol.RolId);
                }
            }
        }

        public bool TieneRegistrosRelacionados(int rolId)
        {
            return false;
        }
    }
}
