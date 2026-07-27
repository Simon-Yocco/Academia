using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IEspecialidadRepository
    {
        Task AddAsync(Especialidad especialidad);
        Task<bool> DeleteAsync(int id);
        Task<Especialidad?> GetAsync(int id);
        Task<IEnumerable<Especialidad>> GetAllAsync();
        Task<bool> UpdateAsync(Especialidad especialidad);
    }
}