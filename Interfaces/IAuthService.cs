using System.Threading.Tasks;
using Inteia.Api.DTOs;


namespace Inteia.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<string?> LoginAndGenerateTokenAsync(LoginRequest request);
    }
}


