using System.Net.Http.Headers;

namespace API.Clients
{
    public class BaseApiClient
    {
        // Esta es la URL base de la WebAPI
        protected static readonly string BaseUrl = "http://localhost:5071/";
        protected static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
