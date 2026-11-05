using BombonesApp2026.Entidades.Enum;

namespace BombonesApp2026.Entidades.Entidades
{
    public class Venta
    {
        private readonly List<DetalleVenta> _detalles = new();

        public int VentaId { get; private set; }

        public int ClienteId { get; private set; }
        public Cliente Cliente { get; private set; } = null!;

        public DateTime FechaVenta { get; private set; }

        public int FormaDePagoId { get; private set; }
        public FormaDePago FormaDePago { get; private set; } = null!;

        public EstadoVenta EstadoVenta { get; private set; }

        public decimal Total { get; private set; }

        public IReadOnlyCollection<DetalleVenta> Detalles =>
            _detalles.AsReadOnly();

        private Venta() { } // EF Core

        public Venta(int clienteId, int formaDePagoId)
        {
            if (clienteId < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(clienteId));

            if (formaDePagoId <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(formaDePagoId));

            ClienteId = clienteId;
            FormaDePagoId = formaDePagoId;
            FechaVenta = DateTime.Now;
            EstadoVenta = EstadoVenta.Pendiente;
        }
        
        public void AgregarDetalle(int productoId, int cantidad, decimal precio)
        {
            if (_detalles.Any(d => d.ProductoId == productoId))
                throw new InvalidOperationException(
                    $"El producto con ID {productoId} ya está incluido en la venta.");
            var detalle = new DetalleVenta(
                productoId, cantidad, precio);

            _detalles.Add(detalle);

            Total = _detalles.Sum(d => d.Subtotal);
        }
        public bool TieneDetalles()
        {
            return _detalles is not null && _detalles.Count > 0;
        }
        public void Facturar()
        {
            if (EstadoVenta == EstadoVenta.Pendiente)
            {
                EstadoVenta = EstadoVenta.Facturada;
            }
        }
        public void Anular()
        {
            if (EstadoVenta == EstadoVenta.Pendiente)
            {
                EstadoVenta = EstadoVenta.Anulada;
            }
        }
    }
}
