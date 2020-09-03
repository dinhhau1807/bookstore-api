using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookstore.API.Services;
using bookstore.BussinessEnitites.Enums;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using bookstore.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IAccountService _accountService;

        public AccountsController(IAuthenticateService authenticateService, IAccountService accountService)
        {
            _authenticateService = authenticateService;
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> GetAccounts(uint pageNumber = 1, uint pageSize = 30)
        {
            var accounts = await _accountService.GetAccounts(pageNumber, pageSize);
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _authenticateService.GetAccount(id);
            return Ok(account);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var account = await _authenticateService.GetAccount(null);
            return Ok(account);
        }

        /// <summary>
        /// Just for testing
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet("hash")]
        public IActionResult GetHash(string pass)
        {
            return Ok(BCrypt.Net.BCrypt.HashPassword(pass));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return Ok(await _authenticateService.Login(model.Username, model.Password));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterAccountDTO model)
        {
            return Ok(await _authenticateService.Register(model));
        }
    }
}
