using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MateriaRepository : IMateriaRepository
    {
        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            using var context = new TPIContext();
            return await context.Materias.ToListAsync();
        }
    }
}
