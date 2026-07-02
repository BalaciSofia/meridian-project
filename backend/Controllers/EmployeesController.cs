using backend.DTOs;
using backend.Mapping;
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
        private readonly AccountMapper _accountMapper;

        public EmployeesController(IEmployeesService employeesService, AccountMapper accountMapper)
        {
            _employeesService = employeesService;
            _accountMapper = accountMapper;
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
        {
            var accounts = await _employeesService.GetAccounts();
            var response = _accountMapper.ToEmployeeResponses(accounts);

            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
        {
            var account = await _employeesService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            var response = _accountMapper.ToEmployeeResponse(account);
            return Ok(response);
        }
    }
}
