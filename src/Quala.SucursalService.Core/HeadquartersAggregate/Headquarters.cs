using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;
using Quala.SucursalService.Core.CurrencyAggregate;

namespace Quala.SucursalService.Core.HeadquartersAggregate;

public class TbHeadquarters : EntityBase, IAggregateRoot
{
  public int Codigo { get; set; }
  public string Descripcion { get; set; } = default!;
  public string Direccion { get; set; } = default!;
  public string Identificacion { get; set; } = default!;
  public DateTime Fecha_Creacion { get; set; }
  public int Moneda_Id { get; set; }
  public TbCurrency Moneda { get; set; } = default!;

}

