using backend.DTOs;
using backend.Mapping;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HR,Admin")]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupsService _lookupsService;
        private readonly LookupMapper _lookupMapper;

        public LookupsController(ILookupsService lookupsService, LookupMapper lookupMapper)
        {
            _lookupsService = lookupsService;
            _lookupMapper = lookupMapper;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetRoles()
        {
            var roles = await _lookupsService.GetAllRoles();
            var response = _lookupMapper.ToRoleResponses(roles);

            return Ok(response);
        }

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<DepartmentResponse>>> GetDepartments()
        {
            var departments = await _lookupsService.GetAllDepartments();
            var response = _lookupMapper.ToDepartmentResponses(departments);

            return Ok(response);
        }
    }
}
