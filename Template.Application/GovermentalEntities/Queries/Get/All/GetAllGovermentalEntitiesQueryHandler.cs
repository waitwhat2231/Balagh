using AutoMapper;
using Template.Application.Abstraction.Queries;
using Template.Application.GovermentalEntities.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.GovermentalEntities.Queries.Get.All
{
    public class GetAllGovermentalEntitiesQueryHandler(IGovermentalEntitiesRepository govermentalEntitiesRepository, IMapper mapper) : IQueryHandler<GetAllGovermentalEntitiesQuery, List<GovermentalEntitiesDto>>
    {
        public async Task<Result<List<GovermentalEntitiesDto>>> Handle(GetAllGovermentalEntitiesQuery request, CancellationToken cancellationToken)
        {
            var govEnt = await govermentalEntitiesRepository.GetAllAsync();
            var res = mapper.Map<List<GovermentalEntitiesDto>>(govEnt);
            return Result.Success(res);
        }
    }
}
