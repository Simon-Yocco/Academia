using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Comision : BusinessEntity
    {
        public int AnioEspecialidad { get; private set; }
        public string Descripcion { get; private set; } = string.Empty;
        public int IDPlan { get; private set; }
        public Comision() { }
        public Comision(int id, int anioEspecialidad, string descripcion, int idPlan)
        {
            SetId(id);
            SetAnioEspecialidad(anioEspecialidad);
            SetDescripcion(descripcion);
            SetIDPlan(idPlan);
        }
        public void SetAnioEspecialidad(int anio)
        {
            if (anio < 2000) throw new ArgumentException("El año de especialidad no es válido.");
            AnioEspecialidad = anio;
        }
        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) throw new ArgumentException("La descripción no puede estar vacía.");
            Descripcion = descripcion.Trim();
        }
        public void SetIDPlan(int idPlan)
        {
            if (idPlan <= 0) throw new ArgumentException("El ID del plan debe ser válido.");
            IDPlan = idPlan;
        }
    }
}
