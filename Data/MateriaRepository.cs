using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly TPIContext _context;

        public MateriaRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia != null)
            {
                _context.Materias.Remove(materia);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Materia?> GetAsync(int id)
        {
            return await _context.Materias
                .Include(m => m.Plan)
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Materias
                .Include(m => m.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<Materia>> GetByCriteriaAsync(string texto)
        {
            return await _context.Materias
                .Include(m => m.Plan)
                .Where(m => m.Descripcion.Contains(texto) || (m.Plan != null && m.Plan.Descripcion.Contains(texto)))
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Materia materia)
        {
            var existing = await _context.Materias.FindAsync(materia.ID);
            if (existing != null)
            {
                existing.SetDescripcion(materia.Descripcion);
                existing.SetHSSemanales(materia.HSSemanales);
                existing.SetHSTotales(materia.HSTotales);
                existing.SetIDPlan(materia.IDPlan);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
