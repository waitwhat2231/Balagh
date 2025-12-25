using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Http;
using Template.Application.Complaints.Commands.Create;
using Template.Benchmark.SetupFiles;

namespace Template.Benchmark.Create;

[MemoryDiagnoser]
[ShortRunJob]
public class CreateComplaintBenchMark
{
    private CreateComplaintCommandHandler _handler;
    private CreateComplaintCommand _command;
    [GlobalSetup]
    public void SetUp()
    {
        var logger = new NullLogger<CreateComplaintCommandHandler>();
        var repo = new InMemoryComplaintRepository();
        var userContext = new FakeUserContext();
        var mapper = new FakeMapper();
        var fileService = new FakeFileService();
        _handler = new CreateComplaintCommandHandler(logger, repo, userContext, mapper, fileService);

        // Prepare a realistic command
        _command = new CreateComplaintCommand()
        {
            Location = "Test location",
            Description = "Benchmark test",
            GovernmentalEntityId = 1,
            ComplaintFiles = new List<IFormFile>() // can be empty or fake files
        };
    }
    [Benchmark]
    public async Task Handle()
    {
        await _handler.Handle(_command, CancellationToken.None);
    }

}
