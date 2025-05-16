using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using Quala.SucursalService.Core.HeadquartersAggregate.Dto;
using Quala.SucursalService.Core.Interfaces;
using Quala.SucursalService.UseCases.Contributors;

namespace Quala.SucursalService.UseCases.Headquarters.List;

public class ListHeadquartersHandle : IQueryHandler<ListHeadquartersQuery, Result<IEnumerable<ListHeadquartersDto>>>
{
  private readonly IHeadquartersRepository _repository;
  private readonly IMapper _mapper;
  public ListHeadquartersHandle(IHeadquartersRepository repository, IMapper mapper)
  {
    _repository = repository;
    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
  }

  public async Task<Result<IEnumerable<ListHeadquartersDto>>>  Handle(ListHeadquartersQuery request, CancellationToken cancellationToken)
  {
    try
    {

      var result = await _repository.GetAllHeadquarterAsync();

      var mapResult = _mapper.Map<IEnumerable<ListHeadquartersDto>>(result);

      return Result.Success(mapResult);

    }
    catch (Exception ex)
    {
      return Result.Error(new string[] { $"Error al obtener los datos: {ex.Message}" });
    }
  }
}
