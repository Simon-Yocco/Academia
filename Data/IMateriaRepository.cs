using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IMateriaRepository { Task<IEnumerable<Materia>> GetAllAsync(); }
}
