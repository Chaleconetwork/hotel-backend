using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Library.Utils;

namespace Backend.Services
{
    public class JwtTokenGeneratorService : IJwtTokenGeneratorInterface
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGeneratorService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateJwtToken(UserDTO dto)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret ?? "")),
                algorithm: SecurityAlgorithms.HmacSha256Signature
            );

            ClaimsIdentity claims = new(new Claim[]
            {
                new Claim(type: ClaimTypes.Name, dto.Username ?? ""),
                new Claim(type: ClaimTypes.Role, dto.Rol),
                new Claim(type: ClaimTypes.NameIdentifier, dto.Id.ToString())
            });

            DateTime chileTimeZone = ChileTimeZone();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _jwtSettings.Issuer, // Emissor do token;
                IssuedAt = chileTimeZone,
                Audience = _jwtSettings.Audience,
                NotBefore = chileTimeZone,
                Expires = chileTimeZone.AddMinutes(_jwtSettings.TokenExpiryMinutes),
                Subject = claims,
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }

        private static DateTime ChileTimeZone()
        {
            // Por algum motivo inexplicável é necessário ajustar a hora por uma diferença apresentada quando publicado em produção no Azure;
            // Produção: +3 horas;
            DateTime chileTimeZone = ChileTime().AddHours(+4);

#if DEBUG
            // Dev: horário normal;
            chileTimeZone = ChileTime();
#endif

            return chileTimeZone;
        }
    }
}
