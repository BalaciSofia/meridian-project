using backend.DTOs;
using backend.Mapping;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AccountMapper _accountMapper;

        public AuthController(IAuthService authService, AccountMapper accountMapper)
        {
            _authService = authService;
            _accountMapper = accountMapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request.Email, request.Password);

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
            var account = _accountMapper.ToEntity(request);

            await _authService.AddAccountAsync(account);
            return Created();
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var accountId = GetCurrentAccountId();
            var passwordChanged = await _authService.ChangePasswordAsync(
                accountId,
                request.CurrentPassword,
                request.NewPassword);

            if (!passwordChanged)
            {
                return BadRequest("Current password is incorrect.");
            }

            return NoContent();
        }

        private int GetCurrentAccountId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
