using AutoMapper;
using Template.Application.Complaints.Commands.Create;
using Template.Domain.Entities;

namespace Template.Application.Complaints.Dtos;

public class ComplaintProfile : Profile
{
    public ComplaintProfile()
    {
        CreateMap<CreateComplaintCommand, Complaint>()
            .ForMember(dest => dest.ComplaintFiles, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Complaint, ComplaintDto>()
            .ReverseMap();
        CreateMap<(Complaint complaint, string userName), ComplaintDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.userName)).ReverseMap();

        CreateMap<ComplaintFile, ComplaintFileDto>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom<ComplaintFilesResolver>())
            .ReverseMap();
    }
}
