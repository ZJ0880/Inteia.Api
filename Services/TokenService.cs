using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Inteia.Api.Services.Interfaces;
using Inteia.Api.Configurations;

namespace Inteia.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Constructor que inyecta las configuraciones de JWT.
        /// </summary>
        /// <param name="options">Configuraciones cargadas desde appsettings.</param>
        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Genera un token JWT con las credenciales del usuario.
        /// </summary>
        /// <param name="email">Email del usuario autenticado.</param>
        /// <param name="userId">ID del usuario en la base de datos.</param>
        /// <returns>Token JWT firmado como string.</returns>
        public string GenerateToken(string email, string userId)
        {
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
                throw new InvalidOperationException("La clave secreta JWT no está configurada.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("userId", userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(string email)
        {
            throw new NotImplementedException();
        }
    }
}
