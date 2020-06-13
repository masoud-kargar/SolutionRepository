using Data;

using IGenericRepository;
using IGenericRepository.Interface;

using System.Threading.Tasks;

namespace GenericRepository.Services {
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class, IEntity {
        private readonly PanelContext _context;
        public UnitOfWork(PanelContext context) {
            _context = context;
            Repository = new Repository<T>(context);
        }
        public IRepository<T> Repository { get; }
        public async Task Commit() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
