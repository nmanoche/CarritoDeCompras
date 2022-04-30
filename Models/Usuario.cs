using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Usuario
    {
        [Display(Name = "Clave")]
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        
    }
}