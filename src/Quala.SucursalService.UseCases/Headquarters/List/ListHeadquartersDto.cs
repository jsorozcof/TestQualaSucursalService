namespace Quala.SucursalService.UseCases.Headquarters.List;
public class ListHeadquartersDto
{
  public int Codigo { get; set; }
  public string Descripcion { get; set; } = string.Empty;
  public string Direccion { get; set; } = string.Empty;
  public string Identificacion { get; set; } = string.Empty;
  public DateTime FechaCreacion { get; set; }
  public string Moneda { get; set; } = string.Empty;
  public int MonedaId { get; set; }

  


}
