using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoDeCompras.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }
        public string IdUsuario { get; set; }
        public string FechaCompra { get; set; }
        public decimal TotalPagar { get; set; }
        public string? NombreApellidoFacturar { get; set; }
        public string? EmailFacturar { get; set; }
        public string? DireccionFacturar { get; set; }
        public string? CodigoPostalFacturar { get; set; }
        //public List<DetalleCompra>? DetalleCompras { get; set; }

        public Compra()
        {

        }

        public Compra(string IdUsuario, string FechaCompra, decimal TotalPagar, string? NombreApellidoFacturar, string? EmailFacturar, string? DireccionFacturar, string? CodigoPostalFacturar)
        {
            this.IdUsuario = IdUsuario;
            this.FechaCompra = FechaCompra;
            this.TotalPagar = TotalPagar;
            this.NombreApellidoFacturar = NombreApellidoFacturar;
            this.EmailFacturar = EmailFacturar;
            this.DireccionFacturar = DireccionFacturar;
            this.CodigoPostalFacturar = CodigoPostalFacturar;
        }

    }
}