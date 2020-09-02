using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccessLayer.Base
{
    public interface ICRUD<T, K> where T : class, new()
    {
        Task<IEnumerable<T>> GetListPaging(uint pageNumber, uint pageSize, Expression<Func<T, object>> orderExpression);
        Task<T> GetOne(K id);

        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindByConfitionPaging(uint pageNumber, uint pageSize, Expression<Func<T, object>> orderExpression, Expression<Func<T, bool>> expression);

        Task<int> CountAll();
        Task<int> CountByCondition(Expression<Func<T, bool>> expression);

        Task<bool> Insert(T value);
        Task Update(T value);
        Task Delete(T value);
    }
}
