using System;

namespace Entidades
{
    public class Especialidad : BusinessEntity
    {
        public string Descripcion { get; private set; } = string.Empty;

        public Especialidad() { }

        public Especialidad(int id, string descripcion)
        {
            SetId(id);
            SetDescripcion(descripcion);
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción de la especialidad no puede estar vacía.");
            Descripcion = descripcion.Trim();
        }
    }
}