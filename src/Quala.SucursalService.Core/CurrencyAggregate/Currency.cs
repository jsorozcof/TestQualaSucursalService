using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;
using Quala.SucursalService.Core.HeadquartersAggregate;

namespace Quala.SucursalService.Core.CurrencyAggregate;

public class TbCurrency : EntityBase, IAggregateRoot
{

  [Required]
  public string Codigo { get; set; } = string.Empty;

  [Required]
  public string Nombre { get; set; } = string.Empty;
  public ICollection<TbHeadquarters> Headquarters { get; set; } = new List<TbHeadquarters>();
}
