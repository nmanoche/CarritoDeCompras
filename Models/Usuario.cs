﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        
    }
}