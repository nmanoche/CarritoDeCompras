﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}