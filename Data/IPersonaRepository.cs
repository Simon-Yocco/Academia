using Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IPersonaRepository
    {
        Task AddAsync(Persona persona);
        Task<bool> DeleteAsync(int id);
        Task<Persona?> GetAsync(int id);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<bool> UpdateAsync(Persona persona);
        Task<int> GetProximoLegajoAsync();
    }
}
