namespace BombonesApp2026.Entidades.Entidades
{
    public class Transporte
    {
        public int TransporteId { get; set; }
        public string NombreEmpresa { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int ProvinciaId { get; set; }
        public Provincia? Provincia { get; set; }
        public bool Activo { get; set; }

    }
}
