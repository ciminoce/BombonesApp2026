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
            throw new NotImplementedException();
        }

        public void Editar(Transporte transporte)
        {
            throw new NotImplementedException();
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

        public object ObtenerPorId(int transporteId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
