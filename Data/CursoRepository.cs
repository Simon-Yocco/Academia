using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data
{
    public class CursoRepository : ICursoRepository
    {
        private readonly TPIContext _context;

        public CursoRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Curso?> GetAsync(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Materia)
                .Include(c => c.Comision)
                .Include(c => c.Docente)
                .FirstOrDefaultAsync(c => c.ID == id);
            return curso;
        }

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Materia)
                .Include(c => c.Comision)
                .Include(c => c.Docente)
                .OrderByDescending(c => c.AnioCalendario)
                .ToListAsync();
            return cursos;
        }

        public async Task<bool> UpdateAsync(Curso curso)
        {
            var existingCurso = await _context.Cursos.FindAsync(curso.ID);
            if (existingCurso != null)
            {
                existingCurso.SetAnioCalendario(curso.AnioCalendario);
                existingCurso.SetCupo(curso.Cupo);
                existingCurso.SetDescripcion(curso.Descripcion);
                existingCurso.SetIDcomision(curso.IDcomision);
                existingCurso.SetIDmateria(curso.IDmateria);
                existingCurso.SetIDdocente(curso.IDdocente);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Curso>> GetByCriteriaAsync(string texto)
        {
            return await _context.Cursos
                .Include(c => c.Materia)
                .Include(c => c.Comision)
                .Include(c => c.Docente)
                .Where(c => c.Descripcion.Contains(texto)
                         || (c.Materia != null && c.Materia.Descripcion.Contains(texto))
                         || (c.Comision != null && c.Comision.Descripcion.Contains(texto))
                         || (c.Docente != null && (c.Docente.Nombre.Contains(texto) || c.Docente.Apellido.Contains(texto))))
                .OrderByDescending(c => c.AnioCalendario)
                .ToListAsync();
        }
    }
}
