using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Abstraction.Commands;

public interface ICommand : IRequest<Result>
{

}
public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}
