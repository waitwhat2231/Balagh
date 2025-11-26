using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;
using Template.Domain;

namespace Template.Application.Complaints.Commands.Update;

public class UpdateComplaintCommand : ICommand<ComplaintDto>
{
    [JsonIgnore]
    [BindNever]
    public int ComplaintId { get; set; }
    public int? GovernmentalEntityId { get; set; }
    public string? Location { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public ComplaintStatus? NewStatus { get; set; }

    public List<IFormFile> ComplaintFiles { get; set; } = [];
}
