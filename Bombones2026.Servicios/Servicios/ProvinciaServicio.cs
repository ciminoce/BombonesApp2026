using Bombones2026.Servicios.DTOs.FormaDePago;
using Bombones2026.Servicios.DTOs.Provincia;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades.Entidades;

namespace Bombones2026.Servicios.Servicios
{
    public class ProvinciaServicio
    {
        private readonly ProvinciaRepositorio _provinciaRepositorio;
        public ProvinciaServicio()
        {
            _provinciaRepositorio = new ProvinciaRepositorio();
        }

        public List<ProvinciaListDto> ObtenerTodos()
        {
            return _provinciaRepositorio.ObtenerTodos()
                .Select(p => new ProvinciaListDto           
                {
                    ProvinciaId = p.ProvinciaId,
                    Nombre = p.NombreProvincia,
                }).ToList();
        }
        public int Agregar(ProvinciaCreateDto? provinciaDto)
        {
            if (provinciaDto == null)
                throw new ArgumentNullException(nameof(provinciaDto), "La provincia no puede ser nula");
            if (string.IsNullOrWhiteSpace(provinciaDto.Nombre))
                throw new ArgumentException(nameof(provinciaDto.Nombre), "El nombre de la provincia es requerido");
            Provincia provincia = new Provincia
            {
                NombreProvincia = provinciaDto.Nombre,
            };
            if (_provinciaRepositorio.ExisteProvincia(provincia)) throw new InvalidCastException($"Ya existe una Provincia {provincia.NombreProvincia}");
            try
            {
                _provinciaRepositorio.Agregar(provincia);
                return provincia.ProvinciaId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al intentar guardar una provincia {ex.Message}");
            }
        }
        public void Borrar(int provinciaId)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (provinciaId <= 0)
                throw new ArgumentException("El ID de la provincia debe ser un entero mayor a cero.", nameof(provinciaId));
            // 2. NUEVO AJUSTE: Verificar existencia real en el sistema antes que cualquier otra cosa
            // Nota: Asumiendo que tu repositorio tiene un método Existe(id) o similar
            var provinciaInDb = _provinciaRepositorio.ObtenerPorId(provinciaId);
            if (provinciaInDb is null)
            {
                throw new KeyNotFoundException($"No se puede borrar. No existe ninguna provincia con el ID {provinciaId}.");
            }

            // AJUSTE: Se cambia Exception genérica por InvalidOperationException
            if (_provinciaRepositorio.TieneRegistrosRelacionados(provinciaId))
            {
                throw new InvalidOperationException($"No se puede eliminar la provincia (ID: {provinciaId}) porque tiene registros relacionados en el sistema.");
            }
            _provinciaRepositorio.Borrar(provinciaId);
        }

        public void Editar(ProvinciaEditDto? provinciaDto)
        {
            if (provinciaDto == null)
                throw new ArgumentNullException(nameof(provinciaDto), "La provincia no puede ser nula");
            if (string.IsNullOrWhiteSpace(provinciaDto.Nombre))
                throw new ArgumentException(nameof(provinciaDto.Nombre), "El nombre de la provincia es requerido");
            Provincia provincia = new Provincia
            {
                ProvinciaId = provinciaDto.ProvinciaId,
                NombreProvincia = provinciaDto.Nombre  
            };
            if (_provinciaRepositorio.ExisteProvincia(provincia)) throw new InvalidOperationException($"Ya existe una Provincia {provincia.NombreProvincia}");
            _provinciaRepositorio.Editar(provincia);
        }
        public ProvinciaEditDto ObtenerParaEditar(int id)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (id <= 0)
                throw new ArgumentException("El ID de la provincia debe ser un entero mayor a cero.", nameof(id));


            Provincia? provincia = _provinciaRepositorio.ObtenerPorId(id);
            if (provincia is null) throw new ArgumentException(nameof(id), $"Id {id} no encontrado");
            ProvinciaEditDto provinciaDto = new ProvinciaEditDto
            {
                ProvinciaId = provincia.ProvinciaId,
                Nombre = provincia.NombreProvincia
            };
            return provinciaDto;
        }

    }

}
