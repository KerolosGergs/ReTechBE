using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReTechApi.Models;
using ReTechApi.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReTechBE.DTO;
using ReTechBE.UserDTO;

namespace ReTechApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepository;

        public AuthController(IAuthRepo authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authRepository.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authRepository.LoginAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _authRepository.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
        [HttpPost("deduct-points")]
        public async Task<IActionResult> DeductPoints([FromBody] DeductPointsDto model)
        {
            var success = await _authRepository.DeductPointsAsync(model.UserId, model.PointsToDeduct);

            if (!success)
                return BadRequest("Failed to deduct points. Either user not found or insufficient points.");

            return Ok("Points deducted successfully.");
        }
    }
}