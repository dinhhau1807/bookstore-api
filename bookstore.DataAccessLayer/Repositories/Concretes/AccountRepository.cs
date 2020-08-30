using bookstore.BussinessEnitites.Models;
using bookstore.DataAccessLayer.Base;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccessLayer.Repositories.Concretes
{
    public class AccountRepository : DALBase<Account, int>, IAccountRepository
    {
        public AccountRepository(IDbConnection connection, ISqlGenerator<Account> sqlGenerator) : base(connection, sqlGenerator)
        {
        }
    }
}
