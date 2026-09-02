using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CursoRepository : ICursoRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Curso curso)
        {
            using var context = CreateContext();
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var curso = await context.Cursos.FindAsync(id);
            if (curso != null)
            {
                context.Cursos.Remove(curso);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Curso?> GetAsync(int id)
        {
            using var context = CreateContext();
            var curso = await context.Cursos.FindAsync(id);
            return curso;
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            using var context = CreateContext();
            var cursos = await context.Cursos.ToListAsync();
            return cursos;
        }

        public async Task<bool> UpdateAsync(Curso curso)
        {
            using var context = CreateContext();
            var existingCurso = await context.Cursos.FindAsync(curso.ID);
            if (existingCurso != null)
            {
                existingCurso.SetAnioCalendario(curso.AnioCalendario);
                existingCurso.SetCupo(curso.Cupo);
                existingCurso.SetDescripcion(curso.Descripcion);
                existingCurso.SetIDcomision(curso.IDcomision);
                existingCurso.SetIDmateria(curso.IDmateria);

                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}