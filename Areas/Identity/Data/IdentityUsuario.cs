using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarritoDeCompras.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Usuario class
public class IdentityUsuario : IdentityUser
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }

}

