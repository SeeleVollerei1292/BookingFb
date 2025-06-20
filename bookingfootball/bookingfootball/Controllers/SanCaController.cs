using bookingfootball.Db_QL;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingfootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanCaController : ControllerBase
    {
        private readonly ISancaRepository _sancaRepository;
        public SanCaController(ISancaRepository sancaRepository)
        {
            _sancaRepository = sancaRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetSanCas()
        {
            try
            {
                var sanCas = await _sancaRepository.GetSanCasAsync();
                return Ok(sanCas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSanCaById(int id)
        {
            try
            {
                var sanCa = await _sancaRepository.GetSanCaByIdAsync(id);
                if (sanCa == null)
                {
                    return NotFound($"SanCa with ID {id} not found.");
                }
                return Ok(sanCa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateSanCa([FromBody] SanCa sanCa)
        {
            if (sanCa == null)
            {
                return BadRequest("SanCa data is null.");
            }
            try
            {
                await _sancaRepository.CreateSanCaAsync(sanCa);
                return CreatedAtAction(nameof(GetSanCaById), new { id = sanCa.Id }, sanCa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating SanCa: {ex.Message}");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSanCa(int id, [FromBody] SanCa sanCa)
        {
            if (sanCa == null || id != sanCa.Id)
            {
                return BadRequest("SanCa data is invalid.");
            }
            try
            {
                await _sancaRepository.UpdateSanCaAsync(id, sanCa);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating SanCa: {ex.Message}");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSanCa(int id)
        {
            try
            {
                await _sancaRepository.DeleteSanCaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting SanCa: {ex.Message}");
            }
        }
    }
}
