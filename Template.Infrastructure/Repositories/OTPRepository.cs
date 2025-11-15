using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
    public class OTPRepository(TemplateDbContext dbcontext) : GenericRepository<OTP>(dbcontext), IOTPRepository
    {
        public async Task<OTP> GetOtpFromCode(string code)
        {
            var otp = await dbContext.OTPs.FirstOrDefaultAsync(ot => ot.Code == code);
            return otp;
        }
    }
}
