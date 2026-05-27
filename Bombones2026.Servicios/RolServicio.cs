using Bombones2026.Servicios.DTOs.Rol;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades;

namespace Bombones2026.Servicios
{
    //TODO: ajustar las excepciones
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
                throw new ArgumentNullException("El rol no puede ser nulo");
            if (string.IsNullOrEmpty(rolDto.Nombre))
                throw new ArgumentException("El nombre del rol es requerido");
            Rol rol = new Rol
            {
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Activo = true//Nuevo roles son activos por defecto
            };
            if (_rolRepositorio.ExisteRol(rol)) throw new ArgumentException("Rol duplicado");
            _rolRepositorio.Agregar(rol);
        }
        public void Borrar(int rolId)
        {
            if (_rolRepositorio.TieneRegistrosRelacionados(rolId))
            {
                throw new Exception("Rol con registros relacionados");
            }
            _rolRepositorio.Borrar(rolId);
        }

        public void Editar(RolEditDto? rolDto)
        {
            if (rolDto == null)
                throw new ArgumentNullException("El rol no puede ser nulo");
            if (string.IsNullOrEmpty(rolDto.Nombre))
                throw new ArgumentException("El nombre del rol es requerido");
            Rol rol = new Rol
            {
                RolId = rolDto.RolId,
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Activo = rolDto.Activo
            };
            if (_rolRepositorio.ExisteRol(rol)) throw new ArgumentException("Rol duplicado");
            _rolRepositorio.Editar(rol);
        }
        public RolEditDto GetForUpdate(int id)
        {
            Rol? rol = _rolRepositorio.ObtenerPorId(id);
            if (rol is null) throw new ArgumentException($"Id {id} no encontrado");
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
