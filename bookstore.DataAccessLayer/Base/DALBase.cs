using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static MicroOrm.Dapper.Repositories.SqlGenerator.Filters.OrderInfo;

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

        public Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var results = FindAllAsync(expression);
            return results;
        }

        public Task<IEnumerable<T>> FindByConfitionPaging(uint pageNumber, uint pageSize, Expression<Func<T, object>> orderExpression, Expression<Func<T, bool>> expression)
        {
            var limit = pageSize;
            var offset = (pageNumber - 1) * pageSize;

            var results = SetOrderBy<T>(SortDirection.ASC, orderExpression).SetLimit(limit, offset).FindAllAsync(expression);
            return results;
        }

        public Task<IEnumerable<T>> GetListPaging(uint pageNumber, uint pageSize, Expression<Func<T, object>> orderExpression)
        {
            var limit = pageSize;
            var offset = (pageNumber - 1) * pageSize;

            var results = SetOrderBy<T>(SortDirection.ASC, orderExpression).SetLimit(limit, offset).FindAllAsync();
            return results;
        }

        public Task<int> CountAll()
        {
            return CountAsync();
        }

        public Task<int> CountByCondition(Expression<Func<T, bool>> expression)
        {
            return CountAsync(expression);
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
