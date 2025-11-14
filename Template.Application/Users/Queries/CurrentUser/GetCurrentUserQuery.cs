using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Users.Dtos;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Users.Queries.CurrentUser;

 public class GetCurrentUserQuery :IRequest<Result<UserDto>>
{

}
