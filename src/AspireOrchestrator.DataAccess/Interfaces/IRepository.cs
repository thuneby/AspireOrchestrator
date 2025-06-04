using AspireOrchestrator.Core.Models;

namespace AspireOrchestrator.DataAccess.Interfaces
{
    public interface IRepository<T1, T2>
        where T1 : Entity<T2>
    {
        IEnumerable<T1> GetAll(int take, int skip);
        T1? Get(T2 id);
        void Add(T1 entity);
        void Delete(T2 id);
        void Update(T1 entity);
    }
}
