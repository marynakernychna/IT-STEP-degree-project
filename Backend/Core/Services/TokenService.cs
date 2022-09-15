using Core.DTO.Authentication;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.CustomService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Core.Interfaces;
using Core.Specifications;

namespace Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public TokenService(
                IOptions<JwtOptions> jwtOptions,
                IRepository<RefreshToken> refreshTokenRepository)
        {
            _jwtOptions = jwtOptions;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task DeleteRefreshTokenAsync(
            string refreshToken)
        {
            var token = await _refreshTokenRepository.SingleOrDefaultAsync(
                new RefreshTokenSpecification.GetByToken(refreshToken));

            ExtensionMethods.RefreshTokenNullCheck(token);

            await _refreshTokenRepository.DeleteAsync(token);
        }

        public async Task<UserAutorizationDTO> GenerateForUserAsync(
            User user, string userRole)
        {
            var claims = SetClaims(user, userRole);
            var accessToken = CreateAccessToken(claims);
            var refreshToken = await CreateRefreshTokenAsync(user.Id);

            user.RefreshTokens.Add(refreshToken);

            var tokens = new UserAutorizationDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };

            return tokens;
        }

        private string CreateAccessToken(
            IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.Value.LifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<RefreshToken> CreateRefreshTokenAsync(
            string userId)
        {
            var randomBytes = new byte[32];

            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            rngCryptoServiceProvider.GetBytes(randomBytes);

            var refreshToken = Convert.ToBase64String(randomBytes);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = userId
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return refreshTokenEntity;
        }

        private static IEnumerable<Claim> SetClaims(
            User user, string userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("role", userRole) // for further work on the frontend
            };

            return claims;
        }
    }
}
