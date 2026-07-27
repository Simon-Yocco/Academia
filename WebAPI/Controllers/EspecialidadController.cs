using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadDTO>>> GetAll()
        {
            var especialidades = await _especialidadService.GetAllAsync();
            return Ok(especialidades);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadDTO>> GetById(int id)
        {
            var especialidad = await _especialidadService.GetByIdAsync(id);
            if (especialidad == null)
                return NotFound();

            return Ok(especialidad);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EspecialidadDTO dto)
        {
            await _especialidadService.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] EspecialidadDTO dto)
        {
            var result = await _especialidadService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _especialidadService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}