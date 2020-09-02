using AutoMapper;
using bookstore.BussinessEnitites.Enums;
using bookstore.BussinessEnitites.Models;
using bookstore.BussinessLogicLayer.Services.Abstracts;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using bookstore.Shared.Helpers;
using bookstore.Shared.Services;
using Microsoft.AspNetCore.Http;
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

        public async Task<Account> CreateAccount(RegisterAccountDTO registerAccountDTO)
        {
            var account = _mapper.Map<Account>(registerAccountDTO);
            account.HashPassword = BCrypt.Net.BCrypt.HashPassword(registerAccountDTO.Password);
            account.Role = RoleNames.User;
            account.CreatedAt = DateTime.UtcNow;

            var isExistedAcc = (await _accountRepository.FindByCondition(a => a.Email == account.Email || a.Username == account.Username))
                .FirstOrDefault();
            if (isExistedAcc != null) throw new AppException(StatusCodes.Status400BadRequest, "Account has already existed!");

            var result = await _accountRepository.Insert(account);
            if (!result)
            {
                throw new AppException(StatusCodes.Status500InternalServerError, "Something went wrong, cannot register account!");
            }

            return account;
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

        public async Task<ApiResponse<IEnumerable<AccountDTO>>> GetAccounts(uint pageNumber, uint pageSize)
        {
            var accounts = await _accountRepository.GetListPaging(pageNumber, pageSize, o => o.Id);
            var total = await _accountRepository.CountAll();
            var accountDTOs = _mapper.Map<IEnumerable<AccountDTO>>(accounts);

            var result = ApiResponse<IEnumerable<AccountDTO>>.Ok(accountDTOs);
            result.ExtraData = new { total };

            return result;
        }
    }
}
