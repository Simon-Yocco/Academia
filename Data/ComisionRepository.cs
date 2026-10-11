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

        public async Task<IEnumerable<Comision>> GetAllAsync()
        {
            return await _context.Comisiones.ToListAsync();
        }
    }
}
