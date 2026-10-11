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

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanDTO> AddAsync(PlanDTO dto)
        {
            var plan = new Plan(0, dto.Descripcion, dto.IDEspecialidad);

            foreach (var materiaDto in dto.Materias)
            {
                var materia = new Materia(0, materiaDto.Descripcion, materiaDto.HSSemanales, materiaDto.HSTotales, 0);
                plan.AgregarMateria(materia);
            }

            await _planRepository.AddAsync(plan);

            dto.ID = plan.ID;
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

        public async Task<PlanDTO?> GetByIdAsync(int id)
        {
            var plan = await _planRepository.GetAsync(id);
            if (plan == null) return null;

            return new PlanDTO
            {
                ID = plan.ID,
                Descripcion = plan.Descripcion,
                IDEspecialidad = plan.IDEspecialidad,
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
