using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Tokens.Commands;
using Template.Application.Users;
using Template.Application.Users.Commands;
using Template.Application.Users.Commands.ConfirmEmail;
using Template.Application.Users.Dtos;
using Template.Application.Users.Queries.CurrentUser;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(IMediator mediator, IUserContext userContext) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserCommand request)
        {
            var result = await mediator.Send(request);
            if (result.Data != null && result.Data.Any())
            {
                return BadRequest(result.Data);
            }
            if (!result.SuccessStatus)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
        [HttpPost("confirmEmail")]
        public async Task<ActionResult> CofirmUserEmail([FromBody] ConfirmEmailCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.SuccessStatus)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginUserCommand request)
        {
            var result = await mediator.Send(request);
            if (!result.SuccessStatus)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        [Authorize]
        [HttpPost]
        [Route("token/refresh")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestCommand request)
        {
            var response = await mediator.Send(request);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("current")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var query = new GetCurrentUserQuery();
            var result = await mediator.Send(query);
            if (!result.SuccessStatus)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
        /*  [HttpGet("{Id}")]
          [ProducesResponseType(200, StatusCode = 200, Type = typeof(UserDetailedDto))]
          [Authorize]
          public async Task<ActionResult<UserDetailedDto>> GetFullUserProfile([FromRoute] string Id)
          {
              var user = await mediator.Send(new GetUserDetailsByIdQuery(Id));
              if (!user.SuccessStatus)
              {
                  return BadRequest(user.Errors);
              }
              if (user.Data == null)
              {
                  return NotFound();
              }
              return Ok(user.Data);
          }
      */
    }
}
