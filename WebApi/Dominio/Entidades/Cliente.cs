using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
