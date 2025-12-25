using Microsoft.AspNetCore.Http;
using Template.Domain.Services;

namespace Template.Benchmark.SetupFiles
{
    public class FakeFileService : IFileService
    {
        public void DeleteFile(string fileNameWithExtension)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveBytesImage(byte[] bytes, string path)
        {
            throw new NotImplementedException();
        }

        public string SaveFile(IFormFile file, string path, string[] allowedFileExtensions)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveFileAsync(IFormFile file, string path, string[] allowedFileExtensions)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> SaveFilesAsync(List<IFormFile> file, string path, string[] allowedFileExtensions)
        {
            var savedPaths = file.Select((f, i) => $"fake/path/file{i}.jpg").ToList();
            return Task.FromResult(savedPaths);
        }
    }
}
