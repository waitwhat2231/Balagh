using Microsoft.AspNetCore.Identity;

namespace Template.Domain.Entities;

public class User : IdentityUser
{
    public int? GovernmentalEntityId { get; set; }
    public GovernmentalEntity? GovernmentalEntity { get; set; }
    public List<Complaint> Complaints { get; set; } = [];
    public List<Device> Devices { get; set; } = [];
    public List<Note> Notes { get; set; } = [];
    public List<History> Histories { get; set; } = [];
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ForgotPasswordToken { get; set; } = string.Empty;
    public OTP Otp { get; set; } = default!;
}
