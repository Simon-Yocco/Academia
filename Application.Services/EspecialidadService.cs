using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IEspecialidadRepository _repository;

        public EspecialidadService(IEspecialidadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EspecialidadDTO>> GetAllAsync()
        {
            var especialidades = await _repository.GetAllAsync();
            return especialidades.Select(e => new EspecialidadDTO
            {
                ID = e.ID,
                Descripcion = e.Descripcion
            }).ToList();
        }

        public async Task<EspecialidadDTO?> GetByIdAsync(int id)
        {
            var e = await _repository.GetAsync(id);
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
            await _repository.AddAsync(especialidad);

            // Actualizamos el ID del DTO con el que se generó en la base de datos
            dto.ID = especialidad.ID;

            return dto;
        }

        public async Task<bool> UpdateAsync(EspecialidadDTO dto)
        {
            var especialidad = new Especialidad(dto.ID, dto.Descripcion);
            return await _repository.UpdateAsync(especialidad);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}