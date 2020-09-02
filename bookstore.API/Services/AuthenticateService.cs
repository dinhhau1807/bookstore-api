using AutoMapper;
using bookstore.BussinessEnitites.Models;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using bookstore.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.API.Services
{
    public interface IAuthenticateService
    {
        Task<ApiResponse<AccountDTO>> Login(string username, string password);
        Task<ApiResponse<AccountDTO>> Register(RegisterAccountDTO registerAccountDTO);
        Task<ApiResponse<AccountDTO>> GetAccount(int? id);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;

        public AuthenticateService(ILogger logger, IMapper mapper, IConfiguration configuration, IAccountService accountService)
        {
            _logger = logger;
            _mapper = mapper;
            _config = configuration;
            _accountService = accountService;
        }

        public async Task<ApiResponse<AccountDTO>> GetAccount(int? id)
        {
            AccountDTO account = await _accountService.GetAccount(id);
            if (account == null)
            {
                throw new AppException(StatusCodes.Status404NotFound, "Account not found.");
            }

            return ApiResponse<AccountDTO>.Ok(account);
        }

        public async Task<ApiResponse<AccountDTO>> Login(string username, string password)
        {
            var account = await _accountService.GetAccountByUsernameOrEmail(username);

            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.HashPassword))
            {
                throw new AppException(StatusCodes.Status401Unauthorized, "Incorret username or password!");
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim("Id", account.Id.ToString()),
                new Claim("Username", account.Username),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.GivenName, account.Name),
                new Claim(ClaimTypes.Role, account.Role),
            });

            var (tokenDescriptor, token) = GenerateSecurityToken(identity);

            var accountDTO = _mapper.Map<Account, AccountDTO>(account);
            accountDTO.AuthInformation = new AuthInformation
            {
                Token = token,
                Expires = tokenDescriptor.Expires,
            };

            _logger.Information($"User {account.Username} logged on {TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time")}.");
            return ApiResponse<AccountDTO>.Ok(accountDTO);
        }

        public async Task<ApiResponse<AccountDTO>> Register(RegisterAccountDTO registerAccountDTO)
        {
            var account = await _accountService.CreateAccount(registerAccountDTO);

            _logger.Information($"Created user {account.Username} on {TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time")}.");
            return await Login(account.Username, registerAccountDTO.Password);
        }

        #region Helpers
        private (SecurityTokenDescriptor tokenDescriptor, string token) GenerateSecurityToken(ClaimsIdentity identity)
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

            return (tokenDescriptor, tokenHandler.WriteToken(token));
        }
        #endregion
    }
}
