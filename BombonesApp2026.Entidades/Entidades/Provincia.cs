namespace BombonesApp2026.Entidades.Entidades
{
    public class Provincia
    {
        public int ProvinciaId { get; set; }

        public string NombreProvincia { get; set; } = null!;
        public ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();

    }
}
