using System.ComponentModel.DataAnnotations;

namespace Workshop.Application.Extensions;

public class AuthenticationSettings
{
    public static string SectionName = "Authentication";
    public string JwtKey { get; set; } = default!;
    public int JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; } = default!;
    public string JwtAudience { get; set; } = default!;
}