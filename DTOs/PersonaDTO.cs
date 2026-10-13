using System;

namespace DTOs
{
    public class PersonaDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;
        public int Legajo { get; set; }
        public string TipoPersona { get; set; } = "Alumno"; // Administrador, Docente, Alumno
        public int? IDPlan { get; set; }
        public string? PlanDescripcion { get; set; }
    }
}
