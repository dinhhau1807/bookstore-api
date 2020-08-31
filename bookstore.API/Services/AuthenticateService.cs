using bookstore.BussinessLogicLayer.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.API.Services
{
    public interface IAuthenticateService
    {
        string Authenticate(string username, string password);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public AuthenticateService(ILogger logger, IConfiguration configuration, IAccountService accountService)
        {
            _config = configuration;
            _logger = logger;
            _accountService = accountService;
        }

        public string Authenticate(string username, string password)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim("Id", "123"),
                new Claim("Username", username)
            });

            return GenerateSecurityToken(identity);
        }

        #region Helpers
        private string GenerateSecurityToken(ClaimsIdentity identity)
        {
            var secret = _config.GetSection("JwtConfig").GetSection("Secret").Value;
            var expDate = _config.GetSection("JwtConfig").GetSection("ExpirationInMinutes").Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
