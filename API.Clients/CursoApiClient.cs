using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    public class CursoApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<CursoDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();
            
            var response = await client.GetAsync("cursos");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<CursoDTO>>()
                       ?? new List<CursoDTO>();
            }
            throw new Exception("Error al obtener los cursos");
        }
        public static async Task<CursoDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"cursos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CursoDTO>();
            }
            return null;
        }
        public static async Task<CursoDTO?> AddAsync(CursoDTO curso)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("cursos", curso);

            if (response.IsSuccessStatusCode)
            {
                // El Add sí devuelve el objeto creado con su nuevo ID
                return await response.Content.ReadFromJsonAsync<CursoDTO>();
            }
            throw new Exception("Error al crear el curso");
        }
        public static async Task<bool> UpdateAsync(CursoDTO curso)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("cursos", curso);

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
            var response = await client.DeleteAsync($"cursos/{id}");
            // Si es exitoso (204 No Content) devolvemos true
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
