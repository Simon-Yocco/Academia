using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Plan : BusinessEntity
    {
        public string Descripcion { get; private set; } = string.Empty;
        public int IDEspecialidad { get; private set; }
        public Especialidad? Especialidad { get; private set; }
        
        public List<Materia> Materias { get; private set; } = new List<Materia>();

        public Plan() { }

        public Plan(int id, string descripcion, int idEspecialidad)
        {
            SetId(id);
            SetDescripcion(descripcion);
            SetIDEspecialidad(idEspecialidad);
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) 
                throw new ArgumentException("La descripción no puede estar vacía.");
            Descripcion = descripcion.Trim();
        }

        public void SetIDEspecialidad(int idEspecialidad)
        {
            if (idEspecialidad <= 0) 
                throw new ArgumentException("El ID de la especialidad debe ser válido.");
            IDEspecialidad = idEspecialidad;
        }

        public void SetEspecialidad(Especialidad especialidad)
        {
            ArgumentNullException.ThrowIfNull(especialidad);
            Especialidad = especialidad;
            IDEspecialidad = especialidad.ID;
        }

        public void AgregarMateria(Materia materia)
        {
            ArgumentNullException.ThrowIfNull(materia);
            Materias.Add(materia);
        }

        public void QuitarMateria(Materia materia)
        {
            ArgumentNullException.ThrowIfNull(materia);
            Materias.Remove(materia);
        }

        public void LimpiarMaterias()
        {
            Materias.Clear();
        }
    }
}
