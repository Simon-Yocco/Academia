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
            throw new System.Exception("Error al obtener comisiones");
        }
    }
}
