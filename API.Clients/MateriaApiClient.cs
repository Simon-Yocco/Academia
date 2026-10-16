using DTOs;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class MateriaApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<MateriaDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();
            var res = await client.GetAsync("materias");
            if (res.IsSuccessStatusCode) return await res.Content.ReadFromJsonAsync<IEnumerable<MateriaDTO>>() ?? new List<MateriaDTO>();
            var error = await res.Content.ReadAsStringAsync();
            throw new System.Exception($"Error al obtener materias: {res.StatusCode} - {error}");
        }

        public static async Task<MateriaDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"materias/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MateriaDTO>();
            }
            return null;
        }

        public static async Task<IEnumerable<MateriaDTO>> GetByCriteriaAsync(string criteria)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"materias/buscar/{System.Uri.EscapeDataString(criteria)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<MateriaDTO>>()
                       ?? new List<MateriaDTO>();
            }
            throw new System.Exception("Error al obtener las materias filtradas");
        }

        public static async Task<MateriaDTO?> AddAsync(MateriaDTO materia)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("materias", materia);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MateriaDTO>();
            }
            throw new System.Exception("Error al crear la materia");
        }

        public static async Task<bool> UpdateAsync(MateriaDTO materia)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("materias", materia);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.DeleteAsync($"materias/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
