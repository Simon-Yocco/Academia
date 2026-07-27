using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IEspecialidadService
    {
        Task<IEnumerable<EspecialidadDTO>> GetAllAsync();
        Task<EspecialidadDTO?> GetByIdAsync(int id);
        Task AddAsync(EspecialidadDTO dto);
        Task<bool> UpdateAsync(int id, EspecialidadDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}