namespace Quala.SucursalService.Web;

public class ApiResponse<T>
{
  private T? _data;

  public bool Success { get; set; }

  public string? SuccessMessage { get; set; } = null!;

  public T? Data { get => _data; set => _data = value; }
  public string? Error { get; set; } = null!;

  public int StatusCode { get; set; }

  public static ApiResponse<T> SuccessResult(T data, string successMessage, int statusCode = StatusCodes.Status200OK)
  {
    return new ApiResponse<T> { Success = true, SuccessMessage = successMessage, Data = data, StatusCode = statusCode };
  }

  public static ApiResponse<T> FailureResult(string error, int statusCode = StatusCodes.Status400BadRequest)
  {
    return new ApiResponse<T> { Success = false, Error = error, StatusCode = statusCode };
  }
}
