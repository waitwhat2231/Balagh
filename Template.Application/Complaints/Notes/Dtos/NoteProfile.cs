using AutoMapper;
using Template.Domain.Entities;

namespace Template.Application.Complaints.Notes.Dtos
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<(Note note, string userName), NoteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.note.Id))
                .ForMember(dest => dest.ComplaintId, opt => opt.MapFrom(src => src.note.ComplaintId))
                .ForMember(dest => dest.NoteBody, opt => opt.MapFrom(src => src.note.NoteBody))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.note.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.userName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.note.CreatedAt))
                .ReverseMap();
        }
    }
}
