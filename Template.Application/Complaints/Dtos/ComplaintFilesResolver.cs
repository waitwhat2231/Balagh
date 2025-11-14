using AutoMapper;
using Microsoft.AspNetCore.Http;
using Template.Domain.Entities;

namespace Template.Application.Complaints.Dtos;

public class ComplaintFilesResolver(IHttpContextAccessor httpContextAccessor) : IValueResolver<ComplaintFile, ComplaintFileDto, string>
{
    public string Resolve(ComplaintFile source, ComplaintFileDto destination, string destMember, ResolutionContext context)
    {
        var request = httpContextAccessor.HttpContext?.Request;

        if (request == null || string.IsNullOrEmpty(source.Path)) return "";


        return $"{request.Scheme}://{request.Host}/{source.Path.Replace("\\", "/")}";
    }
}
