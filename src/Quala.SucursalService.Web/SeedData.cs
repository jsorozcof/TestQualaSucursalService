using Microsoft.AspNetCore.Identity;
using Quala.SucursalService.Infrastructure.Data.Identity;

namespace Quala.SucursalService.Web;

public static class SeedData
{
  public static async Task InitializeAsync(IServiceProvider services)
  {
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

    const string adminRole = "Admin";
    const string adminEmail = "admin@quala.com";
    const string adminPassword = "px30$jioA123!";

    if (!await roleManager.RoleExistsAsync(adminRole))
      await roleManager.CreateAsync(new ApplicationRole { Name = adminRole });

    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
      user = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
      await userManager.CreateAsync(user, adminPassword);
      await userManager.AddToRoleAsync(user, adminRole);
    }
  }
}
