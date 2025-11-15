using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class OTPRepository(TemplateDbContext dbcontext) : GenericRepository<OTP>(dbcontext), IOTPRepository
    {
        public async Task<OTP> GetOtpFromCode(string code, string userId)
        {
            var otp = await dbContext.OTPs.FirstOrDefaultAsync(ot => ot.Code == code && ot.UserId == userId);
            return otp;
        }
    }
}
