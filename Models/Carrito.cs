using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public Producto Producto { get; set; }
        public Usuario Usuario { get; set; }
    }
}