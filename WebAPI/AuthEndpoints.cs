using DTOs;
using Application.Services;

namespace WebAPI
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/auth");

            // Definición de la ruta POST para la autenticación
            group.MapPost("/login", async (LoginRequest request, AuthService authService) =>
            {
                // Procesamiento de las credenciales vía servicio de aplicación
                var token = await authService.LoginAsync(request);
                
                if (string.IsNullOrEmpty(token))
                {
                    return Results.Unauthorized();
                }

                // Respuesta exitosa incluyendo el token generado
                var response = new LoginResponse { Token = token };
                return Results.Ok(response);
            })
            // Endpoint público sin autenticación requerida
            .AllowAnonymous(); 
        }
    }
}
