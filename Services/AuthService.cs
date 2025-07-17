using MongoDB.Driver;
using Inteia.Api.Configurations;
using Microsoft.Extensions.Options;
using Inteia.Api.Core;

public class AuthService : IAuthService
{
    private readonly IMongoCollection<Usuario> _usuarios;
    private readonly IMongoCollection<UsuarioLogin> _usuariosLogin;
    private readonly ITokenService _tokenService;

    public AuthService(IOptions<MongoDBSettings> settings, ITokenService tokenService)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        _usuarios = database.GetCollection<Usuario>("Usuarios");
        _usuariosLogin = database.GetCollection<UsuarioLogin>("UsuariosLogin");
        _tokenService = tokenService;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var existingLogin = await _usuariosLogin.Find(x => x.Email == request.Email).FirstOrDefaultAsync();
        if (existingLogin != null) return false;

        var usuario = new Usuario
        {
            IsActive = true
        };

        await _usuarios.InsertOneAsync(usuario);

        var usuarioLogin = new UsuarioLogin
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = true,
            CreationDate = DateTime.UtcNow
        };

        await _usuariosLogin.InsertOneAsync(usuarioLogin);
        return true;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        var login = await _usuariosLogin.Find(x => x.Email == request.Email && x.IsActive).FirstOrDefaultAsync();

        if (login == null)
            return false;

        return BCrypt.Net.BCrypt.Verify(request.Password, login.PasswordHash);
    }

    public async Task<string?> LoginAndGenerateTokenAsync(LoginRequest request)
    {
        var login = await _usuariosLogin.Find(x => x.Email == request.Email && x.IsActive).FirstOrDefaultAsync();

        if (login == null || !BCrypt.Net.BCrypt.Verify(request.Password, login.PasswordHash))
            return null;

        return _tokenService.GenerateToken(login.Email);
    }

}
