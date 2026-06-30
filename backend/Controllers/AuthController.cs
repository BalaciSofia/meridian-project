using backend.DTOs;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
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

        [Authorize(Roles = "HR,Admin")]
        [HttpPost("new-employee")]
        public async Task<IActionResult> Register(CreateAccountRequest request)
        {
            await _authService.AddAccountAsync(request);
            return Created();
        }
    }
}
