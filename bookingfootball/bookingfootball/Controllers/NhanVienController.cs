using bookingfootball.Db_QL;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingfootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepository _nhanVienRepository;
        public NhanVienController(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAllNhanVien()
        {
            try
            {
                var nhanViens = await _nhanVienRepository.GetAllNhanVienAsync();
                return Ok(nhanViens);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNhanVienById(int id)
        {
            try
            {
                var nhanVien = await _nhanVienRepository.GetNhanVienById(id);
                if (nhanVien == null)
                {
                    return NotFound($"NhanVien with id {id} not found");
                }
                return Ok(nhanVien);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNhanVien([FromBody] NhanVien nv)
        {
            if (nv == null)

            {
                return BadRequest("NhanVien cannot be null");
            }
            try
            {
                await _nhanVienRepository.CreateNhanvien(nv);
                return CreatedAtAction(nameof(GetNhanVienById), new { id = nv.Id }, nv);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating NhanVien: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhanVien(int id, [FromBody] NhanVien nv)
        {
            if (nv == null || nv.Id != id)
            {
                return BadRequest("NhanVien data is invalid");
            }
            try
            {
                var existingNhanVien = await _nhanVienRepository.GetNhanVienById(id);
                if (existingNhanVien == null)
                {
                    return NotFound($"NhanVien with id {id} not found");
                }
                await _nhanVienRepository.UpdateNhanVien(id,nv);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating NhanVien: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            try
            {
                var existingNhanVien = await _nhanVienRepository.GetNhanVienById(id);
                if (existingNhanVien == null)
                {
                    return NotFound($"NhanVien with id {id} not found");
                }
                await _nhanVienRepository.DisableNhanVien(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting NhanVien: {ex.Message}");
            }
        }
    }
}
