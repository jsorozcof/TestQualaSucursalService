using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quala.SucursalService.UseCases.Headquarters.GetById;
public class HeadquartersByIdRequest
{
  public const string Route = "/Headquarter/{HeadquarterId:int}";
  public static string BuildRoute(int HeadquarterId) => Route.Replace("{HeadquarterId:int}", HeadquarterId.ToString());

  public int HeadquarterId { get; set; }
}
