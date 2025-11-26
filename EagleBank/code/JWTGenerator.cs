using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EagleBank.code
{
    public class JWTGenerator : IJWTGenerator
    {
        private readonly JWTSettings _settings;
        public JWTGenerator(IOptions<JWTSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }
        public TokenResponse GenerateToken(string id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _settings.ValidIssuer,
                audience: _settings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
            }
        }
    
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}
