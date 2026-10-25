using DTOs;
using System.Net.Http.Json;
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
    }
}
