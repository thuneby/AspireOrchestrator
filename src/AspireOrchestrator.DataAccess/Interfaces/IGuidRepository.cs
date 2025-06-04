using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.DataAccess.Interfaces
{
    public interface IGuidRepository<T>
    {
        Tenant Tenant { set; }
        IEnumerable<T> GetList(int take);
        IQueryable<T> GetQueryList();
        T? Get(Guid id);
        void Add(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}
