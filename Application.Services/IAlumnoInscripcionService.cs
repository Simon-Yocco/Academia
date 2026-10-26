using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;

namespace Application.Services
{
    public interface IAlumnoInscripcionService
    {
        Task<AlumnoInscripcionDTO?> AddAsync(AlumnoInscripcionDTO dto);
        Task<bool> UpdateAsync(AlumnoInscripcionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<AlumnoInscripcionDTO?> GetByIdAsync(int id);
        Task<IEnumerable<AlumnoInscripcionDTO>> GetAllAsync();
        Task<IEnumerable<AlumnoInscripcionDTO>> GetByCursoAsync(int idCurso);
        Task<IEnumerable<AlumnoInscripcionDTO>> GetByAlumnoAsync(int idAlumno);
        Task<IEnumerable<AlumnoInscripcionDTO>> GetByDocenteAsync(int idDocente);
        Task<IEnumerable<AlumnoInscripcionDTO>> GetByCriteriaAsync(string criteria);
    }
}
