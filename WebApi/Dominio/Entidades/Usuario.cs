using System;
using System.Collections.Generic;

namespace WebApi.Dominio.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {
            RegistrosActividads = new HashSet<RegistrosActividad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int NivelAcceso { get; set; }

        public virtual ICollection<RegistrosActividad> RegistrosActividads { get; set; }
    }
}
