namespace CarritoDeCompras.Models
{
    public class ProductoMostrable
    {
        public Producto? producto { get; set; }
        public int? cantidadProducto { get; set; }

        public ProductoMostrable()
        {

        }

        public ProductoMostrable(Producto? producto, int cantidadProducto)
        {
            this.producto = producto;
            this.cantidadProducto = cantidadProducto;
        }
    }
}
