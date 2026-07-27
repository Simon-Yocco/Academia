using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface ICursoRepository
    {
        Task AddAsync(Curso curso);
        Task<bool> DeleteAsync(int id);
        Task<Curso?> GetAsync(int id);
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<bool> UpdateAsync(Curso curso);
    }
}