using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ComisionRepository : IComisionRepository
    {
        public async Task<IEnumerable<Comision>> GetAllAsync()
        {
            using var context = new TPIContext();
            return await context.Comisiones.ToListAsync();
        }
    }
}
