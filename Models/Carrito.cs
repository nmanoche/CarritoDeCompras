using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CarritoDeCompras.Areas.Identity.Data;

namespace CarritoDeCompras.Models
{
    public class Carrito
    {
        [Key]
        public int? IdCarrito { get; set; }
        public int? IdProducto { get; set; }
        //public Producto? Producto { get; set; }
        public string? IdUsuario { get; set; }

        //public IdentityUsuario? Users { get; set; }
        //public Usuario? Usuario { get; set; }
        public int? Cantidad { get; set; }

        public Carrito()
        {

        }

        public Carrito(int idProducto, string idUsuario, int cantidad)
        {
            //IdCarrito = idCarrito;
            IdProducto = idProducto;
            IdUsuario = idUsuario;
            Cantidad = cantidad;
        }
    }
}