using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface IUsersRepository
{
    Task RegisterUserAsync(User data);
    Task<User?> GetUserByEmailAsync(string email);
    bool CheckIfUserWithProvidedEmailInDb(string email);
}