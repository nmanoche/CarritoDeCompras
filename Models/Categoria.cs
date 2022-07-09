using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }

        [Display(Name ="Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}