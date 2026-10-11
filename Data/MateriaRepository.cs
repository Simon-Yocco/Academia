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

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Materias.ToListAsync();
        }
    }
}
