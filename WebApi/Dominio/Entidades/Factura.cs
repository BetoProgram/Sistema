using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
            Pagos = new HashSet<Pago>();
        }

        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public string? Numero { get; set; }
        public DateOnly? Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
