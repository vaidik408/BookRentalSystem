using BRS.Entities;
using BRS.Model;
using BRS.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BRS.Services
{
    public class AuthorizationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(IConfiguration configuration, ILogger<AuthorizationService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string Generate(UserDto userDto)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    
                    new Claim(ClaimTypes.Email, userDto.UserEmail),
                    new Claim(ClaimTypes.Role, userDto.RoleId.ToString())
                };

                var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating JWT token.");
                throw; 
            }
        }
    }
}
