using backend.DTOs;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
        {
            var response = await _employeesService.GetAccounts();
            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
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
