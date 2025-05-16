using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using Quala.SucursalService.Core.HeadquartersAggregate;
using Quala.SucursalService.Core.HeadquartersAggregate.Specifications;
using Quala.SucursalService.UseCases.Headquarters.List;

namespace Quala.SucursalService.UseCases.Headquarters.GetById;


public class GetHeadquarterByIdHandler : IQueryHandler<GetHeadquarterByIdQuery, Result<ListHeadquartersDto>>
{
  private readonly IMapper _mapper;
  IReadRepository<TbHeadquarters> _repository;

  public GetHeadquarterByIdHandler(IReadRepository<TbHeadquarters> repository, IMapper mapper)
  {
    _mapper = mapper;
    _repository = repository;
  }
  public async Task<Result<ListHeadquartersDto>> Handle(GetHeadquarterByIdQuery request, CancellationToken cancellationToken)
  {
    var spec = new GetHeadquarterByIdSpec(request.HeadquarterId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (entity == null)
      return Result.Error("La sede no existe.");

    var dto = _mapper.Map<ListHeadquartersDto>(entity);

    return Result.Success(dto);

  }
}
