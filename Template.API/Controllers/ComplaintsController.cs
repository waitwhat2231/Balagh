using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Complaints.Commands.Create;
using Template.Application.Complaints.Commands.Proceed;
using Template.Application.Complaints.Commands.Update;
using Template.Application.Complaints.Dtos;
using Template.Application.Complaints.Notes.Commands;
using Template.Application.Complaints.Queries.GetAll;
using Template.Application.Complaints.Queries.GetById;
using Template.Domain.Enums;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintsController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost]
    [Route("CreateComplaint")]
    public async Task<ActionResult<ComplaintDto>> CreatePrize([FromForm] CreateComplaintCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }

    [Authorize]
    [HttpPost]
    [Route("UpdateComplaint/{complaintId:int}")]
    public async Task<ActionResult<ComplaintDto>> UpdateComplaint([FromRoute] int complaintId, [FromForm] UpdateComplaintCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }

    [HttpGet]
    [Authorize]
    [Route("GetAllComplaints")]
    public async Task<ActionResult<IEnumerable<ComplaintDto>>> GetAllComplaints(int pageNum, int pageSize)
    {
        var result = await mediator.Send(new GetAllComplaintsQuery(pageNum, pageSize));
        return Ok(result.Data);
    }

    [HttpGet]
    [Authorize]
    [Route("GetComplaintById/{complaintId:int}")]
    public async Task<ActionResult<ComplaintDto>> GetComplaintById([FromRoute] int complaintId)
    {
        var result = await mediator.Send(new GetComplaintByIdQuery(complaintId));
        return Ok(result.Data);
    }
    [HttpPost]
    [Authorize(Roles = $"{nameof(EnumRoleNames.Employee)},{nameof(EnumRoleNames.Administrator)}")]
    [Route("{complaintId:int}/notes/add")]
    public async Task<ActionResult> AddNotesToComplaint([FromRoute] int complaintId, [FromBody] AddNoteCommand addNoteCommand)
    {
        addNoteCommand.ComplaintId = complaintId;
        var result = await mediator.Send(addNoteCommand);
        if (!result.SuccessStatus)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(EnumRoleNames.Employee)},{nameof(EnumRoleNames.Administrator)}")]
    [Route("ProceedComplaint/{complaintId:int}")]
    public async Task<ActionResult<ComplaintDto>> ProceedComplaint([FromRoute] int complaintId, [FromBody] ProceedComplaintCommand command)
    {
        command.ComplaintId = complaintId;
        var result = await mediator.Send(command);
        if (!result.SuccessStatus)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
