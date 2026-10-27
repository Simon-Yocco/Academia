using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

            // =========================================================================
            // 1. ESPECIALIDADES
            // =========================================================================
            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(255);

                entity.HasData(
                    new { ID = 1, Descripcion = "Ingeniería en Sistemas de Información", State = "Activo" },
                    new { ID = 2, Descripcion = "Ingeniería Química", State = "Activo" },
                    new { ID = 3, Descripcion = "Ingeniería Mecánica", State = "Activo" },
                    new { ID = 4, Descripcion = "Ingeniería Civil", State = "Activo" },
                    new { ID = 5, Descripcion = "Ingeniería Eléctrica", State = "Activo" }
                );
            });

            // =========================================================================
            // 2. PLANES
            // =========================================================================
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IDEspecialidad).IsRequired();

                entity.HasOne(e => e.Especialidad)
                    .WithMany()
                    .HasForeignKey(e => e.IDEspecialidad)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Materias)
                    .WithOne(m => m.Plan)
                    .HasForeignKey(m => m.IDPlan);

                entity.HasData(
                    new { ID = 1, Descripcion = "Plan 2023", IDEspecialidad = 1, State = "Activo" },
                    new { ID = 2, Descripcion = "Plan 2008", IDEspecialidad = 1, State = "Activo" },
                    new { ID = 3, Descripcion = "Plan 2023", IDEspecialidad = 2, State = "Activo" },
                    new { ID = 4, Descripcion = "Plan 2023", IDEspecialidad = 3, State = "Activo" },
                    new { ID = 5, Descripcion = "Plan 2023", IDEspecialidad = 4, State = "Activo" }
                );
            });

            // =========================================================================
            // 3. MATERIAS
            // =========================================================================
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
                    new { ID = 3, Descripcion = "Bases de Datos", HSSemanales = 4, HSTotales = 128, IDPlan = 1, State = "Activo" },
                    new { ID = 4, Descripcion = "Ingeniería de Software", HSSemanales = 6, HSTotales = 192, IDPlan = 1, State = "Activo" },
                    new { ID = 5, Descripcion = "Algoritmos y Estructuras de Datos", HSSemanales = 6, HSTotales = 192, IDPlan = 1, State = "Activo" },
                    new { ID = 6, Descripcion = "Arquitectura de Computadoras", HSSemanales = 4, HSTotales = 128, IDPlan = 1, State = "Activo" },
                    new { ID = 7, Descripcion = "Redes de Información", HSSemanales = 4, HSTotales = 128, IDPlan = 1, State = "Activo" },
                    new { ID = 8, Descripcion = "Análisis Matemático I", HSSemanales = 6, HSTotales = 192, IDPlan = 1, State = "Activo" },
                    new { ID = 9, Descripcion = "Álgebra y Geometría Analítica", HSSemanales = 5, HSTotales = 160, IDPlan = 1, State = "Activo" },
                    new { ID = 10, Descripcion = "Paradigmas de Programación", HSSemanales = 4, HSTotales = 128, IDPlan = 1, State = "Activo" },
                    new { ID = 11, Descripcion = "Química General", HSSemanales = 5, HSTotales = 160, IDPlan = 3, State = "Activo" },
                    new { ID = 12, Descripcion = "Termodinámica", HSSemanales = 4, HSTotales = 128, IDPlan = 3, State = "Activo" },
                    new { ID = 13, Descripcion = "Mecánica de los Fluidos", HSSemanales = 4, HSTotales = 128, IDPlan = 4, State = "Activo" }
                );
            });

            // =========================================================================
            // 4. COMISIONES
            // =========================================================================
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
                    new { ID = 1, Descripcion = "Comisión 1K1", AnioEspecialidad = 1, IDPlan = 1, State = "Activo" },
                    new { ID = 2, Descripcion = "Comisión 1K2", AnioEspecialidad = 1, IDPlan = 1, State = "Activo" },
                    new { ID = 3, Descripcion = "Comisión 2K1", AnioEspecialidad = 2, IDPlan = 1, State = "Activo" },
                    new { ID = 4, Descripcion = "Comisión 2K2", AnioEspecialidad = 2, IDPlan = 1, State = "Activo" },
                    new { ID = 5, Descripcion = "Comisión 3K1", AnioEspecialidad = 3, IDPlan = 1, State = "Activo" },
                    new { ID = 6, Descripcion = "Comisión 3K2", AnioEspecialidad = 3, IDPlan = 1, State = "Activo" },
                    new { ID = 7, Descripcion = "Comisión 4K1", AnioEspecialidad = 4, IDPlan = 1, State = "Activo" },
                    new { ID = 8, Descripcion = "Comisión 1Q1", AnioEspecialidad = 1, IDPlan = 3, State = "Activo" },
                    new { ID = 9, Descripcion = "Comisión 1M1", AnioEspecialidad = 1, IDPlan = 4, State = "Activo" }
                );
            });

            // =========================================================================
            // 5. PERSONAS
            // =========================================================================
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
                    // Administrador (ID: 1)
                    new
                    {
                        ID = 1,
                        Nombre = "Administrador",
                        Apellido = "General",
                        Direccion = "Zeballos 1320",
                        Email = "admin@academia.utn.edu.ar",
                        Telefono = "3411234567",
                        FechaNacimiento = new DateTime(1985, 1, 1),
                        Legajo = 1000,
                        TipoPersona = Persona.TiposPersonas.Administrador,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    // Docentes (ID: 2 a 7)
                    new
                    {
                        ID = 2,
                        Nombre = "Carlos",
                        Apellido = "Gómez",
                        Direccion = "Pellegrini 1540",
                        Email = "carlos.gomez@academia.utn.edu.ar",
                        Telefono = "3415551122",
                        FechaNacimiento = new DateTime(1978, 5, 15),
                        Legajo = 2001,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 3,
                        Nombre = "Laura",
                        Apellido = "Martínez",
                        Direccion = "Oroño 820",
                        Email = "laura.martinez@academia.utn.edu.ar",
                        Telefono = "3415552233",
                        FechaNacimiento = new DateTime(1982, 9, 23),
                        Legajo = 2002,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 4,
                        Nombre = "Roberto",
                        Apellido = "Sánchez",
                        Direccion = "Santa Fe 2100",
                        Email = "roberto.sanchez@academia.utn.edu.ar",
                        Telefono = "3415553344",
                        FechaNacimiento = new DateTime(1975, 12, 10),
                        Legajo = 2003,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 5,
                        Nombre = "Andrea",
                        Apellido = "Morales",
                        Direccion = "San Lorenzo 1450",
                        Email = "andrea.morales@academia.utn.edu.ar",
                        Telefono = "3415554411",
                        FechaNacimiento = new DateTime(1980, 4, 18),
                        Legajo = 2004,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 6,
                        Nombre = "Fernando",
                        Apellido = "Torres",
                        Direccion = "Balcarce 920",
                        Email = "fernando.torres@academia.utn.edu.ar",
                        Telefono = "3415555522",
                        FechaNacimiento = new DateTime(1976, 8, 30),
                        Legajo = 2005,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },
                    new
                    {
                        ID = 7,
                        Nombre = "Patricia",
                        Apellido = "Castro",
                        Direccion = "Alvear 1210",
                        Email = "patricia.castro@academia.utn.edu.ar",
                        Telefono = "3415556633",
                        FechaNacimiento = new DateTime(1984, 11, 12),
                        Legajo = 2006,
                        TipoPersona = Persona.TiposPersonas.Docente,
                        IDPlan = (int?)null,
                        State = "Activo"
                    },

                    // Alumnos (ID: 8 a 27)
                    new { ID = 8, Nombre = "Juan", Apellido = "Pérez", Direccion = "Mitre 450", Email = "juan.perez@academia.utn.edu.ar", Telefono = "3415554455", FechaNacimiento = new DateTime(2001, 8, 20), Legajo = 5001, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 9, Nombre = "María", Apellido = "López", Direccion = "Corrientes 890", Email = "maria.lopez@academia.utn.edu.ar", Telefono = "3415555566", FechaNacimiento = new DateTime(2002, 3, 10), Legajo = 5002, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 10, Nombre = "Lucas", Apellido = "Fernández", Direccion = "San Juan 1100", Email = "lucas.fernandez@academia.utn.edu.ar", Telefono = "3415556677", FechaNacimiento = new DateTime(2000, 11, 5), Legajo = 5003, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 11, Nombre = "Sofía", Apellido = "Rossi", Direccion = "Alvear 630", Email = "sofia.rossi@academia.utn.edu.ar", Telefono = "3415557788", FechaNacimiento = new DateTime(2002, 7, 14), Legajo = 5004, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 12, Nombre = "Mateo", Apellido = "Benítez", Direccion = "Córdoba 1820", Email = "mateo.benitez@academia.utn.edu.ar", Telefono = "3415558899", FechaNacimiento = new DateTime(2001, 4, 30), Legajo = 5005, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 13, Nombre = "Camila", Apellido = "Rodríguez", Direccion = "Mendoza 3200", Email = "camila.rodriguez@academia.utn.edu.ar", Telefono = "3415559900", FechaNacimiento = new DateTime(2003, 1, 19), Legajo = 5006, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 14, Nombre = "Tomás", Apellido = "Díaz", Direccion = "España 1420", Email = "tomas.diaz@academia.utn.edu.ar", Telefono = "3415550011", FechaNacimiento = new DateTime(2001, 10, 8), Legajo = 5007, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 15, Nombre = "Valentina", Apellido = "Romero", Direccion = "Urquiza 950", Email = "valentina.romero@academia.utn.edu.ar", Telefono = "3415551133", FechaNacimiento = new DateTime(2002, 6, 25), Legajo = 5008, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 16, Nombre = "Agustín", Apellido = "Navarro", Direccion = "Dorrego 740", Email = "agustin.navarro@academia.utn.edu.ar", Telefono = "3415552244", FechaNacimiento = new DateTime(2001, 2, 17), Legajo = 5009, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 17, Nombre = "Julieta", Apellido = "Herrera", Direccion = "Rioja 1890", Email = "julieta.herrera@academia.utn.edu.ar", Telefono = "3415553355", FechaNacimiento = new DateTime(2002, 9, 3), Legajo = 5010, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 18, Nombre = "Nicolás", Apellido = "Vega", Direccion = "Italia 1240", Email = "nicolas.vega@academia.utn.edu.ar", Telefono = "3415554466", FechaNacimiento = new DateTime(2000, 12, 28), Legajo = 5011, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 19, Nombre = "Lucía", Apellido = "Giménez", Direccion = "Salta 2300", Email = "lucia.gimenez@academia.utn.edu.ar", Telefono = "3415555577", FechaNacimiento = new DateTime(2003, 5, 21), Legajo = 5012, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 20, Nombre = "Franco", Apellido = "Morales", Direccion = "Paraguay 670", Email = "franco.morales@academia.utn.edu.ar", Telefono = "3415556688", FechaNacimiento = new DateTime(2001, 7, 11), Legajo = 5013, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 21, Nombre = "Martina", Apellido = "Silva", Direccion = "Catamarca 1680", Email = "martina.silva@academia.utn.edu.ar", Telefono = "3415557799", FechaNacimiento = new DateTime(2002, 8, 16), Legajo = 5014, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 22, Nombre = "Bruno", Apellido = "Acosta", Direccion = "Tucumán 1410", Email = "bruno.acosta@academia.utn.edu.ar", Telefono = "3415558800", FechaNacimiento = new DateTime(2001, 1, 24), Legajo = 5015, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 23, Nombre = "Paula", Apellido = "Medina", Direccion = "Moreno 830", Email = "paula.medina@academia.utn.edu.ar", Telefono = "3415559911", FechaNacimiento = new DateTime(2002, 10, 19), Legajo = 5016, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 24, Nombre = "Esteban", Apellido = "Flores", Direccion = "Laprida 1120", Email = "esteban.flores@academia.utn.edu.ar", Telefono = "3415550022", FechaNacimiento = new DateTime(2000, 4, 15), Legajo = 5017, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 25, Nombre = "Carolina", Apellido = "Ortiz", Direccion = "San Martín 1540", Email = "carolina.ortiz@academia.utn.edu.ar", Telefono = "3415551144", FechaNacimiento = new DateTime(2003, 3, 7), Legajo = 5018, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 26, Nombre = "Gonzalo", Apellido = "Ruiz", Direccion = "Sarmiento 980", Email = "gonzalo.ruiz@academia.utn.edu.ar", Telefono = "3415552255", FechaNacimiento = new DateTime(2001, 9, 27), Legajo = 5019, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" },
                    new { ID = 27, Nombre = "Micaela", Apellido = "Sucre", Direccion = "Buenos Aires 760", Email = "micaela.sucre@academia.utn.edu.ar", Telefono = "3415553366", FechaNacimiento = new DateTime(2002, 11, 14), Legajo = 5020, TipoPersona = Persona.TiposPersonas.Alumno, IDPlan = (int?)1, State = "Activo" }
                );
            });

            // =========================================================================
            // 6. USUARIOS
            // =========================================================================
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
                    // Administrador
                    new { ID = 1, NombreUsuario = "admin", Clave = "admin123", Habilitado = true, IDPersona = 1, State = "Activo" },

                    // Docentes (Usuario: nombre en minúsculas, Clave: legajo)
                    new { ID = 2, NombreUsuario = "carlos", Clave = "2001", Habilitado = true, IDPersona = 2, State = "Activo" },
                    new { ID = 3, NombreUsuario = "laura", Clave = "2002", Habilitado = true, IDPersona = 3, State = "Activo" },
                    new { ID = 4, NombreUsuario = "roberto", Clave = "2003", Habilitado = true, IDPersona = 4, State = "Activo" },
                    new { ID = 5, NombreUsuario = "andrea", Clave = "2004", Habilitado = true, IDPersona = 5, State = "Activo" },
                    new { ID = 6, NombreUsuario = "fernando", Clave = "2005", Habilitado = true, IDPersona = 6, State = "Activo" },
                    new { ID = 7, NombreUsuario = "patricia", Clave = "2006", Habilitado = true, IDPersona = 7, State = "Activo" },

                    // Alumnos (Usuario: nombre en minúsculas, Clave: legajo)
                    new { ID = 8, NombreUsuario = "juan", Clave = "5001", Habilitado = true, IDPersona = 8, State = "Activo" },
                    new { ID = 9, NombreUsuario = "maria", Clave = "5002", Habilitado = true, IDPersona = 9, State = "Activo" },
                    new { ID = 10, NombreUsuario = "lucas", Clave = "5003", Habilitado = true, IDPersona = 10, State = "Activo" },
                    new { ID = 11, NombreUsuario = "sofia", Clave = "5004", Habilitado = true, IDPersona = 11, State = "Activo" },
                    new { ID = 12, NombreUsuario = "mateo", Clave = "5005", Habilitado = true, IDPersona = 12, State = "Activo" },
                    new { ID = 13, NombreUsuario = "camila", Clave = "5006", Habilitado = true, IDPersona = 13, State = "Activo" },
                    new { ID = 14, NombreUsuario = "tomas", Clave = "5007", Habilitado = true, IDPersona = 14, State = "Activo" },
                    new { ID = 15, NombreUsuario = "valentina", Clave = "5008", Habilitado = true, IDPersona = 15, State = "Activo" },
                    new { ID = 16, NombreUsuario = "agustin", Clave = "5009", Habilitado = true, IDPersona = 16, State = "Activo" },
                    new { ID = 17, NombreUsuario = "julieta", Clave = "5010", Habilitado = true, IDPersona = 17, State = "Activo" },
                    new { ID = 18, NombreUsuario = "nicolas", Clave = "5011", Habilitado = true, IDPersona = 18, State = "Activo" },
                    new { ID = 19, NombreUsuario = "lucia", Clave = "5012", Habilitado = true, IDPersona = 19, State = "Activo" },
                    new { ID = 20, NombreUsuario = "franco", Clave = "5013", Habilitado = true, IDPersona = 20, State = "Activo" },
                    new { ID = 21, NombreUsuario = "martina", Clave = "5014", Habilitado = true, IDPersona = 21, State = "Activo" },
                    new { ID = 22, NombreUsuario = "bruno", Clave = "5015", Habilitado = true, IDPersona = 22, State = "Activo" },
                    new { ID = 23, NombreUsuario = "paula", Clave = "5016", Habilitado = true, IDPersona = 23, State = "Activo" },
                    new { ID = 24, NombreUsuario = "esteban", Clave = "5017", Habilitado = true, IDPersona = 24, State = "Activo" },
                    new { ID = 25, NombreUsuario = "carolina", Clave = "5018", Habilitado = true, IDPersona = 25, State = "Activo" },
                    new { ID = 26, NombreUsuario = "gonzalo", Clave = "5019", Habilitado = true, IDPersona = 26, State = "Activo" },
                    new { ID = 27, NombreUsuario = "micaela", Clave = "5020", Habilitado = true, IDPersona = 27, State = "Activo" }
                );
            });

            // =========================================================================
            // 7. CURSOS
            // =========================================================================
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
                    // Materia 2: Desarrollo de Software .NET
                    new { ID = 1, Descripcion = ".NET - Turno Tarde", AnioCalendario = 2026, Cupo = 35, IDmateria = 2, IDcomision = 5, IDdocente = 2, State = "Activo" },
                    new { ID = 2, Descripcion = ".NET - Turno Noche", AnioCalendario = 2026, Cupo = 30, IDmateria = 2, IDcomision = 6, IDdocente = 5, State = "Activo" },

                    // Materia 3: Bases de Datos
                    new { ID = 3, Descripcion = "Bases de Datos - Turno Mañana", AnioCalendario = 2026, Cupo = 35, IDmateria = 3, IDcomision = 5, IDdocente = 3, State = "Activo" },
                    new { ID = 4, Descripcion = "Bases de Datos - Turno Noche", AnioCalendario = 2026, Cupo = 30, IDmateria = 3, IDcomision = 6, IDdocente = 6, State = "Activo" },

                    // Materia 1: Sistemas Operativos
                    new { ID = 5, Descripcion = "Sistemas Operativos - Turno Mañana", AnioCalendario = 2026, Cupo = 35, IDmateria = 1, IDcomision = 3, IDdocente = 2, State = "Activo" },
                    new { ID = 6, Descripcion = "Sistemas Operativos - Turno Tarde", AnioCalendario = 2026, Cupo = 30, IDmateria = 1, IDcomision = 4, IDdocente = 4, State = "Activo" },

                    // Materia 5: Algoritmos y Estructuras de Datos
                    new { ID = 7, Descripcion = "Algoritmos - Turno Mañana", AnioCalendario = 2026, Cupo = 40, IDmateria = 5, IDcomision = 1, IDdocente = 4, State = "Activo" },
                    new { ID = 8, Descripcion = "Algoritmos - Turno Tarde", AnioCalendario = 2026, Cupo = 40, IDmateria = 5, IDcomision = 2, IDdocente = 7, State = "Activo" },

                    // Materia 4: Ingeniería de Software
                    new { ID = 9, Descripcion = "Ing. de Software - Turno Tarde", AnioCalendario = 2026, Cupo = 30, IDmateria = 4, IDcomision = 5, IDdocente = 3, State = "Activo" },
                    new { ID = 10, Descripcion = "Ing. de Software - Turno Noche", AnioCalendario = 2026, Cupo = 25, IDmateria = 4, IDcomision = 6, IDdocente = 6, State = "Activo" },

                    // Materia 10: Paradigmas de Programación
                    new { ID = 11, Descripcion = "Paradigmas - Turno Mañana", AnioCalendario = 2026, Cupo = 35, IDmateria = 10, IDcomision = 3, IDdocente = 5, State = "Activo" },
                    new { ID = 12, Descripcion = "Paradigmas - Turno Noche", AnioCalendario = 2026, Cupo = 30, IDmateria = 10, IDcomision = 4, IDdocente = 2, State = "Activo" },

                    // Materia 8: Análisis Matemático I
                    new { ID = 13, Descripcion = "Análisis Matemático I - Turno Mañana", AnioCalendario = 2026, Cupo = 45, IDmateria = 8, IDcomision = 1, IDdocente = 6, State = "Activo" },
                    new { ID = 14, Descripcion = "Análisis Matemático I - Turno Noche", AnioCalendario = 2026, Cupo = 45, IDmateria = 8, IDcomision = 2, IDdocente = 4, State = "Activo" },

                    // Materia 7: Redes de Información
                    new { ID = 15, Descripcion = "Redes de Información - Turno Tarde", AnioCalendario = 2026, Cupo = 30, IDmateria = 7, IDcomision = 7, IDdocente = 7, State = "Activo" }
                );
            });

            // =========================================================================
            // 8. ALUMNOS INSCRIPCIONES
            // =========================================================================
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
                    // --- CURSO 1: .NET - Comisión 3K1 (10 alumnos inscriptos) ---
                    new { ID = 1, IDAlumno = 8, IDCurso = 1, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 2, IDAlumno = 9, IDCurso = 1, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 3, IDAlumno = 10, IDCurso = 1, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 4, IDAlumno = 11, IDCurso = 1, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 5, IDAlumno = 12, IDCurso = 1, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 6, IDAlumno = 13, IDCurso = 1, Condicion = "Libre", Nota = (int?)2, State = "Activo" },
                    new { ID = 7, IDAlumno = 14, IDCurso = 1, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 8, IDAlumno = 15, IDCurso = 1, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 9, IDAlumno = 16, IDCurso = 1, Condicion = "Regular", Nota = (int?)5, State = "Activo" },
                    new { ID = 10, IDAlumno = 17, IDCurso = 1, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },

                    // --- CURSO 2: .NET - Comisión 3K2 (8 alumnos inscriptos) ---
                    new { ID = 11, IDAlumno = 18, IDCurso = 2, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 12, IDAlumno = 19, IDCurso = 2, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 13, IDAlumno = 20, IDCurso = 2, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 14, IDAlumno = 21, IDCurso = 2, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 15, IDAlumno = 22, IDCurso = 2, Condicion = "Libre", Nota = (int?)3, State = "Activo" },
                    new { ID = 16, IDAlumno = 23, IDCurso = 2, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 17, IDAlumno = 24, IDCurso = 2, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 18, IDAlumno = 25, IDCurso = 2, Condicion = "Regular", Nota = (int?)6, State = "Activo" },

                    // --- CURSO 3: Bases de Datos - Comisión 3K1 (11 alumnos inscriptos) ---
                    new { ID = 19, IDAlumno = 8, IDCurso = 3, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 20, IDAlumno = 9, IDCurso = 3, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 21, IDAlumno = 10, IDCurso = 3, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 22, IDAlumno = 11, IDCurso = 3, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 23, IDAlumno = 12, IDCurso = 3, Condicion = "Regular", Nota = (int?)5, State = "Activo" },
                    new { ID = 24, IDAlumno = 13, IDCurso = 3, Condicion = "Libre", Nota = (int?)2, State = "Activo" },
                    new { ID = 25, IDAlumno = 14, IDCurso = 3, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 26, IDAlumno = 15, IDCurso = 3, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 27, IDAlumno = 16, IDCurso = 3, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 28, IDAlumno = 17, IDCurso = 3, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 29, IDAlumno = 26, IDCurso = 3, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },

                    // --- CURSO 4: Bases de Datos - Comisión 3K2 (9 alumnos inscriptos) ---
                    new { ID = 30, IDAlumno = 18, IDCurso = 4, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 31, IDAlumno = 19, IDCurso = 4, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 32, IDAlumno = 20, IDCurso = 4, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 33, IDAlumno = 21, IDCurso = 4, Condicion = "Libre", Nota = (int?)3, State = "Activo" },
                    new { ID = 34, IDAlumno = 22, IDCurso = 4, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 35, IDAlumno = 23, IDCurso = 4, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 36, IDAlumno = 24, IDCurso = 4, Condicion = "Regular", Nota = (int?)5, State = "Activo" },
                    new { ID = 37, IDAlumno = 25, IDCurso = 4, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 38, IDAlumno = 27, IDCurso = 4, Condicion = "Regular", Nota = (int?)6, State = "Activo" },

                    // --- CURSO 5: Sistemas Operativos - Comisión 2K1 (8 alumnos inscriptos) ---
                    new { ID = 39, IDAlumno = 8, IDCurso = 5, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 40, IDAlumno = 9, IDCurso = 5, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 41, IDAlumno = 10, IDCurso = 5, Condicion = "Libre", Nota = (int?)2, State = "Activo" },
                    new { ID = 42, IDAlumno = 11, IDCurso = 5, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 43, IDAlumno = 12, IDCurso = 5, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 44, IDAlumno = 13, IDCurso = 5, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 45, IDAlumno = 14, IDCurso = 5, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 46, IDAlumno = 15, IDCurso = 5, Condicion = "Regular", Nota = (int?)5, State = "Activo" },

                    // --- CURSO 6: Sistemas Operativos - Comisión 2K2 (7 alumnos inscriptos) ---
                    new { ID = 47, IDAlumno = 16, IDCurso = 6, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 48, IDAlumno = 17, IDCurso = 6, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 49, IDAlumno = 18, IDCurso = 6, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 50, IDAlumno = 19, IDCurso = 6, Condicion = "Libre", Nota = (int?)3, State = "Activo" },
                    new { ID = 51, IDAlumno = 20, IDCurso = 6, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 52, IDAlumno = 21, IDCurso = 6, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 53, IDAlumno = 22, IDCurso = 6, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },

                    // --- CURSO 7: Algoritmos y Estructuras - Comisión 1K1 (12 alumnos inscriptos) ---
                    new { ID = 54, IDAlumno = 8, IDCurso = 7, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 55, IDAlumno = 9, IDCurso = 7, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 56, IDAlumno = 10, IDCurso = 7, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 57, IDAlumno = 11, IDCurso = 7, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 58, IDAlumno = 12, IDCurso = 7, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 59, IDAlumno = 13, IDCurso = 7, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 60, IDAlumno = 14, IDCurso = 7, Condicion = "Libre", Nota = (int?)2, State = "Activo" },
                    new { ID = 61, IDAlumno = 15, IDCurso = 7, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 62, IDAlumno = 16, IDCurso = 7, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 63, IDAlumno = 17, IDCurso = 7, Condicion = "Regular", Nota = (int?)5, State = "Activo" },
                    new { ID = 64, IDAlumno = 26, IDCurso = 7, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 65, IDAlumno = 27, IDCurso = 7, Condicion = "Regular", Nota = (int?)6, State = "Activo" },

                    // --- CURSO 8: Algoritmos y Estructuras - Comisión 1K2 (10 alumnos inscriptos) ---
                    new { ID = 66, IDAlumno = 18, IDCurso = 8, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 67, IDAlumno = 19, IDCurso = 8, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 68, IDAlumno = 20, IDCurso = 8, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 69, IDAlumno = 21, IDCurso = 8, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 70, IDAlumno = 22, IDCurso = 8, Condicion = "Libre", Nota = (int?)3, State = "Activo" },
                    new { ID = 71, IDAlumno = 23, IDCurso = 8, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 72, IDAlumno = 24, IDCurso = 8, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 73, IDAlumno = 25, IDCurso = 8, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 74, IDAlumno = 12, IDCurso = 8, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 75, IDAlumno = 15, IDCurso = 8, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },

                    // --- CURSO 9: Ingeniería de Software - Comisión 3K1 (6 alumnos inscriptos) ---
                    new { ID = 76, IDAlumno = 8, IDCurso = 9, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 77, IDAlumno = 10, IDCurso = 9, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 78, IDAlumno = 11, IDCurso = 9, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 79, IDAlumno = 13, IDCurso = 9, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 80, IDAlumno = 14, IDCurso = 9, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 81, IDAlumno = 16, IDCurso = 9, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },

                    // --- CURSO 10: Ingeniería de Software - Comisión 3K2 (5 alumnos inscriptos) ---
                    new { ID = 82, IDAlumno = 18, IDCurso = 10, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 83, IDAlumno = 20, IDCurso = 10, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 84, IDAlumno = 22, IDCurso = 10, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 85, IDAlumno = 24, IDCurso = 10, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 86, IDAlumno = 25, IDCurso = 10, Condicion = "Libre", Nota = (int?)2, State = "Activo" },

                    // --- CURSO 11: Paradigmas - Comisión 2K1 (7 alumnos inscriptos) ---
                    new { ID = 87, IDAlumno = 8, IDCurso = 11, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 88, IDAlumno = 9, IDCurso = 11, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 89, IDAlumno = 10, IDCurso = 11, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 90, IDAlumno = 12, IDCurso = 11, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 91, IDAlumno = 14, IDCurso = 11, Condicion = "Libre", Nota = (int?)3, State = "Activo" },
                    new { ID = 92, IDAlumno = 15, IDCurso = 11, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 93, IDAlumno = 17, IDCurso = 11, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },

                    // --- CURSO 12: Paradigmas - Comisión 2K2 (6 alumnos inscriptos) ---
                    new { ID = 94, IDAlumno = 19, IDCurso = 12, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 95, IDAlumno = 21, IDCurso = 12, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 96, IDAlumno = 23, IDCurso = 12, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 97, IDAlumno = 25, IDCurso = 12, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 98, IDAlumno = 26, IDCurso = 12, Condicion = "Regular", Nota = (int?)5, State = "Activo" },
                    new { ID = 99, IDAlumno = 27, IDCurso = 12, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },

                    // --- CURSO 13: Análisis Matemático I - Comisión 1K1 (9 alumnos inscriptos) ---
                    new { ID = 100, IDAlumno = 8, IDCurso = 13, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 101, IDAlumno = 10, IDCurso = 13, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 102, IDAlumno = 11, IDCurso = 13, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 103, IDAlumno = 12, IDCurso = 13, Condicion = "Libre", Nota = (int?)2, State = "Activo" },
                    new { ID = 104, IDAlumno = 13, IDCurso = 13, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 105, IDAlumno = 15, IDCurso = 13, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 106, IDAlumno = 17, IDCurso = 13, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 107, IDAlumno = 20, IDCurso = 13, Condicion = "Regular", Nota = (int?)5, State = "Activo" },
                    new { ID = 108, IDAlumno = 22, IDCurso = 13, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },

                    // --- CURSO 14: Análisis Matemático I - Comisión 1K2 (8 alumnos inscriptos) ---
                    new { ID = 109, IDAlumno = 18, IDCurso = 14, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },
                    new { ID = 110, IDAlumno = 19, IDCurso = 14, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 111, IDAlumno = 21, IDCurso = 14, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 112, IDAlumno = 23, IDCurso = 14, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 113, IDAlumno = 24, IDCurso = 14, Condicion = "Libre", Nota = (int?)3, State = "Activo" },
                    new { ID = 114, IDAlumno = 25, IDCurso = 14, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 115, IDAlumno = 26, IDCurso = 14, Condicion = "Aprobado", Nota = (int?)7, State = "Activo" },
                    new { ID = 116, IDAlumno = 27, IDCurso = 14, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" },

                    // --- CURSO 15: Redes de Información - Comisión 4K1 (6 alumnos inscriptos) ---
                    new { ID = 117, IDAlumno = 8, IDCurso = 15, Condicion = "Aprobado", Nota = (int?)10, State = "Activo" },
                    new { ID = 118, IDAlumno = 9, IDCurso = 15, Condicion = "Aprobado", Nota = (int?)9, State = "Activo" },
                    new { ID = 119, IDAlumno = 11, IDCurso = 15, Condicion = "Regular", Nota = (int?)7, State = "Activo" },
                    new { ID = 120, IDAlumno = 14, IDCurso = 15, Condicion = "Regular", Nota = (int?)6, State = "Activo" },
                    new { ID = 121, IDAlumno = 16, IDCurso = 15, Condicion = "Inscripto", Nota = (int?)null, State = "Activo" },
                    new { ID = 122, IDAlumno = 18, IDCurso = 15, Condicion = "Aprobado", Nota = (int?)8, State = "Activo" }
                );
            });
        }
    }
}
