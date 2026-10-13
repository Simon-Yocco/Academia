using DTOs;
using Data;
using Entidades;

namespace Application.Services
{
    public class ComisionService : IComisionService
    {
        private readonly IComisionRepository comisionRepository;

        public ComisionService(IComisionRepository comisionRepository)
        {
            this.comisionRepository = comisionRepository;
        }

        public async Task<IEnumerable<ComisionDTO>> GetAllAsync()
        {
            var list = await comisionRepository.GetAllAsync();
            return list.Select(x => new ComisionDTO
            {
                ID = x.ID,
                Descripcion = x.Descripcion,
                AnioEspecialidad = x.AnioEspecialidad,
                IDPlan = x.IDPlan,
                PlanDescripcion = x.Plan?.Descripcion ?? "Sin Plan"
            }).ToList();
        }

        public async Task<ComisionDTO?> GetByIdAsync(int id)
        {
            var comision = await comisionRepository.GetAsync(id);
            if (comision == null) return null;

            return new ComisionDTO
            {
                ID = comision.ID,
                Descripcion = comision.Descripcion,
                AnioEspecialidad = comision.AnioEspecialidad,
                IDPlan = comision.IDPlan,
                PlanDescripcion = comision.Plan?.Descripcion ?? "Sin Plan"
            };
        }

        public async Task<ComisionDTO?> AddAsync(ComisionDTO dto)
        {
            var comision = new Comision(0, dto.AnioEspecialidad, dto.Descripcion, dto.IDPlan);
            await comisionRepository.AddAsync(comision);
            dto.ID = comision.ID;
            return dto;
        }

        public async Task<bool> UpdateAsync(ComisionDTO dto)
        {
            var existing = await comisionRepository.GetAsync(dto.ID);
            if (existing == null) return false;

            var comision = new Comision(dto.ID, dto.AnioEspecialidad, dto.Descripcion, dto.IDPlan);
            return await comisionRepository.UpdateAsync(comision);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await comisionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ComisionDTO>> GetByCriteriaAsync(string texto)
        {
            var comisiones = await comisionRepository.GetByCriteriaAsync(texto);
            return comisiones.Select(x => new ComisionDTO
            {
                ID = x.ID,
                Descripcion = x.Descripcion,
                AnioEspecialidad = x.AnioEspecialidad,
                IDPlan = x.IDPlan,
                PlanDescripcion = x.Plan?.Descripcion ?? "Sin Plan"
            }).ToList();
        }
    }
}
