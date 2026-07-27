using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        // Inyectamos el servicio
        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> GetAll()
        {
            var cursos = await _cursoService.GetAllAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> GetById(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CursoDTO cursoDto)
        {
            await _cursoService.AddAsync(cursoDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CursoDTO cursoDto)
        {
            var result = await _cursoService.UpdateAsync(id, cursoDto);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _cursoService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}