namespace Quala.SucursalService.Web.Headquarters;

public class HeadquartersByIdRequest
{
  public const string Route = "/Headquarter/{HeadquarterId:int}";
  public static string BuildRoute(int HeadquarterId) => Route.Replace("{HeadquarterId:int}", HeadquarterId.ToString());

  public int HeadquarterId { get; set; }
}
