using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IComisionService { Task<IEnumerable<ComisionDTO>> GetAllAsync(); }
}
