using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<Materia?> GetAsync(int id);
        Task<IEnumerable<Materia>> GetByCriteriaAsync(string texto);
        Task AddAsync(Materia materia);
        Task<bool> UpdateAsync(Materia materia);
        Task<bool> DeleteAsync(int id);
    }
}
