using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.AuthEntities;
using Template.Domain.Repositories;

namespace Template.Application.Tokens.Commands;

internal class RefreshTokenRequestCommandHandler(ITokenRepository tokenRepository,
    ILogger<RefreshTokenRequestCommandHandler> logger,
    IUserContext userContext) : IRequestHandler<RefreshTokenRequestCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string user;
            if (userContext.GetCurrentUser() != null)
            {
                user = userContext.GetCurrentUser().Id.ToString();
            }
            else
            {
                var token = userContext.GetAccessToken();
                if (token == null)
                {
                    throw new InvalidOperationException();
                }
                user = tokenRepository.ReadInvalidToken(token);
            }
            var req = new Domain.Entities.AuthEntities.RefreshTokenRequest
            {
                user_id = user.ToString(),
                RefreshToken = request.RefreshToken
            };
            var response = await tokenRepository.VerifyRefreshToken(req);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
