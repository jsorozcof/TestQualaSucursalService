using System.Data;
using Ardalis.GuardClauses;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Quala.SucursalService.Core.Interfaces;

namespace Quala.SucursalService.Infrastructure.Data;

public class DapperRepository<T> : IDapperRepository<T>
{
  private readonly string _connectionString;

  public DapperRepository(IConfiguration configuration)
  {
    _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    Guard.Against.Null(_connectionString, nameof(_connectionString));

  }

  public Task DeleteAsync(string storedProcedure, object parameters)
  {
    throw new NotImplementedException();
  }

  public async Task<(bool success, string message)> ExecuteWithMessageAsync(string storedProcedure, DynamicParameters parameters)
  {
    try
    {
      using var connection = new SqlConnection(_connectionString);
      await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

      string message = parameters.Get<string>("@Resultado");

      return (!string.IsNullOrEmpty(message) && !message.StartsWith("Error"), message);
    }
    catch (SqlException ex)
    {
      return (false, $"Error en SQL: {ex.Message}");
    }
    catch (Exception ex)
    {
      return (false, $"Error inesperado: {ex.Message}");
    }
  }


  public async Task<IEnumerable<T>> GetAllAsync(string storedProcedure)
  {
    try
    {
      using var connection = new SqlConnection(_connectionString);
      return await connection.QueryAsync<T>(storedProcedure, commandType: CommandType.StoredProcedure);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error al obtener todos los registros: {ex.Message}");
      throw;
    }
  }

  public async Task<T?> GetByIdAsync(string storedProcedure, object parameters)
  {
    try
    {
      using var connection = new SqlConnection(_connectionString);
      Guard.Against.Null(parameters, nameof(parameters), "El parámetro no puede ser nulo.");
      var result = await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
      return result;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error al obtener el registro por ID: {ex.Message}");
      throw;
    }
  }

  public Task UpdateAsync(string storedProcedure, object parameters)
  {
    throw new NotImplementedException();
  }

  public async Task<bool> UpsertAsync(string storedProcedure, DynamicParameters parameters)
      => (await ExecuteWithMessageAsync(storedProcedure, parameters)).success;

  //public async Task InsertAsync(string storedProcedure, object parameters) => await ExecuteAsync(storedProcedure, parameters);

  //public async Task UpdateAsync(string storedProcedure, object parameters) => await ExecuteAsync(storedProcedure, parameters);

  //public async Task DeleteAsync(string storedProcedure, object parameters) => await ExecuteAsync(storedProcedure, parameters);
}
