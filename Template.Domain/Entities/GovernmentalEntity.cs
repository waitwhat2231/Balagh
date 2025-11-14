namespace Template.Domain.Entities;

public class GovernmentalEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<User> Employees { get; set; } = [];
    public List<Complaint> Complaints { get; set; } = [];
    public bool IsDeleted { get; set; }
}
