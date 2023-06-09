using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class Inventario
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Producto Producto { get; set; } = null!;
    }
}
