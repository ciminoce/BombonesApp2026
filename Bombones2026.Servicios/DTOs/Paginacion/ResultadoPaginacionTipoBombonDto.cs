using Bombones2026.Servicios.DTOs.TipoBombon;

namespace Bombones2026.Servicios.DTOs.Paginacion
{
    public class ResultadoPaginacionTipoBombonDto
    {
        public List<TipoBombonListDto> Items { get; set; } = new List<TipoBombonListDto>();
        public int TotalRegistros { get; set; }
        public int CantidadPorPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / CantidadPorPagina);
    }
}
