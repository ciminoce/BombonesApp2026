using Bombones2026.Servicios.DTOs.TipoBombon;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades.Interfaces;

namespace Bombones2026.Servicios.Servicios
{
    public class TipoBombonServicio
    {
        private readonly TipoBombonRepositorio _tipoBombonRepositorio;
        public TipoBombonServicio()
        {
            _tipoBombonRepositorio = new TipoBombonRepositorio();
        }
        public List<TipoBombonListDto> ObtenerTodos()
        {
            return _tipoBombonRepositorio.ObtenerTodos()
                .Select(tb => new TipoBombonListDto
                {
                    TipoBombonId = tb.TipoBombonId,
                    Nombre = tb.Nombre,
                    Descripcion = tb.Descripcion,
                    Activo = tb.Activo
                }).ToList();
        }
        public List<TipoBombonListDto> FiltrarPorActivo(bool activo)
        {
            return _tipoBombonRepositorio.FiltrarPorActivo(activo)
                .Select(tb => new TipoBombonListDto
                {
                    TipoBombonId = tb.TipoBombonId,
                    Nombre = tb.Nombre,
                    Descripcion = tb.Descripcion,
                    Activo = tb.Activo
                }).ToList();
        }

        public void Agregar(TipoBombonCreateDto? tipoDto)
        {

            if (tipoDto is null)
            {
                throw new ArgumentNullException(nameof(tipoDto), "El tipo de bombón no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(tipoDto.Nombre))
            {
                throw new ArgumentNullException(nameof(tipoDto.Nombre), "El nombre es requerido");
            }
            TipoBombon tipo = new TipoBombon();
            tipo.Nombre = tipoDto.Nombre;
            tipo.Descripcion = tipoDto.Descripcion;
            tipo.Activo = true;
            if (_tipoBombonRepositorio.ExisteTipoBombon(tipo))
            {
                throw new InvalidOperationException($"Ya existe un tipo de bombón {tipo.Nombre}");
            }
            _tipoBombonRepositorio.Agregar(tipo);
        }

        public void Borrar(int tipoBombonId)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (tipoBombonId <= 0)
                throw new ArgumentException("El ID del tipo de bombón debe ser un entero mayor a cero.", nameof(tipoBombonId));
            // 2. NUEVO AJUSTE: Verificar existencia real en el sistema antes que cualquier otra cosa
            // Nota: Asumiendo que tu repositorio tiene un método Existe(id) o similar
            var tipoBombonInDb = _tipoBombonRepositorio.ObtenerPorId(tipoBombonId);
            if (tipoBombonInDb is null)
            {
                throw new KeyNotFoundException($"No se puede borrar. No existe ningún tipo de bombón con el ID {tipoBombonId}.");
            }

            // AJUSTE: Se cambia Exception genérica por InvalidOperationException
            if (_tipoBombonRepositorio.TieneRegistrosRelacionados(tipoBombonId))
            {
                throw new InvalidOperationException($"No se puede eliminar el tipo de bombón (ID: {tipoBombonId}) porque tiene registros relacionados en el sistema.");
            }
            _tipoBombonRepositorio.Borrar(tipoBombonId);
        }

        public void Editar(TipoBombonEditDto? tipoDto)
        {
            if (tipoDto == null)
                throw new ArgumentNullException(nameof(tipoDto), "El tipo de bombón no puede ser nulo");
            if (string.IsNullOrWhiteSpace(tipoDto.Nombre))
                throw new ArgumentException(nameof(tipoDto.Nombre), "El nombre del tipo de bombón es requerido");
            TipoBombon tipo = new TipoBombon
            {
                TipoBombonId = tipoDto.TipoBombonId,
                Nombre = tipoDto.Nombre,
                Descripcion = tipoDto.Descripcion,
                Activo = tipoDto.Activo
            };
            if (_tipoBombonRepositorio.ExisteTipoBombon(tipo)) throw new InvalidOperationException($"Ya existe un tipo de bombón {tipo.Nombre}");
            _tipoBombonRepositorio.Editar(tipo);
        }
        public TipoBombonEditDto ObtenerParaEditar(int id)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (id <= 0)
                throw new ArgumentException("El ID del tipo de bombón debe ser un entero mayor a cero.", nameof(id));


            TipoBombon? tipo = _tipoBombonRepositorio.ObtenerPorId(id);
            if (tipo is null) throw new ArgumentException(nameof(id), $"Id {id} no encontrado");
            TipoBombonEditDto tipoDto = new TipoBombonEditDto
            {
                TipoBombonId = tipo.TipoBombonId,
                Nombre = tipo.Nombre,
                Descripcion = tipo.Descripcion,
                Activo = tipo.Activo
            };
            return tipoDto;
        }

    }
}
