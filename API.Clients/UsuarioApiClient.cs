using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class UsuarioApiClient : BaseApiClient
    {
        public static async Task<UsuarioDTO?> LoginAsync(string usuario, string clave)
        {
            using var client = CreateHttpClient();

            // Mandamos el usuario y clave en el body como un objeto anónimo
            var loginData = new { Usuario = usuario, Clave = clave };
            var response = await client.PostAsJsonAsync("usuarios/login", loginData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
            }

            // Si devuelve 401 Unauthorized (o cualquier otro error), devolvemos null
            return null;
        }
    }
}
