using Azure.Core;
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

        EmployeesController (EmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var response = await _employeesService.GetAccounts();
            return NoContent();
        }
    }
}
