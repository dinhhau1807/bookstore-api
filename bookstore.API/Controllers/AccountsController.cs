using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bookstore.API.Services;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize]
        public async Task<IActionResult> GetAccount()
        {
            var account = await _accountService.GetAccount(2);
            return Ok(account);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser([FromServices] IHttpContextCurrentUser current)
        {
            var user = new[] { current.CurrentUserId.ToString(), current.CurrentUsername };
            var account = await _accountService.GetAccount(2);
            return Ok(new { user, account });
        }

        [HttpPost]
        public IActionResult Authenticate()
        {
            return Ok(_authenticateService.Authenticate("admin", ""));
        }
    }
}
