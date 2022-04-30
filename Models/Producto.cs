using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; } = "Dell";
        public string Categoria { get; set; } = "Soy una notebook";
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [Display(Name = "Imagen")]
        public string RutaImagen { get; set; }
        
    }
}