using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Complaints.Commands.Create;
using Template.Application.Complaints.Dtos;
using Template.Application.Complaints.Queries.GetAll;
using Template.Application.Complaints.Queries.GetById;

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
}
