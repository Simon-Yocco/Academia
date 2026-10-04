using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IComisionRepository { Task<IEnumerable<Comision>> GetAllAsync(); }
}
