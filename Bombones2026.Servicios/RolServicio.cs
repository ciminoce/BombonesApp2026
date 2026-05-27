using Bombones2026.Servicios.DTOs.Rol;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades;

namespace Bombones2026.Servicios
{
    public class RolServicio
    {
        private readonly RolRepositorio _rolRepositorio;
        public RolServicio()
        {
            _rolRepositorio = new RolRepositorio();
        }

        public List<RolListDto> GetLista()
        {
            return _rolRepositorio.ObtenerTodos()
                .Select(r => new RolListDto
                {
                    RolId = r.RolId,
                    Nombre = r.Nombre,
                    Activo = r.Activo,
                }).ToList();
        }
        public void Agregar(RolCreateDto? rolDto)
        {
            if (rolDto == null)
                throw new ArgumentNullException(nameof(rolDto),"El rol no puede ser nulo");
            if (string.IsNullOrWhiteSpace(rolDto.Nombre))
                throw new ArgumentException(nameof(rolDto.Nombre),"El nombre del rol es requerido");
            Rol rol = new Rol
            {
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Activo = true//Nuevo roles son activos por defecto
            };
            if (_rolRepositorio.ExisteRol(rol)) throw new InvalidOperationException($"Ya existe un rol {rol.Nombre}");
            _rolRepositorio.Agregar(rol);
        }
        public void Borrar(int rolId)
        {
            if (rolId<=0)
            {
                throw new ArgumentException(nameof(rolId), "El id debe ser positivo");
            }
            var rolEnDb=_rolRepositorio.ObtenerPorId(rolId);
            if (rolEnDb is null)
            {
                throw new KeyNotFoundException($"No existe rol con Id {rolId}");
            }
            if (_rolRepositorio.TieneRegistrosRelacionados(rolId))
            {
                throw new InvalidOperationException("Rol con registros relacionados");
            }
            _rolRepositorio.Borrar(rolId);
        }

        public void Editar(RolEditDto? rolDto)
        {
            if (rolDto == null)
                throw new ArgumentNullException(nameof(rolDto),"El rol no puede ser nulo");
            if (string.IsNullOrWhiteSpace(rolDto.Nombre))
                throw new ArgumentException(nameof(rolDto.Nombre),"El nombre del rol es requerido");
            Rol rol = new Rol
            {
                RolId = rolDto.RolId,
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Activo = rolDto.Activo
            };
            if (_rolRepositorio.ExisteRol(rol)) throw new InvalidOperationException($"Ya existe un rol {rol.Nombre}");
            _rolRepositorio.Editar(rol);
        }
        public RolEditDto GetForUpdate(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id), "El id debe ser positivo");
            }

            Rol? rol = _rolRepositorio.ObtenerPorId(id);
            if (rol is null) throw new KeyNotFoundException($"Id {id} no encontrado");
            RolEditDto rolDto = new RolEditDto
            {
                RolId = rol.RolId,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                Activo = rol.Activo
            };
            return rolDto;
        }
        public List<RolListDto> GetPorActivo(bool activo)
        {
            return _rolRepositorio.FiltrarPorActivo(activo)
                .Select(r => new RolListDto
                {
                    RolId = r.RolId,
                    Nombre = r.Nombre,
                    Activo = r.Activo
                }).ToList();
        }
    }
}
