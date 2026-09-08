using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace API.Clients
{
    public class EspecialidadApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();

            var response = await client.GetAsync("especialidades");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<EspecialidadDTO>>()
                       ?? new List<EspecialidadDTO>();
            }
            throw new Exception("Error al obtener las especialidades");
        }
        public static async Task<EspecialidadDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"especialidad/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EspecialidadDTO>();
            }
            return null;
        }
        public static async Task<EspecialidadDTO?> AddAsync(EspecialidadDTO especialiadad)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("especialidades", especialiadad );

            if (response.IsSuccessStatusCode)
            {
                // El Add sí devuelve el objeto creado con su nuevo ID
                return await response.Content.ReadFromJsonAsync<EspecialidadDTO>();
            }
            throw new Exception("Error al crear la especialidad");
        }
        public static async Task<bool> UpdateAsync(EspecialidadDTO curso)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("especialidades", curso);

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
            var response = await client.DeleteAsync($"especialidades/{id}");
            // Si es exitoso (204 No Content) devolvemos true
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
