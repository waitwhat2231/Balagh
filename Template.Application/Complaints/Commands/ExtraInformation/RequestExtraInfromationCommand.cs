using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Complaints.Commands.ExtraInformation
{
    public class RequestExtraInfromationCommand : ICommand
    {
        [JsonIgnore]
        [BindNever]
        [Required]
        public int ComplaintId { get; set; }
        public string Message { get; set; } = "Extra Information About this  Complaint Required";
    }
}
