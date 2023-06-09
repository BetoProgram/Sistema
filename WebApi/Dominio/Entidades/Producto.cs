using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }

        public virtual Inventario? Inventario { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}
