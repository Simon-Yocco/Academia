using DTOs;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class UsuarioApiClient : BaseApiClient
    {
        public static async Task<int> GetProximoLegajoAsync()
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync("usuarios/proximo-legajo");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
            return 1001;
        }

        public static async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();
            var res = await client.GetAsync("usuarios");
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<IEnumerable<UsuarioDTO>>() ?? new List<UsuarioDTO>();
            }
            throw new System.Exception("Error al obtener los usuarios");
        }

        public static async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"usuarios/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
            }
            return null;
        }

        public static async Task<UsuarioDTO?> AddAsync(UsuarioDTO usuario)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("usuarios", usuario);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
            }
            throw new System.Exception("Error al crear el usuario");
        }

        public static async Task<bool> UpdateAsync(UsuarioDTO usuario)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("usuarios", usuario);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.DeleteAsync($"usuarios/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
