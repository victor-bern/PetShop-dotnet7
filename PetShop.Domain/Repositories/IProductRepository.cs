using PetShop.Domain.Entities;

namespace PetShop.Domain.Repositories;

public interface IProductRepository
{
    Task<IList<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task<IList<Product>> GetProductByPriceWithFilter(decimal value, string filter);
    Task<Product> SaveProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task DeleteProduct(Product product);
}