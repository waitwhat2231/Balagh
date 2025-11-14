using Template.Domain.AuthEntities;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthEntities;

namespace Template.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task<string> CreateRefreshToken();
        Task<AuthResponse?> GenerateToken(string UserIdentifier);
        string ReadInvalidToken(string token);
        Task TokenDelete(User user);
        Task<AuthResponse?> VerifyRefreshToken(RefreshTokenRequest request);
    }
}
