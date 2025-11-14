using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ResponseEntity;

namespace Template.Application.Abstraction.Queries;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery,Result> where TQuery : IQuery
{
}

public interface IQueryHandler<TQuery,TResult> : IRequestHandler<TQuery, Result<TResult>> where TQuery : IQuery<TResult>
{
}
