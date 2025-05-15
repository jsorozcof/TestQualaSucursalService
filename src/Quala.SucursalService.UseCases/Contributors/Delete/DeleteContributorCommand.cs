using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Quala.SucursalService.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
