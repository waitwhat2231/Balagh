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
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Histories, opt => opt.MapFrom(src => src.Histories))
            .ReverseMap();

        CreateMap<(Complaint complaint, string userName), ComplaintDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.userName))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.complaint.Location))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.complaint.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.complaint.Status))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.complaint.UserId))
            .ForMember(dest => dest.GovernmentalEntityId, opt => opt.MapFrom(src => src.complaint.GovernmentalEntityId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.complaint.CreatedAt))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.complaint.Id))
            .ReverseMap();

        CreateMap<History, HistoryDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(h => h.User.UserName));


        CreateMap<ComplaintFile, ComplaintFileDto>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom<ComplaintFilesResolver>())
            .ReverseMap();
    }
}
