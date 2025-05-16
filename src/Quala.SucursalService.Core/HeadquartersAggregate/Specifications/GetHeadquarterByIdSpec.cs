using Ardalis.Specification;

namespace Quala.SucursalService.Core.HeadquartersAggregate.Specifications;

public class GetHeadquarterByIdSpec : Specification<TbHeadquarters>
{
  public GetHeadquarterByIdSpec(int id)
  {
    Query.Where(c => c.Id == id)
            .Include(c => c.Moneda);
  }
}
