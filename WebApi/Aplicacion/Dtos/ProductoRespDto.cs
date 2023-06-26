namespace WebApi.Aplicacion.Dtos
{
    public class ProductoRespDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }

    }
}
