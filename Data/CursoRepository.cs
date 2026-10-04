using Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CursoRepository : ICursoRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Curso curso)
        {
            using var context = CreateContext();
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var curso = await context.Cursos.FindAsync(id);
            if (curso != null)
            {
                context.Cursos.Remove(curso);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Curso?> GetAsync(int id)
        {
            using var context = CreateContext();
            var curso = await context.Cursos.FindAsync(id);
            return curso;
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            using var context = CreateContext();
            var cursos = await context.Cursos
                .Include(c => c.Materia)
                .Include(c => c.Comision)
                .OrderByDescending(c => c.AnioCalendario)
                .ToListAsync();
            return cursos;
        }

        public async Task<bool> UpdateAsync(Curso curso)
        {
            using var context = CreateContext();
            var existingCurso = await context.Cursos.FindAsync(curso.ID);
            if (existingCurso != null)
            {
                existingCurso.SetAnioCalendario(curso.AnioCalendario);
                existingCurso.SetCupo(curso.Cupo);
                existingCurso.SetDescripcion(curso.Descripcion);
                existingCurso.SetIDcomision(curso.IDcomision);
                existingCurso.SetIDmateria(curso.IDmateria);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Curso>> GetByCriteriaAsync(string texto)
        {
            const string sql = @"
                SELECT 
                    c.ID, c.AnioCalendario, c.Cupo, c.Descripcion, c.IDcomision, c.IDmateria,
                    m.Descripcion as MateriaDescripcion,
                    com.Descripcion as ComisionDescripcion
                FROM Cursos c
                INNER JOIN Materias m ON c.IDmateria = m.ID
                INNER JOIN Comisiones com ON c.IDcomision = com.ID
                WHERE c.Descripcion LIKE @SearchTerm
                   OR m.Descripcion LIKE @SearchTerm
                   OR com.Descripcion LIKE @SearchTerm
                ORDER BY c.AnioCalendario DESC";

            var cursos = new List<Curso>();
            using var context = CreateContext();
            string connectionString = context.Database.GetConnectionString();

            // Asignamos el patrón de búsqueda directamente
            string searchPattern = $"%{texto}%";
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@SearchTerm", searchPattern);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var curso = new Curso(
                    reader.GetInt32(0),    // ID
                    reader.GetInt32(1),    // AnioCalendario
                    reader.GetInt32(2),    // Cupo
                    reader.GetString(3),   // Descripcion
                    reader.GetInt32(4),    // IDcomision
                    reader.GetInt32(5)     // IDmateria
                );
                // Crear y asignar la Materia
                var materia = new Materia(reader.GetInt32(5), reader.GetString(6), 1, 1, 1);
                curso.SetMateria(materia);
                // Crear y asignar la Comisión
                var comision = new Comision(reader.GetInt32(4), 2000, reader.GetString(7), 1);
                curso.SetComision(comision);
                cursos.Add(curso);
            }
            return cursos;
        }
    }
}