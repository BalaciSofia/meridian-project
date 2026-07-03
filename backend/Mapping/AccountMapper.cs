using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class AccountMapper
    {
        [MapProperty(nameof(CreateAccountRequest.Password), nameof(Account.PasswordHash))]
        [MapperIgnoreTarget(nameof(Account.Id))]
        [MapperIgnoreTarget(nameof(Account.Role))]
        [MapperIgnoreTarget(nameof(Account.Department))]
        public partial Account ToEntity(CreateAccountRequest request);

        [MapProperty("Role.RoleName", nameof(EmployeeResponse.Role))]
        [MapProperty("Department.DepartmentName", nameof(EmployeeResponse.Department))]
        [MapperIgnoreSource(nameof(Account.PasswordHash))]
        [MapperIgnoreSource(nameof(Account.RoleId))]
        [MapperIgnoreSource(nameof(Account.DepartmentId))]
        public partial EmployeeResponse ToEmployeeResponse(Account account);

        public partial IEnumerable<EmployeeResponse> ToEmployeeResponses(IEnumerable<Account> accounts);

        [MapProperty("Role.RoleName", nameof(CurrentAccountResponse.Role))]
        [MapProperty("Department.DepartmentName", nameof(CurrentAccountResponse.Department))]
        [MapProperty("Department.SlackLink", nameof(CurrentAccountResponse.DepartmentSlackLink))]
        [MapperIgnoreSource(nameof(Account.PasswordHash))]
        [MapperIgnoreSource(nameof(Account.RoleId))]
        [MapperIgnoreSource(nameof(Account.DepartmentId))]
        public partial CurrentAccountResponse ToCurrentAccountResponse(Account account);
    }
}
