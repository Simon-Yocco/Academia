using DTOs;
using Data;

namespace Application.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository materiaRepository;
        public MateriaService(IMateriaRepository materiaRepository) {
            this.materiaRepository = materiaRepository;
        }
        public async Task<IEnumerable<MateriaDTO>> GetAllAsync()
        {
            var list = await materiaRepository.GetAllAsync();
            return list.Select(x => new MateriaDTO { ID = x.ID, Descripcion = x.Descripcion, HSSemanales = x.HSSemanales, HSTotales = x.HSTotales, IDPlan = x.IDPlan }).ToList();
        }
    }
}
