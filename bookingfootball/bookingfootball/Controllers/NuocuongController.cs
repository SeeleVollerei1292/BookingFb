using bookingfootball.Db_QL;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingfootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NuocuongController : ControllerBase
    {
        private readonly INuocuongRepository _nuocuongRepository;
        public NuocuongController(INuocuongRepository nuocuongRepository)
        {
            _nuocuongRepository = nuocuongRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNuocUong()
        {
            var nuocUongs = await _nuocuongRepository.GetAllNuocUongAsync();
            return Ok(nuocUongs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNuocUongById(int id)
        {
            var nuocUong = await _nuocuongRepository.GetNuocUongById(id);
            if (nuocUong == null)
            {
                return NotFound();
            }
            return Ok(nuocUong);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNuocUong([FromBody] NuocUong nuocUong)
        {
            if (nuocUong == null)
            {
                return BadRequest("NuocUong cannot be null");
            }
            await _nuocuongRepository.CreateNuocUong(nuocUong);
            return CreatedAtAction(nameof(GetNuocUongById), new { id = nuocUong.Id }, nuocUong);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNuocUong(int id, [FromBody] NuocUong nuocUong)
        {
            if (nuocUong == null || nuocUong.Id != id)
            {
                return BadRequest("NuocUong data is invalid");
            }
            var existingNuocUong = await _nuocuongRepository.GetNuocUongById(id);
            if (existingNuocUong == null)
            {
                return NotFound();
            }
            await _nuocuongRepository.UpdateNuocUong(id, nuocUong);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableNuocUong(int id)
        {
            var existingNuocUong = await _nuocuongRepository.GetNuocUongById(id);
            if (existingNuocUong == null)
            {
                return NotFound();
            }
            await _nuocuongRepository.DisableNuocUong(id);
            return NoContent();
        }
    }
}
//s