﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDemo.Models;

namespace WebDemo.Services
{
    public class JwtTokenService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public JwtTokenService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public const string Key = "1A8655BDB34EB679A4FE1ABFA14EB";
            
        public string GenarateKey(User user)
        {
            var sessionId = "";
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(sessionId, null, claims, null, DateTime.UtcNow.AddMinutes(120), creds);
            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(jwtToken);
            return accessToken;
        }
    }
}