namespace BombonesApp2026.Entidades.Entidades
{
    public abstract class Producto
    {
        private string? _nombre;
        public int ProductoId { get; private set; }
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre es requerido.");
                }
                _nombre = value;
            }
        }

        public string? Descripcion { get; set; }
        private decimal _precio;

        public decimal Precio
        {
            get { return _precio; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("El precio no puede ser negativo.");
                }
                _precio = value;
            }
        }
        private int _stock;

        public int Stock
        {
            get { return _stock; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("El stock no puede ser negativo.");
                }
                _stock = value;
            }
        }

        public bool Activo { get; set; }
        public Producto()
        {

        }
        public Producto(string nombre, decimal precio, int stock,
            bool activo = true, string? descripcion = null)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Activo = activo;
            Descripcion = descripcion;
        }
        public abstract string MostrarDatos();
    }
}
