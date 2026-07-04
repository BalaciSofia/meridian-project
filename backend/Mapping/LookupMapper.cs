using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class LookupMapper
    {
        public partial RoleResponse ToResponse(Role role);

        public partial IEnumerable<RoleResponse> ToRoleResponses(IEnumerable<Role> roles);

        public partial DepartmentResponse ToResponse(Department department);

        public partial IEnumerable<DepartmentResponse> ToDepartmentResponses(IEnumerable<Department> departments);
    }
}
