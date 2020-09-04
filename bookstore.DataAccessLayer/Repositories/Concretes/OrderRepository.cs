using bookstore.BussinessEnitites.Models;
using bookstore.DataAccessLayer.Base;
using bookstore.DataAccessLayer.Repositories.Abstracts;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bookstore.DataAccessLayer.Repositories.Concretes
{
    public class OrderRepository : DALBase<Order, string>, IOrderRepository
    {
        public OrderRepository(IDbConnection connection, ISqlGenerator<Order> sqlGenerator) : base(connection, sqlGenerator)
        {
        }
    }
}
