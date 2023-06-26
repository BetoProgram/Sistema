using WebApi.Aplicacion.Dtos;

namespace WebApi.Aplicacion.Interfaces
{
    public interface IProductoRepositorio : ISimpleCrud<ProductoRequestDto, ProductoRespDto, int>
    {
    }
}
