using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwyTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, 
                                 IOptions<JwtSettings> options)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = options.Value;
        }

        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var signingCredintials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience)
            };

            var securityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer, 
                                                     audience: _jwtSettings.Audience,    
                                                     expires: _dateTimeProvider.UTCNow.AddDays(_jwtSettings.ExpiryInMinutes), 
                                                     claims: claims,
                                                     signingCredentials: signingCredintials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
