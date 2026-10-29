namespace SahinMetal.Api.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(string username, string role);
}
