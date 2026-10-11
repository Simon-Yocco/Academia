using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IPlanRepository
    {
        Task AddAsync(Plan plan);
        Task<IEnumerable<Plan>> GetAllAsync();
        Task<Plan?> GetAsync(int id);
        Task<bool> UpdateAsync(Plan plan);
        Task<bool> DeleteAsync(int id);
    }
}
