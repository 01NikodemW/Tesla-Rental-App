using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class UsersRepository(WorkshopDbContext dbContext) : IUsersRepository
{
    public async Task RegisterUserAsync(User newUser, CancellationToken cancellationToken)
    {
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        return user;
    }


    public bool CheckIfUserWithProvidedEmailInDb(string email)
    {
        return dbContext.Users.Any(u => u.Email == email);
    }
}