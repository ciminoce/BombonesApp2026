namespace BombonesApp2026.Entidades
{
    public class TipoBombon
    {
        public int TipoBombonId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
