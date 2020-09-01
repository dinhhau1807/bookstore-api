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
        Task<AccountDTO> GetAccount(int? id);
        Task<Account> GetAccountByUsernameOrEmail(string usernameOrEmail);
    }
}
