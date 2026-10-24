using System;

namespace Entidades
{
    public class AlumnoInscripcion : BusinessEntity
    {
        public int IDAlumno { get; private set; }
        public int IDCurso { get; private set; }
        public string Condicion { get; private set; } = "Inscripto";
        public int? Nota { get; private set; }

        public Persona? Alumno { get; private set; }
        public Curso? Curso { get; private set; }

        public AlumnoInscripcion() { }

        public AlumnoInscripcion(int id, int idAlumno, int idCurso, string condicion = "Inscripto", int? nota = null)
        {
            SetId(id);
            SetIDAlumno(idAlumno);
            SetIDCurso(idCurso);
            SetCondicion(condicion);
            SetNota(nota);
        }

        public void SetIDAlumno(int idAlumno)
        {
            if (idAlumno <= 0) throw new ArgumentException("El ID del alumno no es válido.");
            IDAlumno = idAlumno;
        }

        public void SetIDCurso(int idCurso)
        {
            if (idCurso <= 0) throw new ArgumentException("El ID del curso no es válido.");
            IDCurso = idCurso;
        }

        public void SetCondicion(string condicion)
        {
            if (string.IsNullOrWhiteSpace(condicion)) throw new ArgumentException("La condición no puede estar vacía.");
            Condicion = condicion.Trim();
        }

        public void SetNota(int? nota)
        {
            if (nota.HasValue && (nota.Value < 1 || nota.Value > 10))
                throw new ArgumentException("La nota debe estar comprendida entre 1 y 10.");
            Nota = nota;
        }

        public void SetAlumno(Persona alumno)
        {
            ArgumentNullException.ThrowIfNull(alumno);
            Alumno = alumno;
            IDAlumno = alumno.ID;
        }

        public void SetCurso(Curso curso)
        {
            ArgumentNullException.ThrowIfNull(curso);
            Curso = curso;
            IDCurso = curso.ID;
        }
    }
}
