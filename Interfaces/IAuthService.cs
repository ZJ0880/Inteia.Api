using Inteia.Api.Core;
public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest request);
    Task<bool> LoginAsync(LoginRequest request);
    Task<string?> LoginAndGenerateTokenAsync(LoginRequest request);
}
