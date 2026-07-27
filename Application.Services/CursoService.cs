using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repository;

        public CursoService(ICursoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CursoDTO>> GetAllAsync()
        {
            var cursos = await _repository.GetAllAsync();

            // Mapeo de Curso (Entidad) a CursoDTO
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

        public async Task<CursoDTO?> GetByIdAsync(int id)
        {
            var curso = await _repository.GetAsync(id);
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

        public async Task AddAsync(CursoDTO dto)
        {
            // Mapeamos de DTO a Entidad.
            var curso = new Curso(0, dto.AnioCalendario, dto.Cupo, dto.Descripcion, dto.IDcomision, dto.IDmateria);
            await _repository.AddAsync(curso);
        }

        public async Task<bool> UpdateAsync(int id, CursoDTO dto)
        {
            var curso = new Curso(id, dto.AnioCalendario, dto.Cupo, dto.Descripcion, dto.IDcomision, dto.IDmateria);
            return await _repository.UpdateAsync(curso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}