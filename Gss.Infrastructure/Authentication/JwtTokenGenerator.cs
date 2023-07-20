using Gss.Application.Common.Interfaces.Authentication;
using Gss.Application.Common.Interfaces.Services;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Gss.Infrastructure.Authentication
{
    class JwtTokenGenerator : IJwtTokenGenerator

    {

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }

        private static SecurityKey GenerateSecureKey(int keySize)
        {
            var key = new byte[keySize / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            return new SymmetricSecurityKey(key);
        }


        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var securitykey = GenerateSecureKey(256);

            var signingCredentials = new SigningCredentials
           (
              // new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
               securitykey,
               SecurityAlgorithms.HmacSha256
           );

            var claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
            };

            var securityToken = new JwtSecurityToken(
             issuer: _jwtSettings.Issuer,
             audience: _jwtSettings.Audience,
             expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
             claims: claims,
             signingCredentials: signingCredentials
            ); ;

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }


    }
}
