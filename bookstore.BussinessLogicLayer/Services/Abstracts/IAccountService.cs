using bookstore.BussinessEnitites.Models;
using bookstore.DataTransferObject.DTOs;
using bookstore.Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Abstracts
{
    public interface IAccountService
    {
        Task<ApiResponse<IEnumerable<AccountDTO>>> GetAccounts(uint pageNumber, uint pageSize);
        Task<AccountDTO> GetAccount(int? id);
        Task<Account> GetAccountByUsernameOrEmail(string usernameOrEmail);
        Task<Account> CreateAccount(RegisterAccountDTO registerAccountDTO);
    }
}
