using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Database.Context;

namespace PetShop.Database;

public static class StartupDatabase
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<PetShopContext>(options =>
            options.UseSqlServer(connectionString));
}