using AutoMapper;
using bookstore.BussinessEnitites.Models;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using bookstore.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Concretes
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextCurrentUser _httpContextCurrentUser;

        public AccountService(IMapper mapper, IAccountRepository accountRepository, IHttpContextCurrentUser httpContextCurrentUser)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _httpContextCurrentUser = httpContextCurrentUser;
        }

        public async Task<AccountDTO> GetAccount(int? id)
        {
            Account account;

            if (id != null)
            {
                account = await _accountRepository.GetOne(id.Value);
            }
            else
            {
                account = await _accountRepository.GetOne(_httpContextCurrentUser.CurrentUserId.Value);
            }

            var accountDTO = _mapper.Map<Account, AccountDTO>(account);
            return accountDTO;
        }

        public async Task<Account> GetAccountByUsernameOrEmail(string usernameOrEmail)
        {
            var account = (await _accountRepository
                .FindByCondition(a => a.Username == usernameOrEmail || a.Email == usernameOrEmail))
                .FirstOrDefault();
            return account;
        }
    }
}
