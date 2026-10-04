using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IMateriaService { Task<IEnumerable<MateriaDTO>> GetAllAsync(); }
}
