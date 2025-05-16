using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Quala.SucursalService.Core.HeadquartersAggregate;
using Quala.SucursalService.Core.HeadquartersAggregate.Dto;
using Quala.SucursalService.Core.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quala.SucursalService.Infrastructure.Data;
public class HeadquartersRepository : DapperRepository<GetAllHeadquartersDto>, IHeadquartersRepository
{
  public HeadquartersRepository(IConfiguration configuration) : base(configuration) { }

  public async Task<IEnumerable<GetAllHeadquartersDto>> GetAllHeadquarterAsync()
  {
    var headquartersList = await GetAllAsync("dbo.fo_suc_get_all");

    return headquartersList.Select(h => new GetAllHeadquartersDto
    {
      Codigo = h.Codigo,
      Descripcion = h.Descripcion,
      Direccion = h.Direccion,
      Identificacion = h.Identificacion,
      FechaCreacion = h.FechaCreacion,
      MonedaId = h.MonedaId,
      Moneda = h.Moneda
    });
  }

  public async Task<(bool status, int code, string message)> UpsertHeadquarterAsync(TbHeadquarters headquarter)
  {
    var parameters = new DynamicParameters();
    parameters.Add("@Codigo", headquarter.Codigo, DbType.Int32);
    parameters.Add("@Descripcion", headquarter.Descripcion, DbType.String, size: 250);
    parameters.Add("@Direccion", headquarter.Direccion, DbType.String, size: 250);
    parameters.Add("@Identificacion", headquarter.Identificacion, DbType.String, size: 50);
    parameters.Add("@Fecha_Creacion", headquarter.Fecha_Creacion, DbType.DateTime);
    parameters.Add("@Moneda_Id", headquarter.Moneda_Id, DbType.Int32);
    parameters.Add("@Resultado", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

    var (success, message) = await ExecuteWithMessageAsync("dbo.fo_suc_create_or_update", parameters);

    return (success, headquarter.Codigo, message);
  }
}
