using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PrimerParcial.Models;
using PrimerParcial.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace PrimerParcial.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly TokenSettings _tokenSettings;

        public AuthenticationService(IUserService userService, IOptions<TokenSettings> tokenSettings)
        {
            _userService = userService;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var user = await _userService.GetByCredentials(username, password);
            if (user is null)
            {
                return false;
            }
            return true;
        }
        public async Task<string> GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            bool isAdmin = user.Id == 1;
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Fullname),
                    new Claim(ClaimTypes.Role, isAdmin ? "admin" : "normal")
                }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpirationMinutos),
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = credentials
            };
            var token = new JwtSecurityTokenHandler().CreateToken(descriptor);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
