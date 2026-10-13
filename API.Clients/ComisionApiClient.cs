using DTOs;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class ComisionApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<ComisionDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();
            var res = await client.GetAsync("comisiones");
            if (res.IsSuccessStatusCode) return await res.Content.ReadFromJsonAsync<IEnumerable<ComisionDTO>>() ?? new List<ComisionDTO>();
            throw new System.Exception("Error al obtener las comisiones");
        }

        public static async Task<ComisionDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"comisiones/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ComisionDTO>();
            }
            return null;
        }

        public static async Task<IEnumerable<ComisionDTO>> GetByCriteriaAsync(string criteria)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"comisiones/buscar/{System.Uri.EscapeDataString(criteria)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ComisionDTO>>()
                       ?? new List<ComisionDTO>();
            }
            throw new System.Exception("Error al obtener las comisiones filtradas");
        }

        public static async Task<ComisionDTO?> AddAsync(ComisionDTO comision)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("comisiones", comision);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ComisionDTO>();
            }
            throw new System.Exception("Error al crear la comisión");
        }

        public static async Task<bool> UpdateAsync(ComisionDTO comision)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("comisiones", comision);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.DeleteAsync($"comisiones/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
