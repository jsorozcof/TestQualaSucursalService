using Ardalis.Result;
using Ardalis.SharedKernel;
using Quala.SucursalService.Core.HeadquartersAggregate;
using Quala.SucursalService.Core.HeadquartersAggregate.Specifications;
using Quala.SucursalService.Core.Interfaces;

namespace Quala.SucursalService.UseCases.Headquarters.Upsert;
/// <summary>
/// En esta clase estoy inplementando el patron de diseño especificacion y repository
/// como ejmeplo
/// </summary>
public class UpsertHeadquartersHandler : ICommandHandler<UpsertHeadquartersCommand, Result>
{
  private readonly IHeadquartersRepository _repository;
  public UpsertHeadquartersHandler(IHeadquartersRepository repository)
  {
   _repository = repository;  
  }

  public async Task<Result> Handle(UpsertHeadquartersCommand req, CancellationToken cancellationToken)
  {


    try
    {

      var setEntity = new TbHeadquarters()
      {
        Codigo = req.Codigo,
        Descripcion = req.Descripcion,
        Direccion = req.Direccion,
        Identificacion = req.Identificacion,
        Fecha_Creacion = req.FechaCreacion,
        Moneda_Id = req.MonedaId
      };

      var (status, code) = await _repository.UpsertHeadquarterAsync(setEntity);

      if (status)
        return Result.Success(); 
      
      else
        return Result.Error(new string[] { $"Error en actualización. Código de sucursal: {code}" });

    }

    catch (Exception ex)
    {
      return Result.Error($"Error inesperado al tratar de consumir el procedimiento almacenado. Detalle: {ex.Message}");
    }

  }
}
