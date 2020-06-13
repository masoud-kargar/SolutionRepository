using IGenericRepository;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data {
    public class Repository<T> : IRepository<T> where T : class , IEntity {
        DbSet<T> Table { get; set; }
        public Repository(PanelContext context) => Table = context.Set<T>();
        public async Task Add(T entity) => await Table.AddAsync(entity);
        public async Task Add(IEnumerable<T> entities) => await Table.AddRangeAsync(entities);
        public void Update(T entity) => Table.Update(entity);
        public void Update(IEnumerable<T> entities) => Table.UpdateRange(entities);
        public void Delete(T entity) => Table.Remove(entity);
        public void Delete(IEnumerable<T> entities) => Table.RemoveRange(entities);
        public async Task<IList<T>> All() => await Table.AsNoTracking().ToListAsync();
        public async Task<T> GetById(Guid Id) => await Table.SingleAsync(x => x.Id == Id);
        public IQueryable<T> where(Expression<Func<T, bool>> expression) => Table.Where<T>(expression);
        public bool where(bool v) => where(v);
        public bool where(Guid v) => where(v);
    }
}
