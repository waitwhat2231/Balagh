using AutoMapper;

namespace Template.Application.GovermentalEntities.Dtos
{
    public class GovermentalEntitiesProfile : Profile
    {
        public GovermentalEntitiesProfile()
        {
            CreateMap<GovermentalEntitiesDto, Domain.Entities.GovernmentalEntity>().ReverseMap();
        }
    }
}
