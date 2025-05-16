using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Quala.SucursalService.Core.HeadquartersAggregate.Dto;
using Quala.SucursalService.Core.HeadquartersAggregate;
using Quala.SucursalService.UseCases.Headquarters.List;

namespace Quala.SucursalService.Infrastructure.Mapping;

public class HeadquartersMappingProfile : Profile
{
  public HeadquartersMappingProfile()
  {
    CreateMap<GetAllHeadquartersDto, ListHeadquartersDto>();
  }
}
