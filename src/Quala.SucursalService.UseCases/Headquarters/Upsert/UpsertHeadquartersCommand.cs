using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Quala.SucursalService.UseCases.Headquarters.Upsert;

public record UpsertHeadquartersCommand(
   int Codigo,
   string Descripcion,
   string Direccion,
   string Identificacion,
   DateTime FechaCreacion,
   int MonedaId

  ) : ICommand<Result>;

