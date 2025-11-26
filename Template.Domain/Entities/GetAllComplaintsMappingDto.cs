namespace Template.Domain.Entities
{
    public class GetAllComplaintsMappingDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public int GovernmentalEntityId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ComplaintStatus Status { get; set; }
        public string LockedBy { get; set; } = string.Empty;
        public string LockedByUserName { get; set; } = string.Empty;
        public bool IsLocked { get; set; }
    }
}
