using DTOs;
using Data;

namespace Application.Services
{
    public class ComisionService : IComisionService
    {
        private readonly IComisionRepository comisionRepository;
        public ComisionService(IComisionRepository comisionRepository) {
            this.comisionRepository = comisionRepository;
        }
        public async Task<IEnumerable<ComisionDTO>> GetAllAsync()
        {
            var list = await comisionRepository.GetAllAsync();
            return list.Select(x => new ComisionDTO { ID = x.ID, Descripcion = x.Descripcion, AnioEspecialidad = x.AnioEspecialidad, IDPlan = x.IDPlan }).ToList();
        }
    }
}
