using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.AuthEntities;

namespace Template.Application.Tokens.Commands;

public class RefreshTokenRequestCommand : IRequest<AuthResponse>
{
    public string? RefreshToken { get; set; }
}

