using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Comision> Comisiones { get; set; }

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
            // Le decimos que use SQL Server (LocalDB que viene con Visual Studio)
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=AcademiaTPI;Integrated Security=true");

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
                // Creamos las entidades pasando por las validaciones de tu dominio
                var materia1 = new Entidades.Materia(1, "Sistemas y Organizaciones", 4, 128, 1);
                var materia2 = new Entidades.Materia(2, "Algoritmos y Estructuras de Datos", 5, 160, 1);
                entity.HasData(
                    new
                    {
                        ID = materia1.ID,
                        Descripcion = materia1.Descripcion,
                        HSSemanales = materia1.HSSemanales,
                        HSTotales = materia1.HSTotales,
                        IDPlan = materia1.IDPlan,
                        State = materia1.State
                    },
                    new
                    {
                        ID = materia2.ID,
                        Descripcion = materia2.Descripcion,
                        HSSemanales = materia2.HSSemanales,
                        HSTotales = materia2.HSTotales,
                        IDPlan = materia2.IDPlan,
                        State = materia2.State
                    }
                );
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
                // Creamos las entidades pasando por las validaciones de tu dominio
                var comision1 = new Entidades.Comision(1, 2026, "1K1", 1);
                var comision2 = new Entidades.Comision(2, 2026, "1K2", 1);
                entity.HasData(
                    new
                    {
                        ID = comision1.ID,
                        AnioEspecialidad = comision1.AnioEspecialidad,
                        Descripcion = comision1.Descripcion,
                        IDPlan = comision1.IDPlan,
                        State = comision1.State
                    },
                    new
                    {
                        ID = comision2.ID,
                        AnioEspecialidad = comision2.AnioEspecialidad,
                        Descripcion = comision2.Descripcion,
                        IDPlan = comision2.IDPlan,
                        State = comision2.State
                    }
                );
            });

        }
    }
}
