using Bombones2026.Servicios.DTOs.TipoBombon;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades;

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
           
            if(tipoDto is null)
            {
                throw new ArgumentNullException(nameof(tipoDto),"El tipo de bombón no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(tipoDto.Nombre))
            {
                throw new ArgumentNullException(nameof(tipoDto.Nombre), "El nombre es requerido");
            }
            TipoBombon tipo = new TipoBombon();
            tipo.Nombre=tipoDto.Nombre;
            tipo.Descripcion = tipoDto.Descripcion;
            tipo.Activo = true;
            if (_tipoBombonRepositorio.ExisteTipoBombon(tipo))
            {
                throw new InvalidOperationException($"Ya existe un tipo de bombón {tipo.Nombre}");
            }
            _tipoBombonRepositorio.Agregar(tipo);
        }
    }
}
