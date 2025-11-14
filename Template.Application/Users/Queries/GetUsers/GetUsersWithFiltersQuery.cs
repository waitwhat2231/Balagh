using Template.Application.Abstraction.Queries;
using Template.Application.Users.Dtos;

namespace Template.Application.Users.Queries.GetUsers
{
    public class GetUsersWithFiltersQuery(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName) : IQuery<List<UserDto>>
    {
        public string? Role { get; set; } = role;
        public string? Email { get; set; } = email;
        public string? PhoneNumber { get; set; } = phoneNumber;
        public string? ClinicAddress { get; set; } = clinicAddress;
        public string? ClinicName { get; set; } = clinicName;
    }
}
