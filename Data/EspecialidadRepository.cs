using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Especialidad especialidad)
        {
            using var context = CreateContext();
            context.Especialidades.Add(especialidad);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var esp = await context.Especialidades.FindAsync(id);
            if (esp != null)
            {
                context.Especialidades.Remove(esp);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Especialidad?> GetAsync(int id)
        {
            using var context = CreateContext();
            var esp = await context.Especialidades.FindAsync(id);
            return esp;
        }

        public async Task<IEnumerable<Especialidad>> GetAllAsync()
        {
            using var context = CreateContext();
            var esps = await context.Especialidades.ToListAsync();
            return esps;
        }

        public async Task<bool> UpdateAsync(Especialidad especialidad)
        {
            using var context = CreateContext();
            var existingEsp = await context.Especialidades.FindAsync(especialidad.ID);
            if (existingEsp != null)
            {
                existingEsp.SetDescripcion(especialidad.Descripcion);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}