using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        internal TPIContext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Lee el appsettings.json directamente en vez de tener el texto fijo
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

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AnioCalendario)
                    .IsRequired();

                entity.Property(e => e.Cupo)
                    .IsRequired();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(c => c.Materia)
                    .WithMany()
                    .HasForeignKey(c => c.IDmateria)
                    .IsRequired(false);

                entity.HasOne(c => c.Comision)
                    .WithMany()
                    .HasForeignKey(c => c.IDcomision)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IDEspecialidad)
                    .IsRequired();

                entity.HasOne(e => e.Especialidad)
                    .WithMany()
                    .HasForeignKey(e => e.IDEspecialidad);

                entity.HasMany(e => e.Materias)
                    .WithOne(m => m.Plan)
                    .HasForeignKey(m => m.IDPlan);
            });
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FechaNacimiento)
                    .IsRequired();

                entity.Property(e => e.Legajo)
                    .IsRequired();

                entity.Property(e => e.TipoPersona)
                    .IsRequired();

                entity.Property(e => e.IDPlan)
                    .IsRequired(false);

                entity.HasOne(e => e.Plan)
                    .WithMany()
                    .HasForeignKey(e => e.IDPlan)
                    .IsRequired(false);

                // Persona inicial para el administrador
                var adminPersona = new Entidades.Persona(1, "AdminNombre", "AdminApellido", "Sistemas 123", "admin@academia.utn.edu.ar", "3411234567", new DateTime(1990, 1, 1), 1000, Entidades.Persona.TiposPersonas.Administrador, null);
                entity.HasData(
                    new
                    {
                        ID = adminPersona.ID,
                        Nombre = adminPersona.Nombre,
                        Apellido = adminPersona.Apellido,
                        Direccion = adminPersona.Direccion,
                        Email = adminPersona.Email,
                        Telefono = adminPersona.Telefono,
                        FechaNacimiento = adminPersona.FechaNacimiento,
                        Legajo = adminPersona.Legajo,
                        TipoPersona = adminPersona.TipoPersona,
                        IDPlan = adminPersona.IDPlan,
                        State = "Activo"
                    }
                );
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Habilitado)
                    .IsRequired();

                entity.Property(e => e.IDPersona)
                    .IsRequired();

                // Restricciones únicas
                entity.HasIndex(e => e.NombreUsuario)
                    .IsUnique();

                // Relación con Persona
                entity.HasOne(u => u.Persona)
                    .WithMany()
                    .HasForeignKey(u => u.IDPersona)
                    .IsRequired();

                // Usuario inicial vinculado a Persona ID = 1
                var adminUser = new Entidades.Usuario(1, "admin123", true, "admin", 1);
                entity.HasData(
                    new
                    {
                        ID = adminUser.ID,
                        NombreUsuario = adminUser.NombreUsuario,
                        Clave = adminUser.Clave,
                        Habilitado = adminUser.Habilitado,
                        IDPersona = adminUser.IDPersona,
                        State = "Activo"
                    }
                );
            });
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HSSemanales)
                    .IsRequired();

                entity.Property(e => e.HSTotales)
                    .IsRequired();

                entity.Property(e => e.IDPlan)
                    .IsRequired();
            });
            modelBuilder.Entity<Comision>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AnioEspecialidad)
                    .IsRequired();

                entity.Property(e => e.IDPlan)
                    .IsRequired();

                entity.HasOne(c => c.Plan)
                    .WithMany()
                    .HasForeignKey(c => c.IDPlan);
            });

        }
    }
}
