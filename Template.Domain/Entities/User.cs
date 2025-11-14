using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities.Notifications;

namespace Template.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<Device> Devices { get; set; } = [];
    }
}
