using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Reports.Queries.Statuses;
using Template.Domain;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("status")]
        public async Task<ActionResult> GetComplaintsReportsForStatuses(DateTime from, DateTime to, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintReportForAllStatusesQuery(from, to, govermentalEntityId, location));
            return Ok(result.Data);
        }
        [HttpGet("by-gov-entity")]
        public async Task<ActionResult> GetComplaintReportForGovermentalEntities(DateTime from, DateTime to, ComplaintStatus status, string location)
        {
            return Ok();
        }
        [HttpGet("by-location-name")]
        public async Task<ActionResult> GetComplaintReportForGovermentalEntities(DateTime from, DateTime to, ComplaintStatus status, int govermentalEntityId, string location)
        {
            return Ok();
        }
    }
}
