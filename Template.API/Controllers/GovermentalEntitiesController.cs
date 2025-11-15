using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.GovermentalEntities.Queries.Get.All;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/govermentalEntities")]
    public class GovermentalEntitiesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetGovermentalEntities()
        {
            var result = await mediator.Send(new GetAllGovermentalEntitiesQuery());
            return Ok(result.Data);
        }
    }
}
