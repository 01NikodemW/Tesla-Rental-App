namespace Workshop.Application.Users.Dtos;

public class LoginResponse(string token)
{
    public string Token { get; set; } = token;
}