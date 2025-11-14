using Microsoft.AspNetCore.Http;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;
using Template.Domain;

namespace Template.Application.Complaints.Commands.Create;

public class CreateComplaintCommand : ICommand<ComplaintDto>
{
    public int GovernmentalEntityId { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ComplaintStatus Status { get; set; }
    public List<IFormFile> ComplaintFiles { get; set; } = [];
}
