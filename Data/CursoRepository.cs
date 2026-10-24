using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class CursoRepository : ICursoRepository
    {
        private readonly TPIContext _context;

        public CursoRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Curso?> GetAsync(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Materia)
                .Include(c => c.Comision)
                .Include(c => c.Docente)
                .FirstOrDefaultAsync(c => c.ID == id);
            return curso;
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Materia)
                .Include(c => c.Comision)
                .Include(c => c.Docente)
                .OrderByDescending(c => c.AnioCalendario)
                .ToListAsync();
            return cursos;
        }

        public async Task<bool> UpdateAsync(Curso curso)
        {
            var existingCurso = await _context.Cursos.FindAsync(curso.ID);
            if (existingCurso != null)
            {
                existingCurso.SetAnioCalendario(curso.AnioCalendario);
                existingCurso.SetCupo(curso.Cupo);
                existingCurso.SetDescripcion(curso.Descripcion);
                existingCurso.SetIDcomision(curso.IDcomision);
                existingCurso.SetIDmateria(curso.IDmateria);
                existingCurso.SetIDdocente(curso.IDdocente);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
        public async Task<IEnumerable<Curso>> GetByCriteriaAsync(string texto)
        {
            const string sql = @"
                SELECT 
                    c.ID, c.AnioCalendario, c.Cupo, c.Descripcion, c.IDcomision, c.IDmateria, c.IDdocente,
                    m.Descripcion as MateriaDescripcion,
                    com.Descripcion as ComisionDescripcion,
                    p.Nombre as DocenteNombre,
                    p.Apellido as DocenteApellido
                FROM Cursos c
                INNER JOIN Materias m ON c.IDmateria = m.ID
                INNER JOIN Comisiones com ON c.IDcomision = com.ID
                INNER JOIN Personas p ON c.IDDocente = p.ID
                WHERE c.Descripcion LIKE @SearchTerm
                   OR m.Descripcion LIKE @SearchTerm
                   OR com.Descripcion LIKE @SearchTerm
                   OR (p.Nombre + ' ' + p.Apellido) LIKE @SearchTerm
                   OR (p.Apellido + ' ' + p.Nombre) LIKE @SearchTerm
                ORDER BY c.AnioCalendario DESC";

            var cursos = new List<Curso>();
            string connectionString = _context.Database.GetConnectionString();

            // Asignamos el patron de busqueda directamente
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
                    reader.GetInt32(5),    // IDmateria
                    reader.GetInt32(6)     // IDdocente
                );
                // Crear y asignar la Materia
                var materia = new Materia(reader.GetInt32(5), reader.GetString(7), 1, 1, 1);
                curso.SetMateria(materia);
                // Crear y asignar la Comision
                var comision = new Comision(reader.GetInt32(4), 2000, reader.GetString(8), 1);
                curso.SetComision(comision);
                // Crear y asignar el Docente
                var docente = new Persona(reader.GetInt32(6), reader.GetString(9), reader.GetString(10), "", "", "", DateTime.Now, 1, Persona.TiposPersonas.Docente);
                curso.SetDocente(docente);
                
                cursos.Add(curso);

            }
            return cursos;
        }
    }
}