using Azure.Core;
using backend.Models;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController (IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<Account>>> GetEmployees()
        {
            var response = await _employeesService.GetAccounts();
            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response = await _employeesService.GetAccountById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
