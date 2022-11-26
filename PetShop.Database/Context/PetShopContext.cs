using Microsoft.EntityFrameworkCore;
using PetShop.Database.Mappings;
using PetShop.Database.Seeds;
using PetShop.Domain.Entities;

namespace PetShop.Database.Context;

public class PetShopContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public PetShopContext(DbContextOptions opt) : base(opt)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new ProductMapping());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetShopContext).Assembly).Seed();

    }
    

    public override void Dispose()
    {
        // Database.EnsureDeleted();
        base.Dispose();
    }
}