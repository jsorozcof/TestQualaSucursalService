using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Quala.SucursalService.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
