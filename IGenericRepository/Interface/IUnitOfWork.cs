using System;
using System.Threading.Tasks;

namespace AllInterfaces {
    public interface IUnitOfWork<T> : IDisposable where T : class , IEntity {
        IRepository<T> Repository { get; }
        Task Commit(); 
    }
}
