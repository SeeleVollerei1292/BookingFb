using bookingfootball.Db_QL;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingfootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaController : ControllerBase
    {
        private readonly ICaRepository _caRepository;

        public CaController(ICaRepository caRepository)
        {
            _caRepository = caRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCas()
        {
            try
            {
                var cas = await _caRepository.GetCasAsync();
                return Ok(cas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCaById(int id)
        {
            try
            {
                var ca = await _caRepository.GetCaByIdAsync(id);
                if (ca == null)
                {
                    return NotFound($"Ca with ID {id} not found.");
                }
                return Ok(ca);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCa([FromBody] Ca ca)
        {
            if (ca == null)
            {
                return BadRequest("Ca data is null.");
            }
            try
            {
                await _caRepository.CreateCaAsync(ca);
                return CreatedAtAction(nameof(GetCaById), new { id = ca.Id }, ca);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating Ca: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCa(int id, [FromBody] Ca ca)
        {
            if (ca == null || id != ca.Id)
            {
                return BadRequest("Ca data is invalid.");
            }
            try
            {
                await _caRepository.UpdateCaAsync(id, ca);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating Ca: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCa(int id)
        {
            try
            {
                await _caRepository.DeleteCaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting Ca: {ex.Message}");
            }
        }
    }
}
