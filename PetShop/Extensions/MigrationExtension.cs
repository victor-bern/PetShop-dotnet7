using Microsoft.EntityFrameworkCore;
using PetShop.Database.Context;

namespace PetShop.Extensions;


public static class MigrationExtension
{
    public static void RunMigrations(this WebApplication app)
    {
        var context = app.Services.GetRequiredService<PetShopContext>();
        context.Database.Migrate();
    }
}