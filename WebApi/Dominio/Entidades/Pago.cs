using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class Pago
    {
        public int Id { get; set; }
        public int? FacturaId { get; set; }
        public DateOnly? Fecha { get; set; }
        public decimal Monto { get; set; }
        public string? MetodoPago { get; set; }

        public virtual Factura? Factura { get; set; }
    }
}
