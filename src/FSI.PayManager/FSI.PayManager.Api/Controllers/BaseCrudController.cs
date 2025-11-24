using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FSI.PayManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController<TDto> : ControllerBase
        where TDto : class, IHasId
    {
        private readonly ICrudAppService<TDto> _service;

        protected BaseCrudController(ICrudAppService<TDto> service)
        {
            _service = service;
        }

        /// <summary>GET lista todos os registros</summary>
        [HttpGet]
        public virtual async Task<ActionResult<List<TDto>>> GetAll(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            return Ok(result);
        }

        /// <summary>GET por Id</summary>
        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<TDto>> GetById(int id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>POST - cria novo registro</summary>
        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Post([FromBody] TDto dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>PUT - atualiza registro existente</summary>
        [HttpPut("{id:int}")]
        public virtual async Task<ActionResult<TDto>> Put(int id, [FromBody] TDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
                return BadRequest("Route id and payload id must match.");

            var updated = await _service.UpdateAsync(id, dto, ct);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        /// <summary>DELETE - apaga registro</summary>
        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
