using Ardalis.Result;
using Azure;
using FastEndpoints;
using MediatR;
using Quala.SucursalService.UseCases.Headquarters.List;

namespace Quala.SucursalService.Web.Headquarters;

public class List(IMediator _mediator) :  EndpointWithoutRequest<ApiResponse<object>>
{
  public override void Configure()
  {
    Get("/Headquarter/List");
    AllowAnonymous();
    Summary(s =>
    {
      // XML Docs are used by default but are overridden by these properties:
      s.Summary = "Lista de sucursales";
    });
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var command = new ListHeadquartersQuery();

    var result = await _mediator.Send(command, ct);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(ct);
    }

    if (result.IsSuccess)
    {
      var response = ApiResponse<object>.SuccessResult(result.Value, "Datos recuperados correctamente", StatusCodes.Status200OK);
      await SendAsync(response, cancellation: ct);
    }
    else
    {
      var response = ApiResponse<object>.FailureResult($"Algo salio mal: {result.Errors}", StatusCodes.Status400BadRequest);
      await SendAsync(response, cancellation: ct);

    }

  }
}
