using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Quala.SucursalService.Core.HeadquartersAggregate.Specifications;

/// <summary>
/// verifico si el codigo de la sucursal existe para no crear duplicados
/// </summary>
public class GetHeadquarterByCodeSpec : Specification<TbHeadquarters>
{
  public GetHeadquarterByCodeSpec(int code)
  {
    Query.Where(c => c.Codigo == code); //&& c.IsActive
  }
}
