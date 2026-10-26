using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Comision> Comisiones { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<AlumnoInscripcion> AlumnosInscripciones { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        internal TPIContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                string? connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. ESPECIALIDADES
            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(255);

                entity.HasData(
                    new { ID = 1, Descripcion = "Ingeniería en Sistemas de Información", State = "Activo" },
                    new { ID = 2, Descripcion = "Ingeniería Química", State = "Activo" }
                );
            });

            // 2. PLANES
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IDEspecialidad).IsRequired();

                entity.HasOne(e => e.Especialidad)
                    .WithMany()
                    .HasForeignKey(e => e.IDEspecialidad);

                entity.HasMany(e => e.Materias)
                    .WithOne(m => m.Plan)
                    .HasForeignKey(m => m.IDPlan);

                entity.HasData(
                    new { ID = 1, Descripcion = "Plan 2023", IDEspecialidad = 1, State = "Activo" },
                    new { ID = 2, Descripcion = "Plan 2008", IDEspecialidad = 1, State = "Activo" }
                );
            });

            // 3. MATERIAS
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(50);
                entity.Property(e => e.HSSemanales).IsRequired();
                entity.Property(e => e.HSTotales).IsRequired();
                entity.Property(e => e.IDPlan).IsRequired();

                entity.HasData(
                    new { ID = 1, Descripcion = "Sistemas Operativos", HSSemanales = 6, HSTotales = 192, IDPlan = 1, State = "Activo" },
                    new { ID = 2, Descripcion = "Desarrollo de Software .NET", HSSemanales = 4, HSTotales = 128, IDPlan = 1, State = "Activo" },
                    new { ID = 3, Descripcion = "Bases de Datos", HSSemanales = 4, HSTotales = 128, IDPlan = 1, State = "Activo" }
                );
            });

            // 4. COMISIONES
            modelBuilder.Entity<Comision>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AnioEspecialidad).IsRequired();
                entity.Property(e => e.IDPlan).IsRequired();

                entity.HasOne(c => c.Plan)
                    .WithMany()
                    .HasForeignKey(c => c.IDPlan);

                entity.HasData(
                    new { ID = 1, Descripcion = "Comisión 3K1", AnioEspecialidad = 3, IDPlan = 1, State = "Activo" },
                    new { ID = 2, Descripcion = "Comisión 3K2", AnioEspecialidad = 3, IDPlan = 1, State = "Activo" }
                );
            });

            // 5. PERSONAS
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FechaNacimiento).IsRequired();
                entity.Property(e => e.Legajo).IsRequired();
                entity.Property(e => e.TipoPersona).IsRequired();
                entity.Property(e => e.IDPlan).IsRequired(false);

                entity.HasOne(e => e.Plan)
                    .WithMany()
                    .HasForeignKey(e => e.IDPlan)
                    .IsRequired(false);

                entity.HasData(
                    new
                    {
                        ID = 1,
                        Nombre = "Administrador",
                        Apellido = "General",
                        Direccion = "Sistemas 123",
                        Email = "admin@academia.utn.edu.ar",
                        Telefono = "3411234567",
                        FechaNacimiento = new DateTime(1990, 1, 1),
                        Legajo = 1000,
                        TipoPersona = Persona.TiposPersonas.Administrador,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 2,
                        Nombre = "Carlos",
                        Apellido = "Gómez",
                        Direccion = "Pellegrini 1234",
                        Email = "profesor@academia.utn.edu.ar",
                        Telefono = "3415551122",
                        FechaNacimiento = new DateTime(1980, 5, 15),
                        Legajo = 2001,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 3,
                        Nombre = "Juan",
                        Apellido = "Pérez",
                        Direccion = "Mitre 450",
                        Email = "alumno@academia.utn.edu.ar",
                        Telefono = "3415553344",
                        FechaNacimiento = new DateTime(2001, 8, 20),
                        Legajo = 5001,
                        TipoPersona = Persona.TiposPersonas.Alumno,
                        IDPlan = (int?)1,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 4,
                        Nombre = "María",
                        Apellido = "López",
                        Direccion = "Corrientes 890",
                        Email = "maria@academia.utn.edu.ar",
                        Telefono = "3415555566",
                        FechaNacimiento = new DateTime(2002, 3, 10),
                        Legajo = 5002,
                        TipoPersona = Persona.TiposPersonas.Alumno,
                        IDPlan = (int?)1,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 5,
                        Nombre = "Lucas",
                        Apellido = "Fernández",
                        Direccion = "San Juan 1100",
                        Email = "lucas@academia.utn.edu.ar",
                        Telefono = "3415557788",
                        FechaNacimiento = new DateTime(2000, 11, 5),
                        Legajo = 5003,
                        TipoPersona = Persona.TiposPersonas.Alumno,
                        IDPlan = (int?)1,
                        State = "Activo"
                    }
                );
            });

            // 6. USUARIOS
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.NombreUsuario).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Habilitado).IsRequired();
                entity.Property(e => e.IDPersona).IsRequired();

                entity.HasIndex(e => e.NombreUsuario).IsUnique();

                entity.HasOne(u => u.Persona)
                    .WithMany()
                    .HasForeignKey(u => u.IDPersona)
                    .IsRequired();

                entity.HasData(
                    new { ID = 1, NombreUsuario = "admin", Clave = "admin123", Habilitado = true, IDPersona = 1, State = "Activo" },
                    new { ID = 2, NombreUsuario = "profesor", Clave = "profe123", Habilitado = true, IDPersona = 2, State = "Activo" },
                    new { ID = 3, NombreUsuario = "alumno", Clave = "alumno123", Habilitado = true, IDPersona = 3, State = "Activo" },
                    new { ID = 4, NombreUsuario = "maria", Clave = "maria123", Habilitado = true, IDPersona = 4, State = "Activo" },
                    new { ID = 5, NombreUsuario = "lucas", Clave = "lucas123", Habilitado = true, IDPersona = 5, State = "Activo" }
                );
            });

            // 7. CURSOS
            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.AnioCalendario).IsRequired();
                entity.Property(e => e.Cupo).IsRequired();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(255);

                entity.HasOne(c => c.Materia)
                    .WithMany()
                    .HasForeignKey(c => c.IDmateria)
                    .IsRequired(false);

                entity.HasOne(c => c.Docente)
                    .WithMany()
                    .HasForeignKey(c => c.IDdocente)
                    .IsRequired();

                entity.HasOne(c => c.Comision)
                    .WithMany()
                    .HasForeignKey(c => c.IDcomision)
                    .IsRequired(false);

                entity.HasData(
                    new
                    {
                        ID = 1,
                        Descripcion = "Curso .NET - Turno Noche",
                        AnioCalendario = 2026,
                        Cupo = 30,
                        IDmateria = 2,
                        IDcomision = 1,
                        IDdocente = 2,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 2,
                        Descripcion = "Curso Bases de Datos - Turno Mañana",
                        AnioCalendario = 2026,
                        Cupo = 25,
                        IDmateria = 3,
                        IDcomision = 1,
                        IDdocente = 2,
                        State = "Activo"
                    }
                );
            });

            // 8. ALUMNOS INSCRIPCIONES
            modelBuilder.Entity<AlumnoInscripcion>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Condicion).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Nota).IsRequired(false);
                entity.Property(e => e.IDAlumno).IsRequired();
                entity.Property(e => e.IDCurso).IsRequired();

                entity.HasOne(e => e.Alumno)
                    .WithMany()
                    .HasForeignKey(e => e.IDAlumno)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Curso)
                    .WithMany()
                    .HasForeignKey(e => e.IDCurso)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new { ID = 1, IDAlumno = 3, IDCurso = 1, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 2, IDAlumno = 4, IDCurso = 1, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 3, IDAlumno = 5, IDCurso = 1, Condicion = "Libre", Nota = (int?)2, State = "Activo" },
                    new { ID = 4, IDAlumno = 3, IDCurso = 2, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 5, IDAlumno = 4, IDCurso = 2, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" }
                );
            });
        }
    }
}
