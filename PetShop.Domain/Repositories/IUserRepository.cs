using PetShop.Domain.Entities;

namespace PetShop.Domain.Repositories;

public interface IUserRepository
{
    Task<IList<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<User> SaveUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(User user);
}