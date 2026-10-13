using System;

namespace DTOs
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public bool Habilitado { get; set; } = true;
        public int IDPersona { get; set; }

        // Datos personales asociados
        public PersonaDTO Persona { get; set; } = new();

        // Atajos de conveniencia para grillas y bindings
        public string Nombre => Persona?.Nombre ?? string.Empty;
        public string Apellido => Persona?.Apellido ?? string.Empty;
        public string Rol => Persona?.TipoPersona ?? string.Empty;
        public int Legajo => Persona?.Legajo ?? 0;
        public string Email => Persona?.Email ?? string.Empty;
    }
}
