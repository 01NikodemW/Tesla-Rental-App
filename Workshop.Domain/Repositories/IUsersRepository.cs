using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface IUsersRepository
{
    Task RegisterUserAsync(User data, CancellationToken cancellationToken);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    bool CheckIfUserWithProvidedEmailInDb(string email);
}