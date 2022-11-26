using Microsoft.EntityFrameworkCore;
using PetShop.Database.Context;
using PetShop.Domain.Entities;
using PetShop.Domain.Repositories;

namespace PetShop.Database.Repositories;


public class UserRepository : IUserRepository
{
    private readonly PetShopContext context;

    public UserRepository(PetShopContext context)
    {
        this.context = context;
    }
    public async Task<IList<User>> GetAll() => await context.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetById(int id) => await context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<User?> GetByEmail(string email) =>
        await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User> SaveUser(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }
    public async Task DeleteUser(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}