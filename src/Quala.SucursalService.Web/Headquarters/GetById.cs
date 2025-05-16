using Ardalis.Result;
using Azure;
using FastEndpoints;
using MediatR;
using Quala.SucursalService.UseCases.Headquarters.GetById;

namespace Quala.SucursalService.Web.Headquarters;


/// <summary>
/// Get a Headquarters by integer ID.
/// </summary>
public class GetById(IMediator _mediator) : Endpoint<HeadquartersByIdRequest, ApiResponse<object>>
{
  public override void Configure()
  {
    Get(HeadquartersByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(HeadquartersByIdRequest request,  CancellationToken ct)
  {
    var command = new GetHeadquarterByIdQuery(request.HeadquarterId);

    var result = await _mediator.Send(command, ct);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    if (result.IsSuccess)
    {
      //Response = new CompanyRecord(
      //  result.Value.Id,
       
      //  );
    }
  }
}
