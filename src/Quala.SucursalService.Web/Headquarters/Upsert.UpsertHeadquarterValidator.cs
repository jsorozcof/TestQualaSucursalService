using FluentValidation;

namespace Quala.SucursalService.Web.Headquarters;


public class UpsertHeadquarterValidator : AbstractValidator<UpsertHeadquarterRequest>
{
  public UpsertHeadquarterValidator()
  {
    RuleFor(x => x.Codigo).NotEmpty().WithMessage("Codigo es requerido.")
      .GreaterThan(0).WithMessage("El número debe ser mayor que cero.");

    RuleFor(x => x.Descripcion).NotEmpty().WithMessage("Descripcion es requerido.")
      .MaximumLength(50).WithMessage("La descripcion no puede superar los 50 caracteres.");
    RuleFor(x => x.Direccion).NotEmpty().WithMessage("Direccion es requerido.");

    RuleFor(x => x.Identificacion).NotEmpty().WithMessage("Identificacion es requerido.")
      .MaximumLength(50).WithMessage("La identificación no puede superar los 50 caracteres.");

    RuleFor(x => x.MonedaId).NotEmpty().WithMessage("Moneda es requerido.");
  }
}
