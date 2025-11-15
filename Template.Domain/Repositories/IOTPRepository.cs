using Template.Domain.Entities;

namespace Template.Domain.Repositories
{
    public interface IOTPRepository : IGenericRepository<OTP>
    {
        public Task<OTP> GetOtpFromCode(string code, string userId);
    }
}
