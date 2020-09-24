using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IMDB.Movies.API.Application.Models;
using IMDB.Movies.API.Data;
using IMDB.Movies.API.Indentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IMDB.Movies.API.Application.Services
{
    public class JwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public AccessTokenDTO GenerateToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim("UserId", user.Id));
            claims.Add(new Claim("UserEmail", user.Email));


            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
            );

            return new AccessTokenDTO(
                new JwtSecurityTokenHandler().WriteToken(token),
                roles,
                user?.UserName);
        }
    }
}