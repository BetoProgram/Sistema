namespace WebApi.Aplicacion.Dtos
{
    public class ClienteRequestDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
