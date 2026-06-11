using BombonesApp2026.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BombonesApp2026.Datos.Repositorios
{
    public class TransporteRepositorio
    {
        public void Agregar(Transporte transporte)
        {
            using (var context=new BombonesDbContext())
            {
                context.Transportes.Add(transporte);
                context.SaveChanges();
            }
        }

        public void Borrar(int transporteId)
        {
            using (var context = new BombonesDbContext())
            {
                var transporteEnDb = context.Transportes
                    .Find(transporteId);
                if (transporteEnDb is null) throw new Exception("Transporte no encontrado");
                context.Transportes.Remove(transporteEnDb);
                context.SaveChanges();
            }
        }

        public void Editar(Transporte transporte)
        {
            using (var context = new BombonesDbContext())
            {

                var transporteEnDb = context.Transportes.Find(transporte.TransporteId);

                if (transporteEnDb is null) throw new Exception("Transporte no encontrado");
                transporteEnDb.NombreEmpresa = transporte.NombreEmpresa;
                transporteEnDb.Telefono = transporte.Telefono;
                transporteEnDb.Email= transporte.Email;
                transporteEnDb.Activo = transporte.Activo;

                context.SaveChanges();

            }
        }

        public bool ExisteTransporte(Transporte transporte)
        {
            using (var context=new BombonesDbContext())
            {
                if (transporte.TransporteId == 0)
                {
                    return context.Transportes
                        .Any(t=>t.NombreEmpresa==transporte.NombreEmpresa && 
                        t.ProvinciaId == transporte.ProvinciaId);
                }
                else
                {
                    return context.Transportes
                        .Any(t => t.NombreEmpresa == transporte.NombreEmpresa &&
                        t.ProvinciaId == transporte.ProvinciaId && 
                        t.TransporteId!=transporte.TransporteId);

                }
            }
        }

        public Transporte? ObtenerPorId(int transporteId)
        {
            using (var context = new BombonesDbContext())
            {
                return context.Transportes.AsNoTracking()
                    .FirstOrDefault(t => t.TransporteId == transporteId);
            }
        }

        public List<Transporte> ObtenerTodos()
        {
            using (var context=new BombonesDbContext())
            {
                return context.Transportes
                    .Include(t=>t.Provincia)
                    .ToList();
            }
        }

        public bool TieneRegistrosRelacionados(int transporteId)
        {
            return false;
        }
    }
}
