using AutoMapper;
using Template.Application.Users.Commands;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;

namespace Template.Application.Users.UserProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<User, UserDto>()
            .ReverseMap();


        CreateMap<User, UserDetailedDto>()
            .ForMember(dest => dest.Devices, opt => opt.MapFrom(src => src.Devices))
            .ReverseMap();

        CreateMap<(User user, string roleName), UserDto>()

            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.user.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.user.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.user.PhoneNumber))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.roleName));
    }

}