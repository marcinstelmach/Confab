namespace Confab.Shared.Infrastructure.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Confab.Shared.Abstractions;
    using Confab.Shared.Abstractions.Auth;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using JsonWebToken = Confab.Shared.Abstractions.Auth.JsonWebToken;
    using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

    public class AuthManager : IAuthManager
    {
        private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = new();
        private readonly AuthSettings _authSettings;
        private readonly IDateTimeService _dateTimeService;

        public AuthManager(IOptions<AuthSettings> authSettings, IDateTimeService dateTimeService)
        {
            if (authSettings.Value.SecretKey is null)
            {
                throw new InvalidOperationException("Issuer signing key not set.");
            }

            _authSettings = authSettings.Value;
            _dateTimeService = dateTimeService;
        }

        public JsonWebToken GenerateToken(Guid userId, string email, string role,
            IDictionary<string, IEnumerable<string>> claims = null)
        {
            var utcNow = _dateTimeService.GetDateTimeUtcNow();
            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, GetJwtDate(utcNow), ClaimValueTypes.Integer64),
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (claims?.Any() is true)
            {
                foreach (var (claim, values) in claims)
                {
                    jwtClaims.AddRange(values.Select(value => new Claim(claim, value)));
                }
            }

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey)), SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                _authSettings.Issuer,
                _authSettings.Audience,
                claims: jwtClaims,
                notBefore: utcNow,
                expires: utcNow.AddMinutes(_authSettings.AccessTokenLifetimeMinutes),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new JsonWebToken
            {
                AccessToken = token,
                RefreshToken = string.Empty,
                Expires = utcNow.AddMinutes(_authSettings.AccessTokenLifetimeMinutes),
                Id = userId,
                Email = email,
                Role = role ?? string.Empty,
                Claims = claims ?? EmptyClaims
            };
        }

        private static string GetJwtDate(DateTime dateTime)
            => EpochTime.GetIntDate(dateTime.ToUniversalTime()).ToString(CultureInfo.InvariantCulture);
    }
}