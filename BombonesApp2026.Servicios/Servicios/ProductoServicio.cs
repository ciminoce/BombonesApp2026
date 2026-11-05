using BombonesApp2026.Datos;
using BombonesApp2026.Datos.Interfaces;
using BombonesApp2026.Entidades.Entidades;
using BombonesApp2026.Servicios.DTOs.Producto;
using BombonesApp2026.Servicios.Interfaces;

namespace BombonesApp2026.Servicios.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IBombonRepositorio _bombonRepositorio;
        private readonly ICajaRepositorio _cajaRepositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ProductoServicio(IProductoRepositorio productoRepositorio,
            IBombonRepositorio bombonRepositorio,
            ICajaRepositorio cajaRepositorio,
            IUnitOfWork unitOfWork)
        {
            _productoRepositorio = productoRepositorio;
            _bombonRepositorio = bombonRepositorio;
            _cajaRepositorio = cajaRepositorio;
            _unitOfWork = unitOfWork;
        }

        public List<ProductoListDto> ObtenerTodos()
        {
            // Asegurate de que el repositorio traiga los productos e incluya 
            // los detalles y los bombones de las cajas si es necesario (Eager Loading)
            var productos = _productoRepositorio.ObtenerTodos();

            return productos.Select(p =>
            {
                decimal precioFinal = 0m;
                string tipoProducto = "Producto";

                // Evaluamos el tipo real gracias al polimorfismo de la herencia
                if (p is Bombon bombon)
                {
                    tipoProducto = "Bombón";
                    precioFinal = bombon.Precio;
                }
                else if (p is Caja caja)
                {
                    tipoProducto = "Caja";

                    precioFinal = caja.Precio;
                }

                return new ProductoListDto
                {
                    ProductoId = p.ProductoId,
                    TipoProducto = tipoProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = precioFinal,
                    Stock = p.Stock
                };
            })
            .ToList();
        }
    }
}
