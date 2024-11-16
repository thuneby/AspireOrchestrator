using AspireOrchestrator.Core.Models;
using AspireOrchestrator.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.DataAccess.Repositories
{
    public class GuidRepositoryBase<T1> : IGuidRepository<T1>
        where T1 : Entity<Guid>
    {
        private readonly DbContext _context; 
        private readonly ILogger<GuidRepositoryBase<T1>> _logger;

        public GuidRepositoryBase(DbContext context, ILogger<GuidRepositoryBase<T1>> logger, Tenant tenant)
        {
            _context = context;
            _logger = logger;
            Tenant = tenant;
        }

        public Tenant Tenant { get; set; }

        public IQueryable<T1> GetQueryList()
        {
            return Query();
        }

        public IEnumerable<T1> GetList(int take = 1000, int skip = 0)
        {
            var entities = Query(true).Skip(skip).Take(take).ToList();
            return entities;
        }

        public T1? Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Passed an empty Guid");
            var result = _context.Set<T1>().FirstOrDefault(x => x.Id == id);
            return result;
        }

        public void Add(T1 entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.Set<T1>().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogWarning("Delete Entity ({ID}) not found", id);
                return;
            }
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T1 entity)
        {
            var existing = Get(entity.Id);
            if (existing == null)
                throw new ArgumentException("Entity not found");
            _context.Update(entity);
            _context.SaveChanges();
        }

        protected IQueryable<T1> Query(bool eager = false)
        {
            var query = _context.Set<T1>().AsQueryable();
            return query;
            //return !eager ? query : _context.Model.FindEntityType(typeof(T1))?.GetNavigations().Aggregate(query, (current, property) => current.Include(property.Name));
        }
    }
}
