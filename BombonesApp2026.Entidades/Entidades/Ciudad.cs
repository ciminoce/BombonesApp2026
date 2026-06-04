namespace BombonesApp2026.Entidades.Entidades
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        public string Nombre { get; set; } = null!;
        public int ProvinciaId { get; set; }
        public Provincia? Provincia { get; set; }
    }
}
