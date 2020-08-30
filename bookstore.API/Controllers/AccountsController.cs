using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bookstore.API.Services;
using bookstore.BussinessLogicLayer.Services.Abstracts;
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
        public async Task<IActionResult> GetAccount()
        {
            var account = await _accountService.GetAccount(2);
            return Ok(account);
        }

        [HttpPost]
        public IActionResult Login()
        {
            return Ok(_authenticateService.Authenticate("demo", "demo"));
        }
    }
}
