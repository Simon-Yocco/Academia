using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<UsuarioDTO?> GetByUsernameAsync(string username);
        Task<UsuarioDTO?> AddAsync(UsuarioDTO dto);
        Task<bool> UpdateAsync(UsuarioDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
