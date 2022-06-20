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
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Display(Name ="Marca")]
        public int? IdMarca { get; set; }
        public Marca? Marca { get; set; }
        [Display (Name ="Categoria")]
        public int? IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }

        [Display(Name = "Imagen")]
        public string? RutaImagen { get; set; }
        
    }
}