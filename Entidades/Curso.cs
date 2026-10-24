using System;

namespace Entidades
{
    public class Curso : BusinessEntity
    {
        public int AnioCalendario { get; private set; }
        public int Cupo { get; private set; }
        public string Descripcion { get; private set; } = string.Empty;
        public int IDcomision { get; private set; }
        public int IDmateria { get; private set; }

        public int IDdocente { get; private set; }
        public Materia? Materia { get; private set; }
        public Comision? Comision { get; private set; }

        public Persona? Docente { get; private set; }

        public Curso() { }

        public Curso(int id, int anioCalendario, int cupo, string descripcion, int idComision, int idMateria , int idDocente)
        {
            SetId(id);
            SetAnioCalendario(anioCalendario);
            SetCupo(cupo);
            SetDescripcion(descripcion);
            SetIDcomision(idComision);
            SetIDmateria(idMateria);
            SetIDdocente(idDocente);
        }

        public void SetAnioCalendario(int anio)
        {
            if (anio < 2000) throw new ArgumentException("El año no es válido.");
            AnioCalendario = anio;
        }

        public void SetCupo(int cupo)
        {
            if (cupo <= 0) throw new ArgumentException("El cupo debe ser mayor a cero.");
            Cupo = cupo;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) throw new ArgumentException("La descripción no puede estar vacía.");
            Descripcion = descripcion.Trim();
        }

        public void SetIDcomision(int idComision)
        {
            if (idComision <= 0) throw new ArgumentException("El ID de la comisión debe ser válido.");
            IDcomision = idComision;
        }

        public void SetIDmateria(int idMateria)
        {
            if (idMateria <= 0) throw new ArgumentException("El ID de la materia debe ser válido.");
            IDmateria = idMateria;
        }

        public void SetMateria(Materia materia)
        {
            ArgumentNullException.ThrowIfNull(materia);
            Materia = materia;
            IDmateria = materia.ID;
        }

        public void SetComision(Comision comision)
        {
            ArgumentNullException.ThrowIfNull(comision);
            Comision = comision;
            IDcomision = comision.ID;
        }
        public void SetIDdocente(int idDocente)
        {
            if (idDocente <= 0)
                throw new ArgumentException("El ID del docente debe ser válido.");
            IDdocente = idDocente;
        }
        public void SetDocente(Persona docente)
        {
            ArgumentNullException.ThrowIfNull(docente);
            Docente = docente;
            IDdocente = docente.ID;
        }
    }
}