using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Reports.Queries.GovermentalEntities;
using Template.Application.Reports.Queries.PDF.GovermentalEntity;
using Template.Application.Reports.Queries.PDF.Statuses;
using Template.Application.Reports.Queries.PDF.TimeBased;
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
        public async Task<ActionResult> GetComplaintsReportsForStatuses(DateTime? from = null, DateTime? to = null, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintReportForAllStatusesQuery(from, to, govermentalEntityId, location));
            return Ok(result.Data);
        }

        [HttpGet("status/export")]
        //[Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GenerateStatusReportPdf(DateTime? from = null, DateTime? to = null, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GenerateStatusReportAsPdfQuery(from, to, govermentalEntityId, location));
            return File(result,
                "application/pdf",
                "complaint-status-summary.pdf");
        }


        [HttpGet("by-gov-entity")]
        [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GetComplaintReportForGovermentalEntities(DateTime? from = null, DateTime? to = null, ComplaintStatus? status = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintReportForAllGovermentalEntitiesQuery(from, to, null, location, status));
            return Ok(result.Data);
        }


        [HttpGet("by-gov-entity/export")]
        // [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GenerateGovermentalEntityReportPdf(DateTime? from = null, DateTime? to = null, ComplaintStatus? status = null, string? location = null)
        {
            var result = await mediator.Send(new GenerateGovermentalEntityReportAsPdfQuery(from, to, null, location, status));
            return File(result,
                "application/pdf",
                "complaint-govEntities-summary.pdf");
        }


        [HttpGet("by-time")]
        [Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GetComplaintReportForYearsAndMonths(ComplaintStatus? status = null, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GetComplaintTimeReportQuery(govermentalEntityId, location, status));
            return Ok(result.Data);
        }



        [HttpGet("by-time/export")]
        //[Authorize(Roles = nameof(EnumRoleNames.Administrator))]
        public async Task<ActionResult> GenerateTimeBasedReportPdf(ComplaintStatus? status = null, int? govermentalEntityId = null, string? location = null)
        {
            var result = await mediator.Send(new GenerateTimeBasedReportPdfQuery(govermentalEntityId, location, status));
            return File(
                result,
                "application/pdf",
                "complaint-timebased-summary.pdf"
                );
        }
    }
}
