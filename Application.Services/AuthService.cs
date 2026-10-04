using DTOs;
using Data;
using Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        // Inyección de dependencias del repositorio de usuarios
        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginRequest loginRequest)
        {
            // Validación de formato de credenciales
            if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return null;
            }

            // Obtención de la entidad Usuario desde la base de datos
            var usuario = await _usuarioRepository.GetByUsernameAsync(loginRequest.Username);

            // Verificación de existencia del usuario y validación de contraseña
            if (usuario == null || usuario.Clave != loginRequest.Password || !usuario.Habilitado)
            {
                return null;
            }

            // Generación y firma del token JWT
            return GenerateJwtToken(usuario);
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            // Lectura de configuraciones JWT desde appsettings.json o variables de entorno
            var jwtSettings = _configuration.GetSection("Jwt");
            var keyString = jwtSettings["Key"] ?? "TPI-JWT-Secret-Key-256-bits-long-for-development-only-never-use-in-production";
            var issuer = jwtSettings["Issuer"] ?? "TPI-API";
            var audience = jwtSettings["Audience"] ?? "TPI-Clients";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Definición de reclamos (claims) integrados en el payload del JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.ID.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Construcción del token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            // Serialización del token a formato string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

