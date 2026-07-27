using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private static readonly List<Especialidad> _especialidades = new List<Especialidad>();
        private static int _nextId = 1;

        public Task AddAsync(Especialidad especialidad)
        {
            especialidad.ID = _nextId++;
            _especialidades.Add(especialidad);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var esp = _especialidades.FirstOrDefault(e => e.ID == id);
            if (esp != null)
            {
                _especialidades.Remove(esp);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Especialidad?> GetAsync(int id)
        {
            var esp = _especialidades.FirstOrDefault(e => e.ID == id);
            return Task.FromResult(esp);
        }

        public Task<IEnumerable<Especialidad>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Especialidad>>(_especialidades.ToList());
        }

        public Task<bool> UpdateAsync(Especialidad especialidad)
        {
            var existing = _especialidades.FirstOrDefault(e => e.ID == especialidad.ID);
            if (existing != null)
            {
                existing.SetDescripcion(especialidad.Descripcion);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}