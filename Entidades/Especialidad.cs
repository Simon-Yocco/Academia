using System;

namespace Entidades
{
    public class Especialidad : BusinessEntity
    {
        public string Descripcion { get; private set; } = string.Empty;

        // Constructor vacío
        public Especialidad() { }

        public Especialidad(int id, string descripcion)
        {
            ID = id;
            SetDescripcion(descripcion);
        }

        // Método de validación
        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción de la especialidad no puede estar vacía.");
            Descripcion = descripcion.Trim();
        }
    }
}