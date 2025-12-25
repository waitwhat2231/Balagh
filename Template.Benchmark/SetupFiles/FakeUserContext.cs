using Template.Application.Users;

namespace Template.Benchmark.SetupFiles
{
    public class FakeUserContext : IUserContext
    {
        public string? GetAccessToken()
        {
            throw new NotImplementedException();
        }

        public CurrentUser? GetCurrentUser() => new("User-123", "TestMail@mail.com", ["User"]);
    }
}
