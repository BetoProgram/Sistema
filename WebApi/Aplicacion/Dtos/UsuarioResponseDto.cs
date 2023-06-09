namespace WebApi.Aplicacion.Dtos
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Token { get; set; }
    }
}
