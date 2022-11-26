using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;

namespace PetShop.Database.Seeds;

public static class SeedContext
{
    public static void Seed(this ModelBuilder builder)
    {
        builder.SeedProducts();
    }


    private static void SeedProducts(this ModelBuilder builder)
    {
        var random = new Random();
        List<Product> listProducts = new();
        for (var i = 1; i <= 10; i++)
        {
            var price = random.NextDouble() * (1.0 * 100.0);
            var product = new Product()
            {
                Id = i,
                Title = $"Product {i}",
                Price = decimal.Parse(price.ToString("n2")),
                ProductImageUrl = "Url"
            };
            listProducts.Add(product);
            Console.WriteLine(product.Price);
        }
        builder.Entity<Product>(product =>
        {
            product.HasData(listProducts);
        });
    }
}