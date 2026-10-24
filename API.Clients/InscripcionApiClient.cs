using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class InscripcionApiClient : BaseApiClient
    {
        public static async Task<IEnumerable<AlumnoInscripcionDTO>> GetAllAsync()
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync("inscripciones");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<AlumnoInscripcionDTO>>()
                       ?? new List<AlumnoInscripcionDTO>();
            }
            throw new Exception("Error al obtener las inscripciones");
        }

        public static async Task<AlumnoInscripcionDTO?> GetByIdAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"inscripciones/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AlumnoInscripcionDTO>();
            }
            return null;
        }

        public static async Task<IEnumerable<AlumnoInscripcionDTO>> GetByCursoAsync(int idCurso)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"inscripciones/curso/{idCurso}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<AlumnoInscripcionDTO>>()
                       ?? new List<AlumnoInscripcionDTO>();
            }
            throw new Exception("Error al obtener los alumnos del curso");
        }

        public static async Task<IEnumerable<AlumnoInscripcionDTO>> GetByAlumnoAsync(int idAlumno)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"inscripciones/alumno/{idAlumno}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<AlumnoInscripcionDTO>>()
                       ?? new List<AlumnoInscripcionDTO>();
            }
            throw new Exception("Error al obtener las inscripciones del alumno");
        }

        public static async Task<IEnumerable<AlumnoInscripcionDTO>> GetByCriteriaAsync(string criteria)
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync($"inscripciones/buscar/{Uri.EscapeDataString(criteria)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<AlumnoInscripcionDTO>>()
                       ?? new List<AlumnoInscripcionDTO>();
            }
            throw new Exception("Error al buscar inscripciones");
        }

        public static async Task<AlumnoInscripcionDTO?> AddAsync(AlumnoInscripcionDTO dto)
        {
            using var client = CreateHttpClient();
            var response = await client.PostAsJsonAsync("inscripciones", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AlumnoInscripcionDTO>();
            }
            throw new Exception((await response.Content.ReadAsStringAsync()).Trim('"'));
        }

        public static async Task<bool> UpdateAsync(AlumnoInscripcionDTO dto)
        {
            using var client = CreateHttpClient();
            var response = await client.PutAsJsonAsync("inscripciones", dto);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception((await response.Content.ReadAsStringAsync()).Trim('"'));
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            using var client = CreateHttpClient();
            var response = await client.DeleteAsync($"inscripciones/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
