using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Complaints.Commands.AddFile;

public class AddFileCommand : ICommand
{
    public int ComplaintId { get; set; }
    public IFormFile NewFile { get; set; } = default!;
    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
