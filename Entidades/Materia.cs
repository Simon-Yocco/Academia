using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Materia : BusinessEntity
    {
        public string Descripcion { get; private set; } = string.Empty;
        public int HSSemanales { get; private set; }
        public int HSTotales { get; private set; }
        public int IDPlan { get; private set; }
        public Materia() { }
        public Materia(int id, string descripcion, int hsSemanales, int hsTotales, int idPlan)
        {
            SetId(id);
            SetDescripcion(descripcion);
            SetHSSemanales(hsSemanales);
            SetHSTotales(hsTotales);
            SetIDPlan(idPlan);
        }
        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) throw new ArgumentException("La descripción no puede estar vacía.");
            Descripcion = descripcion.Trim();
        }
        public void SetHSSemanales(int hsSemanales)
        {
            if (hsSemanales <= 0) throw new ArgumentException("Las horas semanales deben ser mayores a cero.");
            HSSemanales = hsSemanales;
        }
        public void SetHSTotales(int hsTotales)
        {
            if (hsTotales <= 0) throw new ArgumentException("Las horas totales deben ser mayores a cero.");
            HSTotales = hsTotales;
        }
        public void SetIDPlan(int idPlan)
        {
            if (idPlan <= 0) throw new ArgumentException("El ID del plan debe ser válido.");
            IDPlan = idPlan;
        }
    }
}
