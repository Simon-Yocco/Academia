using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IComisionRepository
    {
        Task AddAsync(Comision comision);
        Task<bool> DeleteAsync(int id);
        Task<Comision?> GetAsync(int id);
        Task<IEnumerable<Comision>> GetAllAsync();
        Task<IEnumerable<Comision>> GetByCriteriaAsync(string texto);
        Task<bool> UpdateAsync(Comision comision);
    }
}
