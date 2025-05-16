using FastEndpoints;
using MediatR;
using Quala.SucursalService.UseCases.Headquarters.Upsert;
using Quala.SucursalService.Web.Common;

namespace Quala.SucursalService.Web.Headquarters;


public class Upsert : Endpoint<UpsertHeadquarterRequest, ApiResponse<object>>
{
  private readonly IMediator _mediator;
  private readonly IServiceProvider _serviceProvider;
  public Upsert(IMediator mediator, IServiceProvider serviceProvider)
  {
    _mediator = mediator;
    _serviceProvider = serviceProvider;
  }
  public override void Configure()
  {
    Post(UpsertHeadquarterRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Permite gestionar un CRUD de sucursales para la empresa QUALA";
      s.Description = "";
    });
  }


  public override async Task HandleAsync(UpsertHeadquarterRequest req, CancellationToken ct)
  {
    #region Validation request
    using var scope = _serviceProvider.CreateScope();
    var validationService = scope.ServiceProvider.GetRequiredService<ValidationRequestService>();

    var validationResult = await validationService.ValidateAsync(req);
    if (validationResult is not null)
    {
      await SendAsync(ApiResponse<object>.SuccessResult(validationResult, "", StatusCodes.Status400BadRequest), cancellation: ct);
      return;
    }
    #endregion

    var result = await _mediator.Send(new UpsertHeadquartersCommand(
                                          req.Codigo,req.Descripcion, 
                                          req.Direccion, req.Identificacion,
                                          req.FechaCreacion, req.MonedaId), ct);
    if (result.IsSuccess)
    {
      var response = ApiResponse<object>.SuccessResult(result.Value, "La operación se realizó con éxito", StatusCodes.Status200OK);
      await SendAsync(response, cancellation: ct);
    }
    else
    {
      var errorResponse = ApiResponse<object>.FailureResult(result.Errors.First(), StatusCodes.Status400BadRequest);
      await SendAsync(errorResponse, cancellation: ct);
    }

  }
}
