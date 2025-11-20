using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Complaints.Notes.Commands
{
    public class AddNoteCommand : ICommand
    {
        [JsonIgnore]
        [BindNever]
        public int ComplaintId { get; set; }

        public string NoteBody { get; set; } = string.Empty;
    }
}
