using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Abstraction.Queries;

public interface IQuery:IRequest<Result>
{
}
public interface IQuery<TResult> : IRequest<Result<TResult>> 
{

}
