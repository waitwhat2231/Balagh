using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Abstraction.Commands;

public interface ICommandHandler <in TCommand>:IRequestHandler<TCommand,Result> where TCommand:ICommand
{
}
public interface ICommandHandler<in TCommand, TResult> :IRequestHandler<TCommand,Result<TResult>> where TCommand:ICommand<TResult>
{
   
}
