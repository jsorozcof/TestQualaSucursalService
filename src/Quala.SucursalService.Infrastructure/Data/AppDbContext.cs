using System.Reflection;
using Ardalis.SharedKernel;
using Quala.SucursalService.Core.ContributorAggregate;
using Microsoft.EntityFrameworkCore;
using Quala.SucursalService.Core.HeadquartersAggregate;

namespace Quala.SucursalService.Infrastructure.Data;
public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }



  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TbHeadquarters>()
    .HasOne(h => h.Moneda)
    .WithMany(c => c.Headquarters)
    .HasForeignKey(h => h.Moneda_Id);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();


}
