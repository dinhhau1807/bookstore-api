using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccessLayer.Base
{
    public interface ICRUD<T, K> where T : class, new()
    {
        Task<IEnumerable<T>> GetListPaging(uint pageNumber, uint pageSize);
        Task<T> GetOne(K id);

        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindByConfitionPaging(Expression<Func<T, bool>> expression, uint pageNumber, uint pageSize);

        Task<bool> Insert(T value);
        Task Update(T value);
        Task Delete(T value);
    }
}
