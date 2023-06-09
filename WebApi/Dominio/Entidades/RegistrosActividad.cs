using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class RegistrosActividad
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? Accion { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
