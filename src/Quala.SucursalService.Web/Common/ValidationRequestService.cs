using FluentValidation;

namespace Quala.SucursalService.Web.Common;

public class ValidationRequestService
{
  private readonly IServiceProvider _serviceProvider;

  public ValidationRequestService(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<ValidationErrorResponse?> ValidateAsync<T>(T request, CancellationToken cancellationToken = default)
  {
    var validator = _serviceProvider.GetRequiredService<IValidator<T>>();
    var result = await validator.ValidateAsync(request, cancellationToken);

    return result.IsValid ? null : new ValidationErrorResponse
    {
      Errors = result.Errors.Select(error => new ValidationError
      {
        PropertyName = error.PropertyName,
        ErrorMessage = error.ErrorMessage
      }).ToList()
    };
  }
}
public class ValidationError
{
  public string PropertyName { get; set; } = string.Empty;
  public string ErrorMessage { get; set; } = string.Empty;
}

public class ValidationErrorResponse
{
  public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
}
