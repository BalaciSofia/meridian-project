using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(response);
        }

        [HttpPost("new-employee")]
        public async Task<IActionResult> Register(CreateAccountRequest request)
        {
            await _authService.AddAccountAsync(request);
            return Created();
        }
    }
}
