using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface ICursoService
    {
        Task<IEnumerable<CursoDTO>> GetAllAsync();
        Task<CursoDTO?> GetByIdAsync(int id);
        Task<CursoDTO?> AddAsync(CursoDTO cursoDto);
        Task<bool> UpdateAsync(CursoDTO cursoDto);
        Task<bool> DeleteAsync(int id);
    }
}