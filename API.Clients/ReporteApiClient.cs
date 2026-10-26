using DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class ReporteApiClient : BaseApiClient
    {
        public static async Task<List<CursoOcupacionDTO>> GetCursosOcupacionAsync()
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync("reportes/cursos-ocupacion");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CursoOcupacionDTO>>()
                       ?? new List<CursoOcupacionDTO>();
            }
            throw new Exception("Error al obtener el reporte de cursos y ocupación.");
        }

        public static async Task<List<CursoRendimientoDTO>> GetCursosRendimientoAsync()
        {
            using var client = CreateHttpClient();
            var response = await client.GetAsync("reportes/cursos-rendimiento");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CursoRendimientoDTO>>()
                       ?? new List<CursoRendimientoDTO>();
            }
            throw new Exception("Error al obtener el reporte de rendimiento académico de cursos.");
        }
    }
}
