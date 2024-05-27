using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class UsersRepository(WorkshopDbContext dbContext) : IUsersRepository
{
    public async Task RegisterUserAsync(User newUser)
    {
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}