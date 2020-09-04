using bookstore.BussinessEnitites.Models;
using bookstore.DataAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookstore.DataAccessLayer.Repositories.Abstracts
{
    public interface IOrderRepository : ICRUD<Order, string>
    {
    }
}
