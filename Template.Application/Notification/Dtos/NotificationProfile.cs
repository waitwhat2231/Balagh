using AutoMapper;

namespace Template.Application.Notification.Dtos;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Domain.Entities.Notification, NotificationDto>();
    }
}
