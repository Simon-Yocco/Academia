using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IEspecialidadRepository especialidadRepository;

        public EspecialidadService(IEspecialidadRepository especialidadRepository)
        {
            this.especialidadRepository = especialidadRepository;
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            var especialidades = await especialidadRepository.GetAllAsync();
            return especialidades.Select(e => new EspecialidadDTO
            {
                ID = e.ID,
                Descripcion = e.Descripcion
            }).ToList();
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetByCriteriaAsync(string texto)
        {
            var especialidades = await especialidadRepository.GetByCriteriaAsync(texto);

            return especialidades.Select(e => new EspecialidadDTO
            {
                ID = e.ID,
                Descripcion = e.Descripcion
            }).ToList();
        }

        public async Task<EspecialidadDTO?> GetByIdAsync(int id)
        {
            var e = await especialidadRepository.GetAsync(id);
            if (e == null) return null;

            return new EspecialidadDTO
            {
                ID = e.ID,
                Descripcion = e.Descripcion
            };
        }

        public async Task<EspecialidadDTO> AddAsync(EspecialidadDTO dto)
        {
            // Mapeamos de DTO a Entidad.
            var especialidad = new Especialidad(0, dto.Descripcion);
            await especialidadRepository.AddAsync(especialidad);

            // Actualizamos el ID del DTO con el que se generó en la base de datos
            dto.ID = especialidad.ID;

            return dto;
        }

        public async Task<bool> UpdateAsync(EspecialidadDTO dto)
        {
            var especialidad = new Especialidad(dto.ID, dto.Descripcion);
            return await especialidadRepository.UpdateAsync(especialidad);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await especialidadRepository.DeleteAsync(id);
        }
    }
}