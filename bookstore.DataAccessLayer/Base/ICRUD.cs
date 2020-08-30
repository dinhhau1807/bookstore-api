using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccessLayer.Base
{
    public interface ICRUD<T, K> where T : class, new()
    {
        IAsyncEnumerable<T> GetListPaging(uint pageNumber, uint pageSize);
        Task<T> GetOne(K id);

        IAsyncEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IAsyncEnumerable<T> FindByConfitionPaging(Expression<Func<T, bool>> expression, uint pageNumber, uint pageSize);

        Task<bool> Insert(T value);
        Task Update(T value);
        Task Delete(T value);
    }
}
