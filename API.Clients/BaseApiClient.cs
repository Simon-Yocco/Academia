using System.Net.Http.Headers;
using System.Text.Json;

namespace API.Clients
{
    public abstract class BaseApiClient
    {
        // Variable privada que almacena el token JWT en memoria mientras la app esté abierta.
        private static string _token = "";
        
        // Propiedad pública estática para leer o asignar el Token.
        public static string Token 
        { 
            get => _token; 
            set 
            {
                _token = value;
                // Si el token cambia (al hacer login o logout), dispara el evento para avisarle
                // a la interfaz que el usuario inició o cerró sesión.
                AuthenticationStateChanged?.Invoke(!string.IsNullOrEmpty(_token));
            }
        }

        // Datos del usuario logueado actualmente en la sesión
        public static string Rol { get; set; } = string.Empty;
        public static string UsuarioActual { get; set; } = string.Empty;
        public static int IDPersonaActual { get; set; }

        // Propiedades de ayuda para saber rápido el rol del usuario sin comparar strings a mano
        public static bool EsAdmin => Rol == "Administrador";
        public static bool EsDocente => Rol == "Docente";
        public static bool EsAlumno => Rol == "Alumno";

        // Evento que notifica a la interfaz gráfica cuando el estado de autenticación cambia
        public static event Action<bool>? AuthenticationStateChanged;

        // Dirección base donde está corriendo la WebAPI
        protected static string BaseUrl => "https://localhost:7001/";

        // Método fábrica: crea y configura un HttpClient listo para usar.
        protected static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            // Le indica a qué servidor pegarle
            client.BaseAddress = new Uri(BaseUrl);
            
            // Configura que las respuestas esperadas sean en formato JSON
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            // Si hay un token guardado (usuario logueado), lo adjunta en los encabezados HTTP
            // bajo el esquema estándar: "Authorization: Bearer <token>"
            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            return client; // Devuelve el cliente configurado y autenticado
        }
    }
}
