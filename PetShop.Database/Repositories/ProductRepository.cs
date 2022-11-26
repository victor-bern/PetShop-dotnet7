using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PetShop.Database.Context;
using PetShop.Domain.Entities;
using PetShop.Domain.Repositories;

namespace PetShop.Database.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PetShopContext _context;

    public ProductRepository(PetShopContext context)
    {
        _context = context;
    }

    public async Task<IList<Product>> GetAll() => await _context.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetById(int id) =>
        await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IList<Product>> GetProductByPriceWithFilter(decimal value, string filter)
    {
        var valueParam = new SqlParameter("value", value);
        var filterParam = filter == "greater" ? ">" : "<";
        return await _context.Products.FromSqlRaw($"SELECT * FROM Product where Price {filterParam} @value", valueParam)
            .ToListAsync();
    }


    public async Task<Product> SaveProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}