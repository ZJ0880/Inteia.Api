// Services/Interfaces/ITokenService.cs
namespace Inteia.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string email);
    }
}
