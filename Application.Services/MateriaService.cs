using DTOs;
using Data;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            this.materiaRepository = materiaRepository;
        }

        public async Task<IEnumerable<MateriaDTO>> GetAllAsync()
        {
            var list = await materiaRepository.GetAllAsync();
            return list.Select(x => new MateriaDTO
            {
                ID = x.ID,
                Descripcion = x.Descripcion,
                HSSemanales = x.HSSemanales,
                HSTotales = x.HSTotales,
                IDPlan = x.IDPlan,
                PlanDescripcion = x.Plan?.Descripcion ?? "Sin Plan"
            }).ToList();
        }

        public async Task<MateriaDTO?> GetByIdAsync(int id)
        {
            var materia = await materiaRepository.GetAsync(id);
            if (materia == null) return null;

            return new MateriaDTO
            {
                ID = materia.ID,
                Descripcion = materia.Descripcion,
                HSSemanales = materia.HSSemanales,
                HSTotales = materia.HSTotales,
                IDPlan = materia.IDPlan,
                PlanDescripcion = materia.Plan?.Descripcion ?? "Sin Plan"
            };
        }

        public async Task<IEnumerable<MateriaDTO>> GetByCriteriaAsync(string texto)
        {
            var list = await materiaRepository.GetByCriteriaAsync(texto);
            return list.Select(x => new MateriaDTO
            {
                ID = x.ID,
                Descripcion = x.Descripcion,
                HSSemanales = x.HSSemanales,
                HSTotales = x.HSTotales,
                IDPlan = x.IDPlan,
                PlanDescripcion = x.Plan?.Descripcion ?? "Sin Plan"
            }).ToList();
        }

        public async Task<MateriaDTO?> AddAsync(MateriaDTO dto)
        {
            var materia = new Materia(0, dto.Descripcion, dto.HSSemanales, dto.HSTotales, dto.IDPlan);
            await materiaRepository.AddAsync(materia);
            dto.ID = materia.ID;
            return dto;
        }

        public async Task<bool> UpdateAsync(MateriaDTO dto)
        {
            var existing = await materiaRepository.GetAsync(dto.ID);
            if (existing == null) return false;

            var materia = new Materia(dto.ID, dto.Descripcion, dto.HSSemanales, dto.HSTotales, dto.IDPlan);
            return await materiaRepository.UpdateAsync(materia);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await materiaRepository.DeleteAsync(id);
        }
    }
}
