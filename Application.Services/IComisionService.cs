using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IComisionService
    {
        Task<ComisionDTO?> AddAsync(ComisionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<ComisionDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ComisionDTO>> GetAllAsync();
        Task<IEnumerable<ComisionDTO>> GetByCriteriaAsync(string texto);
        Task<bool> UpdateAsync(ComisionDTO dto);
    }
}
