using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int Cantidad { get; set; }

        
    }
}