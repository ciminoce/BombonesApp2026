using Bombones2026.Servicios.DTOs.FormaDePago;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades.Entidades;

namespace Bombones2026.Servicios.Servicios
{
    public class FormaDePagoServicio
    {
        private readonly FormaDePagoRepositorio _formasDePagoRepositorio;
        public FormaDePagoServicio()
        {
            _formasDePagoRepositorio = new FormaDePagoRepositorio();
        }

        public List<FormaDePagoListDto> ObtenerTodos()
        {
            return _formasDePagoRepositorio.ObtenerTodos()
                .Select(f => new FormaDePagoListDto
                {
                    FormaDePagoId = f.FormaDePagoId,
                    Nombre = f.Nombre,
                    Activo = f.Activo,
                }).ToList();
        }
        public void Agregar(FormaDePagoCreateDto? formaDePagoDto)
        {
            if (formaDePagoDto == null)
                throw new ArgumentNullException(nameof(formaDePagoDto), "La forma de pago no puede ser nula");
            if (string.IsNullOrWhiteSpace(formaDePagoDto.Nombre))
                throw new ArgumentException(nameof(formaDePagoDto.Nombre), "El nombre de la forma de pago es requerido");
            FormaDePago formaDePago = new FormaDePago
            {
                Nombre = formaDePagoDto.Nombre,
                Activo = true//Nuevas formas de pago son activas por defecto
            };
            if (_formasDePagoRepositorio.ExisteFormaDePago(formaDePago)) throw new InvalidCastException($"Ya existe una Forma de Pago {formaDePago.Nombre}");
            _formasDePagoRepositorio.Agregar(formaDePago);
        }
        public void Borrar(int formaDePagoId)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (formaDePagoId <= 0)
                throw new ArgumentException("El ID de la forma de pago debe ser un entero mayor a cero.", nameof(formaDePagoId));
            // 2. NUEVO AJUSTE: Verificar existencia real en el sistema antes que cualquier otra cosa
            // Nota: Asumiendo que tu repositorio tiene un método Existe(id) o similar
            var formaDePagoInDb = _formasDePagoRepositorio.ObtenerPorId(formaDePagoId);
            if (formaDePagoInDb is null)
            {
                throw new KeyNotFoundException($"No se puede borrar. No existe ninguna forma de pago con el ID {formaDePagoId}.");
            }

            // AJUSTE: Se cambia Exception genérica por InvalidOperationException
            if (_formasDePagoRepositorio.TieneRegistrosRelacionados(formaDePagoId))
            {
                throw new InvalidOperationException($"No se puede eliminar la forma de pago (ID: {formaDePagoId}) porque tiene registros relacionados en el sistema.");
            }
            _formasDePagoRepositorio.Borrar(formaDePagoId);
        }

        public void Editar(FormaDePagoEditDto? formaDePagoDto)
        {
            if (formaDePagoDto == null)
                throw new ArgumentNullException(nameof(formaDePagoDto), "La forma de pago no puede ser nula");
            if (string.IsNullOrWhiteSpace(formaDePagoDto.Nombre))
                throw new ArgumentException(nameof(formaDePagoDto.Nombre), "El nombre de la forma de pago es requerido");
            FormaDePago formaDePago = new FormaDePago
            {
                FormaDePagoId = formaDePagoDto.FormaDePagoId,
                Nombre = formaDePagoDto.Nombre,
                Activo = formaDePagoDto.Activo
            };
            if (_formasDePagoRepositorio.ExisteFormaDePago(formaDePago)) throw new InvalidOperationException($"Ya existe una Forma de Pago {formaDePago.Nombre}");
            _formasDePagoRepositorio.Editar(formaDePago);
        }
        public FormaDePagoEditDto ObtenerParaEditar(int id)
        {
            // AJUSTE: Validación defensiva del ID antes de operar
            if (id <= 0)
                throw new ArgumentException("El ID de la forma de pago debe ser un entero mayor a cero.", nameof(id));


            FormaDePago? formaDePago = _formasDePagoRepositorio.ObtenerPorId(id);
            if (formaDePago is null) throw new ArgumentException(nameof(id), $"Id {id} no encontrado");
            FormaDePagoEditDto formaDePagoDto = new FormaDePagoEditDto
            {
                FormaDePagoId = formaDePago.FormaDePagoId,
                Nombre = formaDePago.Nombre,
                Activo = formaDePago.Activo
            };
            return formaDePagoDto;
        }
        public List<FormaDePagoListDto> FiltrarPorActivo(bool activo)
        {
            return _formasDePagoRepositorio.FiltrarPorActivo(activo)
                .Select(f => new FormaDePagoListDto
                {
                    FormaDePagoId = f.FormaDePagoId,
                    Nombre = f.Nombre,
                    Activo = f.Activo
                }).ToList();
        }

    }
}
