namespace BombonesApp2026.Entidades.Entidades
{
    public class Bombon : Producto
    {
        private int _pesoEnGramos;
        public bool TieneAzucar { get; set; }
        public int PesoEnGramos
        {
            get => _pesoEnGramos;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("El peso en gramos debe ser un valor positivo.");
                _pesoEnGramos = value;
            }
        }

        public Bombon() : base()
        {

        }
        public Bombon(string nombre, decimal precio, int stock,
            int pesoEnGramos,
            bool tieneAzucar,
            bool activo = true, string? descripcion = null) :
            base(nombre, precio, stock, activo, descripcion)
        {
            TieneAzucar = tieneAzucar;
            PesoEnGramos = pesoEnGramos;
        }
        public override string MostrarDatos()
        {
            return $"Bombón: Nombre: {Nombre} - Precio:{Precio:C2} - Tiene Azúcar: {TieneAzucar}";
        }

    }
}
