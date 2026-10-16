using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IEspecialidadRepository _especialidadRepository;

        public PlanService(IPlanRepository planRepository, IEspecialidadRepository especialidadRepository)
        {
            _planRepository = planRepository;
            _especialidadRepository = especialidadRepository;
        }

        public async Task<PlanDTO> AddAsync(PlanDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                throw new ArgumentException("La descripción del plan es requerida.");
            }

            if (dto.Materias == null || !dto.Materias.Any())
            {
                throw new ArgumentException("El plan debe contener al menos una materia.");
            }

            var especialidad = await _especialidadRepository.GetAsync(dto.IDEspecialidad);
            if (especialidad == null)
            {
                throw new ArgumentException($"La especialidad con ID {dto.IDEspecialidad} no existe.");
            }

            var plan = new Plan(0, dto.Descripcion, dto.IDEspecialidad);

            foreach (var materiaDto in dto.Materias)
            {
                var materia = new Materia(0, materiaDto.Descripcion, materiaDto.HSSemanales, materiaDto.HSTotales, 0);
                plan.AgregarMateria(materia);
            }

            await _planRepository.AddAsync(plan);

            dto.ID = plan.ID;
            dto.EspecialidadDescripcion = especialidad.Descripcion;
            return dto;
        }

        public async Task<IEnumerable<PlanDTO>> GetAllAsync()
        {
            var planes = await _planRepository.GetAllAsync();
            return planes.Select(p => new PlanDTO
            {
                ID = p.ID,
                Descripcion = p.Descripcion,
                IDEspecialidad = p.IDEspecialidad,
                EspecialidadDescripcion = p.Especialidad?.Descripcion ?? string.Empty,
                // Mapear también los detalles
                Materias = p.Materias.Select(m => new MateriaDTO
                {
                    ID = m.ID,
                    Descripcion = m.Descripcion,
                    HSSemanales = m.HSSemanales,
                    HSTotales = m.HSTotales,
                    IDPlan = m.IDPlan
                }).ToList()
            }).ToList();
        }

        public async Task<IEnumerable<PlanDTO>> GetByCriteriaAsync(string texto)
        {
            var planes = await _planRepository.GetByCriteriaAsync(texto);
            return planes.Select(p => new PlanDTO
            {
                ID = p.ID,
                Descripcion = p.Descripcion,
                IDEspecialidad = p.IDEspecialidad,
                EspecialidadDescripcion = p.Especialidad?.Descripcion ?? string.Empty,
                Materias = p.Materias.Select(m => new MateriaDTO
                {
                    ID = m.ID,
                    Descripcion = m.Descripcion,
                    HSSemanales = m.HSSemanales,
                    HSTotales = m.HSTotales,
                    IDPlan = m.IDPlan
                }).ToList()
            }).ToList();
        }

        public async Task<PlanDTO?> GetByIdAsync(int id)
        {
            var plan = await _planRepository.GetAsync(id);
            if (plan == null) return null;

            return new PlanDTO
            {
                ID = plan.ID,
                Descripcion = plan.Descripcion,
                IDEspecialidad = plan.IDEspecialidad,
                EspecialidadDescripcion = plan.Especialidad?.Descripcion ?? string.Empty,
                Materias = plan.Materias.Select(m => new MateriaDTO
                {
                    ID = m.ID,
                    Descripcion = m.Descripcion,
                    HSSemanales = m.HSSemanales,
                    HSTotales = m.HSTotales,
                    IDPlan = m.IDPlan
                }).ToList()
            };
        }

        public async Task<bool> UpdateAsync(PlanDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Descripcion))
            {
                throw new ArgumentException("La descripción del plan es requerida.");
            }

            if (dto.Materias == null || !dto.Materias.Any())
            {
                throw new ArgumentException("El plan debe contener al menos una materia.");
            }

            var especialidad = await _especialidadRepository.GetAsync(dto.IDEspecialidad);
            if (especialidad == null)
            {
                throw new ArgumentException($"La especialidad con ID {dto.IDEspecialidad} no existe.");
            }

            // Convertimos de DTO a Entidad para pasárselo al Repositorio
            var plan = new Plan(dto.ID, dto.Descripcion, dto.IDEspecialidad);

            foreach (var mDto in dto.Materias)
            {
                var materia = new Materia(mDto.ID, mDto.Descripcion, mDto.HSSemanales, mDto.HSTotales, dto.ID);
                plan.AgregarMateria(materia);
            }

            return await _planRepository.UpdateAsync(plan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _planRepository.DeleteAsync(id);
        }
    }
}
