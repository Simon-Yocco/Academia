using System.Net.Http.Headers;
using System.Text.Json;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        private static string _token = "";
        
        public static string Token 
        { 
            get => _token; 
            set 
            {
                _token = value;
                AuthenticationStateChanged?.Invoke(!string.IsNullOrEmpty(_token));
            }
        }
        public static string Rol { get; set; } = string.Empty;
        public static string UsuarioActual { get; set; } = string.Empty;
        public static bool EsAdmin => Rol == "Administrador";

        public static event Action<bool>? AuthenticationStateChanged;

        protected static string BaseUrl => "https://localhost:7001/"; 

        protected static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            return client;
        }
    }
}
