namespace Quala.SucursalService.Core.HeadquartersAggregate.Dto;
public class GetAllHeadquartersDto
{
  public int Codigo { get; set; }
  public string Descripcion { get; set; } = default!;
  public string Direccion { get; set; } = default!;
  public string Identificacion { get; set; } = default!;
  public DateTime FechaCreacion { get; set; }
  public int MonedaId { get; set; }
  public string Moneda { get; set; } = default!;
}
