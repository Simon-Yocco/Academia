using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;

namespace Data
{
    public interface IAlumnoInscripcionRepository
    {
        Task AddAsync(AlumnoInscripcion inscripcion);
        Task<bool> UpdateAsync(AlumnoInscripcion inscripcion);
        Task<bool> DeleteAsync(int id);
        Task<AlumnoInscripcion?> GetAsync(int id);
        Task<IEnumerable<AlumnoInscripcion>> GetAllAsync();
        Task<IEnumerable<AlumnoInscripcion>> GetByCursoAsync(int idCurso);
        Task<IEnumerable<AlumnoInscripcion>> GetByAlumnoAsync(int idAlumno);
        Task<IEnumerable<AlumnoInscripcion>> GetByDocenteAsync(int idDocente);
        Task<int> CountInscriptosByCursoAsync(int idCurso);
        Task<bool> ExisteInscripcionAsync(int idAlumno, int idCurso);
        Task<IEnumerable<AlumnoInscripcion>> GetByCriteriaAsync(string texto);
    }
}
