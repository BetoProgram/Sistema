namespace WebApi.Aplicacion.Dtos
{
    public class UsuarioRequestRegisterDto
    {
        public string Nombre { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
