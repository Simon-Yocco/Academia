using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly TPIContext _context;

        public EspecialidadRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Especialidad especialidad)
        {
            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var esp = await _context.Especialidades.FindAsync(id);
            if (esp == null)
            {
                return false;
            }
            // chequeo si hay planes asociados a  la especialidad
            bool tienePlanes = await _context.Planes.AnyAsync(p => p.IDEspecialidad == id);
            if (tienePlanes)
            {
                throw new InvalidOperationException("No se puede eliminar la especialidad porque tiene planes de estudio asociados.");
            }
            _context.Especialidades.Remove(esp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Especialidad?> GetAsync(int id)
        {
            var esp = await _context.Especialidades.FindAsync(id);
            return esp;
        }

        public async Task<IEnumerable<Especialidad>> GetAllAsync()
        {
            var esps = await _context.Especialidades.ToListAsync();
            return esps;
        }

        public async Task<IEnumerable<Especialidad>> GetByCriteriaAsync(string texto)
        {
            const string sql = @"
                SELECT ID, Descripcion 
                FROM Especialidades 
                WHERE Descripcion LIKE @SearchTerm 
                ORDER BY Descripcion";

            var especialidades = new List<Especialidad>();
            string connectionString = _context.Database.GetConnectionString() ?? throw new InvalidOperationException("Cadena de conexión no configurada.");
            
            string searchPattern = $"%{texto}%";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@SearchTerm", searchPattern);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var especialidad = new Especialidad(
                    reader.GetInt32(0),   // ID
                    reader.GetString(1)   // Descripcion
                );
                especialidades.Add(especialidad);
            }

            return especialidades;
        }

        public async Task<bool> UpdateAsync(Especialidad especialidad)
        {
            var existingEsp = await _context.Especialidades.FindAsync(especialidad.ID);
            if (existingEsp != null)
            {
                existingEsp.SetDescripcion(especialidad.Descripcion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}