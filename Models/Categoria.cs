﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Categoria
    {
        
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
    }
}