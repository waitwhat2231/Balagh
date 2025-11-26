using Template.Application.Abstraction.Commands;

namespace Template.Application.Complaints.Commands.DeleteFile;

public class DeleteFileCommand : ICommand
{
    public int ComplaintId { get; set; }
    public int FileId { get; set; }
}
