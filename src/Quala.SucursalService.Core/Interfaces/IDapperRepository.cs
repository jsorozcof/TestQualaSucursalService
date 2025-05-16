using Quala.SucursalService.Core.HeadquartersAggregate;

namespace Quala.SucursalService.Core.Interfaces;

public interface IDapperRepository<T>
{
  Task<IEnumerable<T>> GetAllAsync(string storedProcedure);
  Task<T?> GetByIdAsync(string storedProcedure, object parameters);
  Task<bool> UpsertAsync(string storedProcedure, object parameters);
  Task UpdateAsync(string storedProcedure, object parameters);
  Task DeleteAsync(string storedProcedure, object parameters);
}
