using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AlumnoInscripcionRepository : IAlumnoInscripcionRepository
    {
        private readonly TPIContext _context;

        public AlumnoInscripcionRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AlumnoInscripcion inscripcion)
        {
            _context.AlumnosInscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inscripcion = await _context.AlumnosInscripciones.FindAsync(id);
            if (inscripcion != null)
            {
                _context.AlumnosInscripciones.Remove(inscripcion);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<AlumnoInscripcion?> GetAsync(int id)
        {
            return await _context.AlumnosInscripciones
                .Include(i => i.Alumno)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Materia)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Comision)
                .FirstOrDefaultAsync(i => i.ID == id);
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetAllAsync()
        {
            return await _context.AlumnosInscripciones
                .Include(i => i.Alumno)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Materia)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Comision)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetByCursoAsync(int idCurso)
        {
            return await _context.AlumnosInscripciones
                .Include(i => i.Alumno)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Materia)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Comision)
                .Where(i => i.IDCurso == idCurso)
                .OrderBy(i => i.Alumno!.Apellido)
                .ThenBy(i => i.Alumno!.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetByAlumnoAsync(int idAlumno)
        {
            return await _context.AlumnosInscripciones
                .Include(i => i.Alumno)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Materia)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Comision)
                .Where(i => i.IDAlumno == idAlumno)
                .ToListAsync();
        }

        public async Task<int> CountInscriptosByCursoAsync(int idCurso)
        {
            return await _context.AlumnosInscripciones
                .CountAsync(i => i.IDCurso == idCurso);
        }

        public async Task<bool> ExisteInscripcionAsync(int idAlumno, int idCurso)
        {
            return await _context.AlumnosInscripciones
                .AnyAsync(i => i.IDAlumno == idAlumno && i.IDCurso == idCurso);
        }

        public async Task<bool> UpdateAsync(AlumnoInscripcion inscripcion)
        {
            var existing = await _context.AlumnosInscripciones.FindAsync(inscripcion.ID);
            if (existing != null)
            {
                existing.SetIDAlumno(inscripcion.IDAlumno);
                existing.SetIDCurso(inscripcion.IDCurso);
                existing.SetCondicion(inscripcion.Condicion);
                existing.SetNota(inscripcion.Nota);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<AlumnoInscripcion>> GetByCriteriaAsync(string texto)
        {
            return await _context.AlumnosInscripciones
                .Include(i => i.Alumno)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Materia)
                .Include(i => i.Curso)
                    .ThenInclude(c => c!.Comision)
                .Where(i => (i.Alumno != null && (i.Alumno.Nombre.Contains(texto) || i.Alumno.Apellido.Contains(texto) || i.Alumno.Legajo.ToString().Contains(texto)))
                         || (i.Curso != null && i.Curso.Descripcion.Contains(texto))
                         || (i.Curso != null && i.Curso.Materia != null && i.Curso.Materia.Descripcion.Contains(texto))
                         || (i.Curso != null && i.Curso.Comision != null && i.Curso.Comision.Descripcion.Contains(texto))
                         || i.Condicion.Contains(texto))
                .ToListAsync();
        }
    }
}
