using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccessLayer.Base
{
    public class DALBase<T, K> : DapperRepository<T>, ICRUD<T, K> where T : class, new()
    {
        public DALBase(IDbConnection connection, ISqlGenerator<T> sqlGenerator) : base(connection, sqlGenerator)
        {
        }

        public Task Delete(T value)
        {
            var result = DeleteAsync(value);
            return result;
        }

        public IAsyncEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var results = FindAllAsync(expression);
            return results as IAsyncEnumerable<T>;
        }

        public IAsyncEnumerable<T> FindByConfitionPaging(Expression<Func<T, bool>> expression, uint pageNumber, uint pageSize)
        {
            var limit = pageSize;
            var offset = (pageNumber - 1) * pageSize;

            var results = SetLimit(limit, offset).FindAllAsync(expression);
            return results as IAsyncEnumerable<T>;
        }

        public IAsyncEnumerable<T> GetListPaging(uint pageNumber, uint pageSize)
        {
            var limit = pageSize;
            var offset = (pageNumber - 1) * pageSize;

            var results = SetLimit(limit, offset).FindAllAsync();
            return results as IAsyncEnumerable<T>;
        }

        public Task<T> GetOne(K id)
        {
            var result = FindByIdAsync(id);
            return result;
        }

        public Task Update(T value)
        {
            var result = UpdateAsync(value);
            return result;
        }

        Task<bool> ICRUD<T, K>.Insert(T value)
        {
            var result = InsertAsync(value);
            return result;
        }
    }
}
