using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebApi.Aplicacion.Commons.Exceptions;
using WebApi.Aplicacion.Dtos;
using WebApi.Aplicacion.Interfaces;
using WebApi.Aplicacion.Models;
using WebApi.Dominio.Entidades;
using WebApi.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Aplicacion.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly FactAppDbContext _context;
        public JwtSettings _jwtSettings { get; }

        public AuthService(FactAppDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<UsuarioResponseDto> Login(UsuarioRequestDto request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (usuario == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, 
                    new {  mensje = $"No se encuentra el usuario ${request.Username} registrado" });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            {
                throw new CustomException(HttpStatusCode.Unauthorized, new
                {
                    mensaje = $"Usuario incorrecto no tienes autorizacion"
                });
            }

            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Username = usuario.Username,
                Token = CreateToken(usuario, null)
            };

        }

        public async Task Register(UsuarioRequestRegisterDto request)
        {
            var usuario = new Usuario
            {
                Username = request.Username,
                Nombre = request.Nombre,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                NivelAcceso = 2
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        private string CreateToken(Usuario usuario, IList<string>? roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,usuario.Username!),
                new Claim("userId", usuario.Id!.ToString()),
                new Claim("email", usuario.Username!),
            };

            //foreach (var rol in roles!)
            //{
            //    var claim = new Claim(ClaimTypes.Role, rol);
            //    claims.Add(claim);
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

    }
}
