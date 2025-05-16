using Quala.SucursalService.Core.HeadquartersAggregate;

namespace Quala.SucursalService.Core.Interfaces;

public interface IHeadquartersRepository
{
  Task<(bool status, int code)> UpsertHeadquarterAsync(TbHeadquarters headquarter);
}
