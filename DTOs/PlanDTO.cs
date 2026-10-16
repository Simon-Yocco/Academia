using System;
using System.Collections.Generic;

namespace DTOs
{
    public class PlanDTO
    {
        public int ID { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int IDEspecialidad { get; set; }
        public string EspecialidadDescripcion { get; set; } = string.Empty;
        
        // Esta lista de materias se enviará junto con el plan al momento de guardar.
        public List<MateriaDTO> Materias { get; set; } = new List<MateriaDTO>();
    }
}
