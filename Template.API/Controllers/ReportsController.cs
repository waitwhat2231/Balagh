using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Reports.Queries.GovermentalEntities;
using Template.Application.Reports.Queries.Statuses;
using Template.Application.Reports.Queries.Time;
using Template.Domain;
using Template.Domain.Enums;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("status")]
        [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GetComplaintsReportsForStatuses(DateTime from, DateTime to, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintReportForAllStatusesQuery(from, to, govermentalEntityId, location));
            return Ok(result.Data);
        }
        [HttpGet("by-gov-entity")]
        [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GetComplaintReportForGovermentalEntities(DateTime from, DateTime to, ComplaintStatus? status = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintReportForAllGovermentalEntitiesQuery(from, to, null, location, status));
            return Ok(result.Data);
        }
        [HttpGet("by-time")]
        [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GetComplaintReportForYearsAndMonths(ComplaintStatus? status = null, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintTimeReportQuery(govermentalEntityId, location, status));
            return Ok(result.Data);
        }
    }
}
