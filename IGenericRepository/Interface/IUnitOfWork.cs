using System;
using System.Threading.Tasks;

namespace IGenericRepository.Interface {
    public interface IUnitOfWork<T> : IDisposable where T : class , IEntity {
        IRepository<T> Repository { get; }
        Task Commit(); 
    }
}
