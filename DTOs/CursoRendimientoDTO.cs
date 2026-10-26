namespace DTOs
{
    public class CursoRendimientoDTO
    {
        public int IDCurso { get; set; }
        public string Materia { get; set; } = string.Empty;
        public string Comision { get; set; } = string.Empty;
        public int AnioCalendario { get; set; }
        public string Docente { get; set; } = string.Empty;
        public int TotalInscriptos { get; set; }

        public int CantidadAprobados { get; set; }
        public int CantidadRegulares { get; set; }
        public int CantidadLibres { get; set; }
        public int CantidadCursando { get; set; }

        public double PorcentajeAprobados => TotalInscriptos > 0 ? Math.Round((double)CantidadAprobados / TotalInscriptos * 100, 1) : 0;
        public double PorcentajeRegulares => TotalInscriptos > 0 ? Math.Round((double)CantidadRegulares / TotalInscriptos * 100, 1) : 0;
        public double PorcentajeLibres => TotalInscriptos > 0 ? Math.Round((double)CantidadLibres / TotalInscriptos * 100, 1) : 0;
        public double PorcentajeCursando => TotalInscriptos > 0 ? Math.Round((double)CantidadCursando / TotalInscriptos * 100, 1) : 0;

        public double? NotaPromedio { get; set; }
    }
}
