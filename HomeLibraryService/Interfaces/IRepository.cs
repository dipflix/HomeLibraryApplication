using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLibraryService.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> Items { get; }

        T Get(int id);
        Task<T> GetAsync(int id, CancellationToken Cancel = default);

        T Add(T entity);
        Task<T> AddAsync(T entity, CancellationToken Cancel = default);

        void Update(T entity);
        Task UpdateAsync(T entity, CancellationToken Cancel = default);

        void Remove(int id);
        Task RemoveAsync(int id, CancellationToken Cancel = default);

        
    }
}
