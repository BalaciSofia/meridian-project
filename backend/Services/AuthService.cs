using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Account> _passwordHasher = new();
        public AuthService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var account = await _accountRepository.GetByEmailWithRoleAndDepartmentAsync(request.Email);
            
            if (account == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                account,
                account.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = GenerateJwtToken(account);

            return new LoginResponse
            {
                Token = token,
                AccountId = account.Id,
                Email = account.Email,
                Role = account.Role.RoleName,
                Department = account.Department.DepartmentName,
                MustChangePassword = account.MustChangePassword
            };
        }

        private string GenerateJwtToken(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.RoleName),
                new Claim("department", account.Department.DepartmentName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
