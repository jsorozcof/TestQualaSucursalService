using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Quala.SucursalService.UseCases.Headquarters.List;

namespace Quala.SucursalService.UseCases.Headquarters.GetById;

public record GetHeadquarterByIdQuery(int HeadquarterId) : IQuery<Result<ListHeadquartersDto>>;
