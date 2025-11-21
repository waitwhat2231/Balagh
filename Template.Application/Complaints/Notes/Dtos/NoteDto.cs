namespace Template.Application.Complaints.Notes.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int ComplaintId { get; set; }
        public string NoteBody { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
