using BombonesApp2026.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class RolRepositorio
    {
        public List<Rol> ObtenerTodos()
        {
            using (var context = new BombonesDbContext())
            {
                return context.Roles.ToList();
            }
        }
        public List<Rol> FiltrarPorActivo(bool activo)
        {
            using (var context = new BombonesDbContext())
            {
                return context.Roles
                    .Where(r => r.Activo == activo)
                    .ToList();
            }
        }
        public void Agregar(Rol rol)
        {
            using (var context = new BombonesDbContext())
            {
                context.Roles.Add(rol);
                context.SaveChanges();
            }
        }
        public void Editar(Rol rol)
        {
            using (var context = new BombonesDbContext())
            {
                //context.Entry(rol).State = EntityState.Modified;
                var rolEnDb = context.Roles.Find(rol.RolId);
                if (rolEnDb is null) throw new Exception("Rol no encontrado");
                rolEnDb.Nombre = rol.Nombre;
                rolEnDb.Descripcion = rol.Descripcion;
                rolEnDb.Activo = rol.Activo;
                context.SaveChanges();
            }
        }
        public void Borrar(int id)
        {
            using (var context = new BombonesDbContext())
            {
                var rolEnDb = context.Roles.Find(id);
                if (rolEnDb is null) throw new Exception("Rol no encontrado");
                context.Roles.Remove(rolEnDb);
                context.SaveChanges();
            }
        }
        public Rol? ObtenerPorId(int id)
        {
            using (var context=new BombonesDbContext())
            {
                return context.Roles
                    .FirstOrDefault(r => r.RolId == id);
            }
        }

        public bool ExisteRol(Rol rol)
        {
            using (var context=new BombonesDbContext())
            {
                if (rol.RolId==0)
                {
                    return context.Roles.Any(r => r.Nombre == rol.Nombre);
                }
                else
                {
                    return context.Roles.Any(r=>r.Nombre==rol.Nombre &&
                            r.RolId!=rol.RolId);
                }
            }
        }

        public bool TieneRegistrosRelacionados(int rolId)
        {
            return false;
        }
    }
}
