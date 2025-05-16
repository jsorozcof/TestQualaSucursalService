using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Quala.SucursalService.Core.Interfaces;
using Quala.SucursalService.Core.Services;
using Quala.SucursalService.Infrastructure.Data;
using Quala.SucursalService.Infrastructure.Data.Queries;
using Quala.SucursalService.Infrastructure.Email;
using Quala.SucursalService.UseCases.Contributors.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quala.SucursalService.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace Quala.SucursalService.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("DefaultConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

    //services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

    services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    services.AddScoped<IHeadquartersRepository, HeadquartersRepository>();


    services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
    services.AddScoped<IDeleteContributorService, DeleteContributorService>();



    services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
