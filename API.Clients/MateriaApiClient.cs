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
            throw new System.Exception("Error al obtener materias");
        }
    }
}
