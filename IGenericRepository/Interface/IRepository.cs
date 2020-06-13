using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IGenericRepository {
    public interface IRepository<T> where T : class ,IEntity{
        Task Add(T entity);
        Task Add(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        Task<IList<T>> All();
        Task<T> GetById(Guid Id);
        IQueryable<T> where(Expression<Func<T, bool>> expression);
        bool where(bool v);
        bool where(Guid v);
    }
}
