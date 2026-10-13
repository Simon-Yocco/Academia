using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ComisionRepository : IComisionRepository
    {
        private readonly TPIContext _context;

        public ComisionRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comision comision)
        {
            _context.Comisiones.Add(comision);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comision = await _context.Comisiones.FindAsync(id);
            if (comision != null)
            {
                _context.Comisiones.Remove(comision);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Comision?> GetAsync(int id)
        {
            return await _context.Comisiones
                .Include(c => c.Plan)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<IEnumerable<Comision>> GetAllAsync()
        {
            return await _context.Comisiones
                .Include(c => c.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comision>> GetByCriteriaAsync(string texto)
        {
            return await _context.Comisiones
                .Include(c => c.Plan)
                .Where(c => c.Descripcion.Contains(texto) || (c.Plan != null && c.Plan.Descripcion.Contains(texto)))
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Comision comision)
        {
            var existing = await _context.Comisiones.FindAsync(comision.ID);
            if (existing != null)
            {
                existing.SetDescripcion(comision.Descripcion);
                existing.SetAnioEspecialidad(comision.AnioEspecialidad);
                existing.SetIDPlan(comision.IDPlan);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
