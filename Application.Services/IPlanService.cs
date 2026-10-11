using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IPlanService
    {
        Task<PlanDTO> AddAsync(PlanDTO dto);
        Task<IEnumerable<PlanDTO>> GetAllAsync();
        Task<PlanDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(PlanDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
