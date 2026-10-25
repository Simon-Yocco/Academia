using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CursoOcupacionDTO
    {
        public int IDCurso { get; set; }
        public string Materia { get; set; } = string.Empty;
        public string Comision { get; set; } = string.Empty;
        public int AnioCalendario { get; set; }
        public string Docente { get; set; } = string.Empty;
        public int CupoTotal { get; set; }
        public int Inscriptos { get; set; }
        public double PorcentajeOcupacion => CupoTotal > 0
            ? Math.Round((double)Inscriptos / CupoTotal * 100, 1)
            : 0;
        public string Descripcioncorta => $"{Materia} ({Comision})";
    }
}