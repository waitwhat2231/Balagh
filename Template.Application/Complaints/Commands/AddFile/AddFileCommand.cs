using Microsoft.AspNetCore.Http;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Complaints.Commands.AddFile;

public class AddFileCommand : ICommand
{
    public int ComplaintId { get; set; }
    public IFormFile NewFile { get; set; } = default!;
}
