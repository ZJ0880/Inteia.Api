using Inteia.Api.Configurations;
using Inteia.Api.DTOs;
using Inteia.Api.Models;
using Inteia.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace Inteia.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMongoCollection<Usuario> _usuarios;
        private readonly ITokenService _tokenService;

        public AuthService(IOptions<MongoDBSettings> mongoSettings, ITokenService tokenService)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _usuarios = database.GetCollection<Usuario>("Usuarios");
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _usuarios.Find(u => u.Correo == request.Correo).FirstOrDefaultAsync();
            if (existingUser != null) return false;

            var nuevoUsuario = new Usuario
            {
                Nombre = request.Nombre,
                Ciudad = request.Ciudad,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Correo = request.Correo,
                Web = request.Web,
                
            };

            await _usuarios.InsertOneAsync(nuevoUsuario);
            return true;
        }

        public async Task<string?> LoginAndGenerateTokenAsync(LoginRequest request)
        {
            var usuario = await _usuarios.Find(u => u.Correo == request.Correo).FirstOrDefaultAsync();

            if (usuario == null || usuario.Contrasena != HashPassword(request.Contrasena))
                return null;

            return _tokenService.GenerateToken(usuario);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
