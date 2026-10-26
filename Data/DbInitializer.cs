using Entidades;
using System;
using System.Linq;

namespace Data
{
    public static class DbInitializer
    {
        public static void Initialize(TPIContext context)
        {
            context.Database.EnsureCreated();

            // 1. Especialidades
            if (!context.Especialidades.Any())
            {
                context.Especialidades.AddRange(
                    new Especialidad(0, "Ingeniería en Sistemas de Información"),
                    new Especialidad(0, "Ingeniería Química")
                );
                context.SaveChanges();
            }

            // 2. Planes
            if (!context.Planes.Any())
            {
                var esp = context.Especialidades.First();
                context.Planes.AddRange(
                    new Plan(0, "Plan 2023", esp.ID),
                    new Plan(0, "Plan 2008", esp.ID)
                );
                context.SaveChanges();
            }

            // 3. Materias
            if (!context.Materias.Any())
            {
                var plan = context.Planes.First();
                context.Materias.AddRange(
                    new Materia(0, "Sistemas Operativos", 6, 192, plan.ID),
                    new Materia(0, "Desarrollo de Software .NET", 4, 128, plan.ID),
                    new Materia(0, "Bases de Datos", 4, 128, plan.ID)
                );
                context.SaveChanges();
            }

            // 4. Comisiones
            if (!context.Comisiones.Any())
            {
                var plan = context.Planes.First();
                context.Comisiones.AddRange(
                    new Comision(0, 3, "Comisión 3K1", plan.ID),
                    new Comision(0, 3, "Comisión 3K2", plan.ID)
                );
                context.SaveChanges();
            }

            // 5. Personas y Usuarios (Docente y Alumnos)
            if (!context.Personas.Any(p => p.TipoPersona == Persona.TiposPersonas.Docente))
            {
                var plan = context.Planes.First();
                var docente = new Persona(0, "Carlos", "Gómez", "Pellegrini 1234", "profesor@academia.utn.edu.ar", "3415551122", new DateTime(1980, 5, 15), 2001, Persona.TiposPersonas.Docente, null);
                var alumno1 = new Persona(0, "Juan", "Pérez", "Mitre 450", "alumno@academia.utn.edu.ar", "3415553344", new DateTime(2001, 8, 20), 5001, Persona.TiposPersonas.Alumno, plan.ID);
                var alumno2 = new Persona(0, "María", "López", "Corrientes 890", "maria@academia.utn.edu.ar", "3415555566", new DateTime(2002, 3, 10), 5002, Persona.TiposPersonas.Alumno, plan.ID);
                var alumno3 = new Persona(0, "Lucas", "Fernández", "San Juan 1100", "lucas@academia.utn.edu.ar", "3415557788", new DateTime(2000, 11, 5), 5003, Persona.TiposPersonas.Alumno, plan.ID);

                context.Personas.AddRange(docente, alumno1, alumno2, alumno3);
                context.SaveChanges();

                if (!context.Usuarios.Any(u => u.NombreUsuario == "profesor"))
                    context.Usuarios.Add(new Usuario(0, "profe123", true, "profesor", docente.ID));

                if (!context.Usuarios.Any(u => u.NombreUsuario == "alumno"))
                    context.Usuarios.Add(new Usuario(0, "alumno123", true, "alumno", alumno1.ID));

                if (!context.Usuarios.Any(u => u.NombreUsuario == "maria"))
                    context.Usuarios.Add(new Usuario(0, "maria123", true, "maria", alumno2.ID));

                if (!context.Usuarios.Any(u => u.NombreUsuario == "lucas"))
                    context.Usuarios.Add(new Usuario(0, "lucas123", true, "lucas", alumno3.ID));

                context.SaveChanges();
            }

            // 6. Cursos
            if (!context.Cursos.Any())
            {
                var materiaNet = context.Materias.FirstOrDefault(m => m.Descripcion.Contains(".NET")) ?? context.Materias.First();
                var materiaBD = context.Materias.FirstOrDefault(m => m.Descripcion.Contains("Bases")) ?? context.Materias.Last();
                var comision = context.Comisiones.First();
                var docente = context.Personas.FirstOrDefault(p => p.TipoPersona == Persona.TiposPersonas.Docente);

                if (docente != null)
                {
                    var c1 = new Curso(0, 2026, 30, "Curso .NET - Turno Tarde", comision.ID, materiaNet.ID, docente.ID);
                    var c2 = new Curso(0, 2026, 25, "Curso Bases de Datos - Turno Noche", comision.ID, materiaBD.ID, docente.ID);
                    context.Cursos.AddRange(c1, c2);
                    context.SaveChanges();

                    // 7. Inscripciones
                    var alumnos = context.Personas.Where(p => p.TipoPersona == Persona.TiposPersonas.Alumno).ToList();
                    if (alumnos.Count >= 3)
                    {
                        context.AlumnosInscripciones.AddRange(
                            new AlumnoInscripcion(0, alumnos[0].ID, c1.ID, "Aprobado", 8),
                            new AlumnoInscripcion(0, alumnos[1].ID, c1.ID, "Regular", 6),
                            new AlumnoInscripcion(0, alumnos[2].ID, c1.ID, "Libre", 2),
                            new AlumnoInscripcion(0, alumnos[0].ID, c2.ID, "Aprobado", 9),
                            new AlumnoInscripcion(0, alumnos[1].ID, c2.ID, "Inscripto", null)
                        );
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
