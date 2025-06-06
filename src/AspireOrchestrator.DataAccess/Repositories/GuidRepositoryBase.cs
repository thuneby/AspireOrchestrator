using AspireOrchestrator.Core.Models;
using AspireOrchestrator.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.DataAccess.Repositories
{
    public class GuidRepositoryBase<T1>(DbContext context, ILogger<GuidRepositoryBase<T1>> logger) : IGuidRepository<T1>
        where T1 : Entity<Guid>
    {
        public Tenant Tenant { get; set; } = new() { Id = 0 };

        public IQueryable<T1> GetQueryList()
        {
            return Query();
        }

        public IEnumerable<T1> GetList(int take = 100)
        {
            var entities = Query(true).Take(take).ToList();
            return entities;
        }

        public T1? Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Passed an empty Guid");
            var result = context.Set<T1>().FirstOrDefault(x => x.Id == id);
            return result;
        }

        public void Add(T1 entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = context.Set<T1>().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                logger.LogWarning("Delete Entity ({ID}) not found", id);
                return;
            }
            context.Remove(entity);
            context.SaveChanges();
        }

        public void Update(T1 entity)
        {
            var existing = Get(entity.Id);
            if (existing == null)
                throw new ArgumentException("Entity not found");
            context.Update(entity);
            context.SaveChanges();
        }

        protected IQueryable<T1> Query(bool eager = false)
        {
            var query = context.Set<T1>().AsQueryable();
            return query;
            //return !eager ? query : _context.Model.FindEntityType(typeof(T1))?.GetNavigations().Aggregate(query, (current, property) => current.Include(property.Name));
        }
    }
}
