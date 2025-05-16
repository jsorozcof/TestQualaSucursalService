using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;

namespace Quala.SucursalService.Core.CurrencyAggregate;

public class TbCurrency : EntityBase, IAggregateRoot
{

  [Required]
  public string Codigo { get; set; } = string.Empty;

  [Required]
  public string Nombre { get; set; } = string.Empty;

}
