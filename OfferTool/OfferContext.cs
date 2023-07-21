using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Wolverine;

namespace OfferTool;

public class OfferContext : DbContext 
{
    private readonly IMessageContext _messaging;

    public OfferContext(DbContextOptions<OfferContext> options, IMessageContext messaging)
        : base(options)
    {
        _messaging = messaging;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Offer>().HasKey(o => o.OfferNumber);
        builder.Entity<Offer>().Property(o => o.EmailAddress).HasMaxLength(255);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not Entity e) continue;
            
            var events = e.GetDomainEvents();
            foreach (var @event in events) await _messaging.PublishAsync(@event);
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    public DbSet<Offer> Offers { get; set; }
}

public class OfferContextFactory : IDesignTimeDbContextFactory<OfferContext>
{
    public OfferContext CreateDbContext(string[] args) => 
        new OfferContext(new DbContextOptionsBuilder<OfferContext>().UseSqlServer(args[0]).Options, null!);
}