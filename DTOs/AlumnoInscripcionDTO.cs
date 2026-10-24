namespace DTOs
{
    public class AlumnoInscripcionDTO
    {
        public int ID { get; set; }
        public int IDAlumno { get; set; }
        public int IDCurso { get; set; }
        public string Condicion { get; set; } = "Inscripto";
        public int? Nota { get; set; }

        // Propiedades auxiliares para mostrar en las grillas de WinForms y Blazor
        public string AlumnoNombreCompleto { get; set; } = string.Empty;
        public int AlumnoLegajo { get; set; }
        public string CursoDescripcion { get; set; } = string.Empty;
        public string MateriaDescripcion { get; set; } = string.Empty;
        public string ComisionDescripcion { get; set; } = string.Empty;
        public int AnioCalendario { get; set; }
        public int Cupo { get; set; }
    }
}
