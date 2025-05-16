using Quala.SucursalService.Core.HeadquartersAggregate;
using Quala.SucursalService.Core.HeadquartersAggregate.Dto;

namespace Quala.SucursalService.Core.Interfaces;

public interface IHeadquartersRepository
{
  Task<(bool status, int code, string message)> UpsertHeadquarterAsync(TbHeadquarters headquarter);
  Task<IEnumerable<GetAllHeadquartersDto>> GetAllHeadquarterAsync();
}
