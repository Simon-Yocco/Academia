using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            // Creamos un pequeño DTO para recibir las credenciales
            app.MapPost("/usuarios/login", async (LoginRequest request, UsuarioService usuarioService) =>
            {
                var usuario = await usuarioService.LoginAsync(request.Usuario, request.Clave);
                if (usuario != null)
                {
                    return Results.Ok(usuario);
                }
                return Results.Unauthorized();
            })
            .WithName("LoginUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        }
    }

    // Clase temporal para recibir usuario y clave en el body
    public class LoginRequest
    {
        public string Usuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }
}
