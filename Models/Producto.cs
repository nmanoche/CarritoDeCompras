﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        
    }
}