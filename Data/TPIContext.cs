using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

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
                var vendedorUser = new Entidades.Usuario(2, "VendedorApellido", "vendedor123", true, "VendedorNombre", "vendedor");
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
                    },
                    new
                    {
                        ID = vendedorUser.ID,
                        NombreUsuario = vendedorUser.NombreUsuario,
                        Clave = vendedorUser.Clave,
                        Nombre = vendedorUser.Nombre,
                        Apellido = vendedorUser.Apellido,
                        Habilitado = vendedorUser.Habilitado,
                        State = "Activo"
                    }
                );
            });
        }
    }
}
