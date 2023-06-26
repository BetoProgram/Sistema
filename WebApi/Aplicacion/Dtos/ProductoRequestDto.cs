namespace WebApi.Aplicacion.Dtos
{
    public class ProductoRequestDto
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }
        public int Cantidad { get; set; }
    }
}
