using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IMateriaService
    {
        Task<MateriaDTO?> AddAsync(MateriaDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<MateriaDTO?> GetByIdAsync(int id);
        Task<IEnumerable<MateriaDTO>> GetAllAsync();
        Task<IEnumerable<MateriaDTO>> GetByCriteriaAsync(string texto);
        Task<bool> UpdateAsync(MateriaDTO dto);
    }
}
