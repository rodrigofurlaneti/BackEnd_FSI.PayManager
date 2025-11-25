using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using FSI.PayManager.Application.Security;
using FSI.PayManager.Domain.Entities;
using FSI.PayManager.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Application.Services
{
    public sealed class AuthAppService : IAuthAppService
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthAppService(
            IRepository<User> userRepository,
            IOptions<JwtSettings> jwtOptions)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
        {
            var users = await _userRepository.FindAsync(u => u.Email == request.Email, ct);
            var user = users.SingleOrDefault();

            if (user is null)
                return null;

            if (!string.Equals(user.PasswordHash, request.Password))
                return null;

            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_jwtSettings.AccessTokenMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", user.FullName),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: nowUtc,
                expires: expires,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponseDto
            {
                AccessToken = tokenString,
                ExpiresAtUtc = expires,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };
        }
    }
}