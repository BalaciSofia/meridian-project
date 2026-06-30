using Azure.Core;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesService _employeesService;

        public EmployeesController (EmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<Account>>> GetEmployees()
        {
            var response = await _employeesService.GetAccounts();
            return response;
        }

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
