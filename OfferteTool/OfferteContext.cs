using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Wolverine;
using Wolverine.EntityFrameworkCore;

namespace OfferteTool;

public class OfferteContext : DbContext 
{
    private readonly IMessageContext _messaging;

    public OfferteContext(DbContextOptions<OfferteContext> options, IMessageContext messaging)
        : base(options)
    {
        _messaging = messaging;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Offerte>().HasKey(o => o.Offertenummer);
        builder.Entity<Offerte>().Property(o => o.RelatieEmail).HasMaxLength(255);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not Entity e) continue;
            
            var events = e.GetDomainEvents();
            foreach (var @event in events) await _messaging.SendAsync(@event);
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    public DbSet<Offerte> Offertes { get; set; }
}

public class OfferteContextFactory : IDesignTimeDbContextFactory<OfferteContext>
{
    public OfferteContext CreateDbContext(string[] args) => 
        new OfferteContext(new DbContextOptionsBuilder<OfferteContext>().UseSqlServer(args[0]).Options, null!);
}