using bookstore.BussinessEnitites.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.BussinessLogicLayer.Services.Abstracts
{
    public interface IAccountService
    {
        Task<Account> GetAccount(int? id);
    }
}
