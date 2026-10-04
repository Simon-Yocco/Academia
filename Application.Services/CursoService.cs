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
            this.cursoRepository= cursoRepository;
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
                MateriaDescripcion = c.Materia?.Descripcion ?? "Sin Materia",
                ComisionDescripcion = c.Comision?.Descripcion ?? "Sin Comisión"
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
                IDmateria = curso.IDmateria
            };
        }

        public async Task<CursoDTO?> AddAsync(CursoDTO dto)
        {
            // Mapeamos de DTO a Entidad.
            var curso = new Curso(0, dto.AnioCalendario, dto.Cupo, dto.Descripcion, dto.IDcomision, dto.IDmateria);
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

            var curso = new Curso(dto.ID, dto.AnioCalendario, dto.Cupo, dto.Descripcion, dto.IDcomision, dto.IDmateria);
            return await cursoRepository.UpdateAsync(curso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await cursoRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<CursoDTO>> GetByCriteriaAsync(string criteria)
        {
            // Llamar al repositorio
            var cursos = await cursoRepository.GetByCriteriaAsync(criteria);

            // Mapear Domain Model a DTO
            return cursos.Select(c => new CursoDTO
            {
                ID = c.ID,
                AnioCalendario = c.AnioCalendario,
                Cupo = c.Cupo,
                Descripcion = c.Descripcion,
                IDcomision = c.IDcomision,
                IDmateria = c.IDmateria
            });
        }
        public async Task<IEnumerable<CursoDTO>> GetByCriteria(string texto)
        {
            var cursos = await cursoRepository.GetByCriteriaAsync(texto);
            return cursos.Select(c => new CursoDTO
            {
                ID = c.ID,
                AnioCalendario = c.AnioCalendario,
                Cupo = c.Cupo,
                Descripcion = c.Descripcion,
                IDcomision = c.IDcomision,
                IDmateria = c.IDmateria,
                MateriaDescripcion = c.Materia?.Descripcion ?? "Sin Materia",
                ComisionDescripcion = c.Comision?.Descripcion ?? "Sin Comisión"
            }).ToList();
        }
    }
}