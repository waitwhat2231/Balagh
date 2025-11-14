using MediatR;
using Template.Domain.AuthEntities;

namespace Template.Application.Tokens.Commands;

public class RefreshTokenRequestCommand : IRequest<AuthResponse>
{
    public string? RefreshToken { get; set; }
}

