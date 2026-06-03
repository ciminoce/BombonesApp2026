namespace BombonesApp2026.Entidades.Entidades
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

    }
}
