using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Template.Application.Abstraction.Commands;
using Template.Application.Complaints.Dtos;

namespace Template.Application.Complaints.Commands.Proceed;

public class ProceedComplaintCommand : ICommand<ComplaintDto>
{
    [BindNever]
    [JsonIgnore]
    public int ComplaintId { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = [];
}
