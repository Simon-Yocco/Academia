using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            this.cursoRepository = cursoRepository;
        }

        public async Task<IEnumerable<CursoDTO>> GetAllAsync()
        {
            var cursos = await cursoRepository.GetAllAsync();

            return cursos.Select(c => new CursoDTO
            {
                ID = c.ID,
                AnioCalendario = c.AnioCalendario,
                Cupo = c.Cupo,
                Descripcion = c.Descripcion,
                IDcomision = c.IDcomision,
                IDmateria = c.IDmateria,
                IDDocente = c.IDdocente,
                MateriaDescripcion = c.Materia?.Descripcion ?? "Sin Materia",
                ComisionDescripcion = c.Comision?.Descripcion ?? "Sin Comisión",
                DocenteNombreCompleto = c.Docente != null 
                    ? $"{c.Docente.Apellido}, {c.Docente.Nombre}" 
                    : "Sin Asignar"
            }).ToList();
        }

        public async Task<CursoDTO?> GetByIdAsync(int id)
        {
            var curso = await cursoRepository.GetAsync(id);
            if (curso == null) return null;

            return new CursoDTO
            {
                ID = curso.ID,
                AnioCalendario = curso.AnioCalendario,
                Cupo = curso.Cupo,
                Descripcion = curso.Descripcion,
                IDcomision = curso.IDcomision,
                IDmateria = curso.IDmateria,
                IDDocente = curso.IDdocente,
                MateriaDescripcion = curso.Materia?.Descripcion ?? "Sin Materia",
                ComisionDescripcion = curso.Comision?.Descripcion ?? "Sin Comisión",
                DocenteNombreCompleto = curso.Docente != null 
                    ? $"{curso.Docente.Apellido}, {curso.Docente.Nombre}" 
                    : "Sin Asignar"
            };
        }

        public async Task<CursoDTO?> AddAsync(CursoDTO dto)
        {
            // Mapeamos de DTO a Entidad.
            var curso = new Curso(0, dto.AnioCalendario, dto.Cupo, dto.Descripcion, dto.IDcomision, dto.IDmateria, dto.IDDocente);
            await cursoRepository.AddAsync(curso);
            
            // Actualizamos el ID del DTO con el que se generó en la base de datos
            dto.ID = curso.ID;
            
            return dto;
        }

        public async Task<bool> UpdateAsync(CursoDTO dto)
        {
            var existing = await cursoRepository.GetAsync(dto.ID);

            if (existing == null)
                return false;

            var curso = new Curso(dto.ID, dto.AnioCalendario, dto.Cupo, dto.Descripcion, dto.IDcomision, dto.IDmateria, dto.IDDocente);
            return await cursoRepository.UpdateAsync(curso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await cursoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CursoDTO>> GetByCriteriaAsync(string criteria)
        {
            var cursos = await cursoRepository.GetByCriteriaAsync(criteria);

            return cursos.Select(c => new CursoDTO
            {
                ID = c.ID,
                AnioCalendario = c.AnioCalendario,
                Cupo = c.Cupo,
                Descripcion = c.Descripcion,
                IDcomision = c.IDcomision,
                IDmateria = c.IDmateria,
                IDDocente = c.IDdocente,
                MateriaDescripcion = c.Materia?.Descripcion ?? "Sin Materia",
                ComisionDescripcion = c.Comision?.Descripcion ?? "Sin Comisión",
                DocenteNombreCompleto = c.Docente != null 
                    ? $"{c.Docente.Apellido}, {c.Docente.Nombre}" 
                    : "Sin Asignar"
            }).ToList();
        }

        public async Task<IEnumerable<CursoDTO>> GetByCriteria(string texto)
        {
            return await GetByCriteriaAsync(texto);
        }
    }
}
