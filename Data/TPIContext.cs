using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Comision> Comisiones { get; set; }
        public DbSet<Plan> Planes { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            // this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        internal TPIContext()
        {
            // this.Database.EnsureDeleted();
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
                    .WithOne()
                    .HasForeignKey(m => m.IDPlan);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID); // Cambiado de Id a ID por heredar de BusinessEntity

                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Habilitado)
                    .IsRequired();

                // Restricciones únicas
                entity.HasIndex(e => e.NombreUsuario)
                    .IsUnique();

                // Usuarios iniciales (Actualizados al nuevo constructor con ID)
                var adminUser = new Entidades.Usuario(1, "AdminApellido", "admin123", true, "AdminNombre", "admin");
                entity.HasData(
                    new
                    {
                        ID = adminUser.ID,
                        NombreUsuario = adminUser.NombreUsuario,
                        Clave = adminUser.Clave,
                        Nombre = adminUser.Nombre,
                        Apellido = adminUser.Apellido,
                        Habilitado = adminUser.Habilitado,
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
            });

        }
    }
}
