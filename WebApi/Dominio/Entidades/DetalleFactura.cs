using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class DetalleFactura
    {
        public int Id { get; set; }
        public int? FacturaId { get; set; }
        public int? ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }

        public virtual Factura? Factura { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
