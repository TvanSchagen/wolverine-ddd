using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OfferteTool;

public class OfferteContext : DbContext 
{

    public OfferteContext(DbContextOptions<OfferteContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Offerte>().HasKey(o => o.Offertenummer);
        builder.Entity<Offerte>().Property(o => o.RelatieEmail).HasMaxLength(255);
    }
    
    public DbSet<Offerte> Offertes { get; set; }
}

public class OfferteContextFactory : IDesignTimeDbContextFactory<OfferteContext>
{
    public OfferteContext CreateDbContext(string[] args) => 
        new OfferteContext(new DbContextOptionsBuilder<OfferteContext>().UseSqlServer(args[0]).Options);
}