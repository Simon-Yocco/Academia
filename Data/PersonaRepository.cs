using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly TPIContext _context;

        public PersonaRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Persona?> GetAsync(int id)
        {
            return await _context.Personas
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas
                .Include(p => p.Plan)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
