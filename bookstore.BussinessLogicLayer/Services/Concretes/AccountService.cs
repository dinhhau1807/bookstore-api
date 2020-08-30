using bookstore.BussinessEnitites.Models;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Concretes
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> GetAccount(int? id)
        {
            Account user = null;
            if (id != null)
            {
                user = await _accountRepository.GetOne(id.Value);
            }

            return user;
        }
    }
}
