using Duong_API.Data;
using Duong_API.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Duong_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoThueController : ControllerBase
    {
        private readonly IDoThueRepository _doThueRepository;
        public DoThueController(IDoThueRepository doThueRepository)
        {
            _doThueRepository = doThueRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doThues = await _doThueRepository.GetAllAsync();
            return Ok(doThues);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doThue = await _doThueRepository.GetByIdAsync(id);
            if (doThue == null)
            {
                return NotFound();
            }
            return Ok(doThue);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoThue doThue)
        {
            if (doThue == null)
            {
                return BadRequest("DoThue object is null");
            }
            await _doThueRepository.CreateAsync(doThue);
            return CreatedAtAction(nameof(GetById), new { id = doThue.Id }, doThue);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoThue doThue)
        {
            if (doThue == null || id != doThue.Id)
            {
                return BadRequest("DoThue object is null or ID mismatch");
            }
            var existingDoThue = await _doThueRepository.GetByIdAsync(id);
            if (existingDoThue == null)
            {
                return NotFound();
            }
            await _doThueRepository.UpdateAsync(id, doThue);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doThue = await _doThueRepository.GetByIdAsync(id);
            if (doThue == null)
            {
                return NotFound();
            }
            await _doThueRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
