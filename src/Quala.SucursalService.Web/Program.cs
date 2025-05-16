using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using Quala.SucursalService.Core.ContributorAggregate;
using Quala.SucursalService.Core.Interfaces;
using Quala.SucursalService.Infrastructure;
using Quala.SucursalService.Infrastructure.Data;
using Quala.SucursalService.Infrastructure.Email;
using Quala.SucursalService.UseCases.Contributors.Create;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using FluentValidation;
using Serilog;
using Serilog.Extensions.Logging;
using Quala.SucursalService.Web.Common;
using Quala.SucursalService.Core.CurrencyAggregate;
using Quala.SucursalService.UseCases.Headquarters.Upsert;
using Quala.SucursalService.Core.HeadquartersAggregate;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
using Quala.SucursalService.Web;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

// Configure Web Behavior
builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                  o.ShortSchemaNames = true;
                });

ConfigureMediatR();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Guard.Against.Null(connectionString, nameof(connectionString));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger);

if (builder.Environment.IsDevelopment())
{
  // Use a local test email server
  // See: https://ardalis.com/configuring-a-local-test-email-server/
  builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();
  builder.Services.AddScoped<IHeadquartersRepository, HeadquartersRepository>();

  // Otherwise use this:
  //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
  AddShowAllServicesSupport();
}
else
{
  builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();
}

// Register FluentValidation validators
//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<ValidationRequestService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Transient);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}

app.UseFastEndpoints()
    .UseSwaggerGen(); // Includes AddFileServer and static files middleware

app.UseHttpsRedirection();

SeedDatabase(app);

app.Run();

static async void SeedDatabase(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    //          context.Database.Migrate();
    context.Database.EnsureCreated();
    //SeedData.Initialize(services);
    await SeedData.InitializeAsync(scope.ServiceProvider);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

void ConfigureMediatR()
{
  var mediatRAssemblies = new[]
{
  Assembly.GetAssembly(typeof(Contributor)), // Core
  Assembly.GetAssembly(typeof(TbCurrency)),
  Assembly.GetAssembly(typeof(TbHeadquarters)),
  Assembly.GetAssembly(typeof(CreateContributorCommand)), // UseCases
   Assembly.GetAssembly(typeof(UpsertHeadquartersCommand))
};
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
  builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
  builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport()
{
  // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
  builder.Services.Configure<ServiceConfig>(config =>
  {
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
  });
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
