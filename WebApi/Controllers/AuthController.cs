using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Aplicacion.Dtos;
using WebApi.Aplicacion.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioResponseDto>> Login([FromBody] UsuarioRequestDto request)
        {
            var usuario = await _authService.Login(request);
            return Ok(usuario);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UsuarioRequestRegisterDto request)
        {
            await _authService.Register(request);
            return Ok();
        }
    }
}
