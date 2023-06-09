using WebApi.Aplicacion.Dtos;

namespace WebApi.Aplicacion.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioResponseDto> Login(UsuarioRequestDto request);

        Task Register(UsuarioRequestRegisterDto request);

    }
}
