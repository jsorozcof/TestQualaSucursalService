using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Quala.SucursalService.Core.HeadquartersAggregate;
using Quala.SucursalService.Core.Interfaces;

namespace Quala.SucursalService.Infrastructure.Data;
public class HeadquartersRepository : DapperRepository<TbHeadquarters>, IHeadquartersRepository
{
  public HeadquartersRepository(IConfiguration configuration) : base(configuration) { }

  public async Task<(bool status, int code)> UpsertHeadquarterAsync(TbHeadquarters headquarter)
  {
    var parameters = new
    {
      codigo = headquarter.Codigo,
      descripcion = headquarter.Descripcion,
      direccion = headquarter.Direccion,
      identificacion = headquarter.Identificacion,
      fecha_creacion = headquarter.Fecha_Creacion,
      moneda_id = headquarter.Moneda_Id
    };

   var result = await UpsertAsync("fo_suc_upsert", parameters);

    return (result, parameters.codigo);
  }
}
