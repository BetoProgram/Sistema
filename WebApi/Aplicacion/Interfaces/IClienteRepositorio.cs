using WebApi.Aplicacion.Dtos;

namespace WebApi.Aplicacion.Interfaces
{
    public interface IClienteRepositorio: ISimpleCrud<ClienteRequestDto, ClienteResponseDto, int>
    {
    }
}
