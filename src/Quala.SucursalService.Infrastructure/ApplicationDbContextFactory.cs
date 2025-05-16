using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quala.SucursalService.Infrastructure.Data.Identity;

namespace Quala.SucursalService.Infrastructure;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    // Ruta a appsettings.json del proyecto Web
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Quala.SucursalService.Web"))
        .AddJsonFile("appsettings.Development.json")
        .Build();

    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    optionsBuilder.UseSqlServer(connectionString);

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}
