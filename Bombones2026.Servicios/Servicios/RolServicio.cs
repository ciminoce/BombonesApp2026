using Bombones2026.Servicios.DTOs.Rol;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades.Interfaces;

namespace Bombones2026.Servicios.Servicios
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
                throw new ArgumentNullException(nameof(rolDto), "El rol no puede ser nulo");
            if (string.IsNullOrWhiteSpace(rolDto.Nombre))
                throw new ArgumentException(nameof(rolDto.Nombre), "El nombre del rol es requerido");
            Rol rol = new Rol
            {
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Activo = true//Nuevo roles son activos por defecto
            };
            if (_rolRepositorio.ExisteRol(rol)) throw new InvalidCastException($"Ya existe un Rol {rol.Nombre}");
            _rolRepositorio.Agregar(rol);
        }
        public void Borrar(int rolId)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (rolId <= 0)
                throw new ArgumentException("El ID del rol debe ser un entero mayor a cero.", nameof(rolId));
            // 2. NUEVO AJUSTE: Verificar existencia real en el sistema antes que cualquier otra cosa
            // Nota: Asumiendo que tu repositorio tiene un método Existe(id) o similar
            var rolInDb = _rolRepositorio.ObtenerPorId(rolId);
            if (rolInDb is null)
            {
                throw new KeyNotFoundException($"No se puede borrar. No existe ningún rol con el ID {rolId}.");
            }

            // AJUSTE: Se cambia Exception genérica por InvalidOperationException
            if (_rolRepositorio.TieneRegistrosRelacionados(rolId))
            {
                throw new InvalidOperationException($"No se puede eliminar el rol (ID: {rolId}) porque tiene registros relacionados en el sistema.");
            }
            _rolRepositorio.Borrar(rolId);
        }

        public void Editar(RolEditDto? rolDto)
        {
            if (rolDto == null)
                throw new ArgumentNullException(nameof(rolDto), "El rol no puede ser nulo");
            if (string.IsNullOrWhiteSpace(rolDto.Nombre))
                throw new ArgumentException(nameof(rolDto.Nombre), "El nombre del rol es requerido");
            Rol rol = new Rol
            {
                RolId = rolDto.RolId,
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Activo = rolDto.Activo
            };
            if (_rolRepositorio.ExisteRol(rol)) throw new InvalidOperationException($"Ya existe un Rol {rol.Nombre}");
            _rolRepositorio.Editar(rol);
        }
        public RolEditDto GetForUpdate(int id)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (id <= 0)
                throw new ArgumentException("El ID del rol debe ser un entero mayor a cero.", nameof(id));


            Rol? rol = _rolRepositorio.ObtenerPorId(id);
            if (rol is null) throw new ArgumentException(nameof(id), $"Id {id} no encontrado");
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
