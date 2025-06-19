using bookingfootball.DTOs;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace bookingfootball.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaNhanVienController : ControllerBase
    {
        private readonly ICaNhanVienRepo _repo;

        public CaNhanVienController(ICaNhanVienRepo repo)
        {
            _repo = repo;
        }

        // GET: api/CaNhanVien
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result);
        }

        // GET: api/CaNhanVien/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        // POST: api/CaNhanVien
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CaNhanVienDTO dto)
        {
            if(dto == null) 
            {
                return BadRequest("DTO cannot null");
            }
            await _repo.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            
        }

        // PUT: api/CaNhanVien/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CaNhanVienDTO dto)
        {
            try
            {
                var updated = await _repo.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/CaNhanVien/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.GetByIdAsync(id);
            if (deleted == null)
                return NotFound();
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
