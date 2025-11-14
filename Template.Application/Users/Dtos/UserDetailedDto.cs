using Template.Domain.Entities.Notifications;

namespace Template.Application.Users.Dtos
{
    public class UserDetailedDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public List<Device>? Devices { get; set; }
    }
}
