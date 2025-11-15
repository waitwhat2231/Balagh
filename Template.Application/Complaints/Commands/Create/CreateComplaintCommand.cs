using Microsoft.AspNetCore.Http;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;

namespace Template.Application.Complaints.Commands.Create;

public class CreateComplaintCommand : ICommand<ComplaintDto>
{
    public int GovernmentalEntityId { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<IFormFile> ComplaintFiles { get; set; } = [];
}
