using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public class CursoRepository : ICursoRepository
    {
        private static readonly List<Curso> _cursos = new List<Curso>();
        private static int _nextId = 1;

        public Task AddAsync(Curso curso)
        {
            curso.ID = _nextId++;
            _cursos.Add(curso);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var curso = _cursos.FirstOrDefault(c => c.ID == id);
            if (curso != null)
            {
                _cursos.Remove(curso);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<Curso?> GetAsync(int id)
        {
            var curso = _cursos.FirstOrDefault(c => c.ID == id);
            return Task.FromResult(curso);
        }

        public Task<IEnumerable<Curso>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Curso>>(_cursos.ToList());
        }

        public Task<bool> UpdateAsync(Curso curso)
        {
            var existing = _cursos.FirstOrDefault(c => c.ID == curso.ID);
            if (existing != null)
            {
                existing.SetAnioCalendario(curso.AnioCalendario);
                existing.SetCupo(curso.Cupo);
                existing.SetDescripcion(curso.Descripcion);
                existing.SetIDcomision(curso.IDcomision);
                existing.SetIDmateria(curso.IDmateria);

                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}