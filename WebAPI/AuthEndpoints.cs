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
                var response = await authService.LoginAsync(request);
                
                if (response == null)
                {
                    return Results.Unauthorized();
                }

                // Respuesta exitosa incluyendo el token generado, usuario y rol
                return Results.Ok(response);
            })
            // Endpoint público sin autenticación requerida
            .AllowAnonymous(); 
        }
    }
}
