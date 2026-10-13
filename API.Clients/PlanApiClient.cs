using DTOs;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;

namespace API.Clients
{
    public class PlanApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<PlanDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();
            
            var response = await client.GetAsync("planes");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<PlanDTO>>()
                       ?? new List<PlanDTO>();
            }
            throw new Exception("Error al obtener los planes");
        }

        public static async Task<PlanDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"planes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PlanDTO>();
            }
            return null;
        }

        public static async Task<IEnumerable<PlanDTO>> GetByCriteriaAsync(string criteria)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"planes/buscar/{System.Uri.EscapeDataString(criteria)}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<PlanDTO>>()
                       ?? new List<PlanDTO>();
            }
            throw new Exception("Error al obtener los planes filtrados");
        }

        public static async Task<PlanDTO?> AddAsync(PlanDTO plan)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("planes", plan);

            if (response.IsSuccessStatusCode)
            {
                // El Add devuelve el objeto creado con su nuevo ID
                return await response.Content.ReadFromJsonAsync<PlanDTO>();
            }
            throw new Exception("Error al crear el plan");
        }

        public static async Task<bool> UpdateAsync(PlanDTO plan)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("planes", plan);

            // Si es exitoso (204 No Content) devolvemos true
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.DeleteAsync($"planes/{id}");
            
            // Si es exitoso (204 No Content) devolvemos true
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
