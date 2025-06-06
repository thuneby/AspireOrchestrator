using AspireOrchestrator.Core.Models;
using AspireOrchestrator.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspireOrchestrator.DataAccess.Repositories
{
    public class ModelRepositoryBase<T1>(DbContext context, ILogger<ModelRepositoryBase<T1>> logger)
        : IRepository<T1, long>
        where T1 : Entity<long>
    {
        public IEnumerable<T1> GetAll(int take = 100, int skip = 0)
        {
            var entities = context.Set<T1>().AsQueryable().Skip(skip).Take(take).ToList();
            return entities;
        }

        public T1? Get(long id)
        {
            if (id == 0)
                return null;
            var result = context.Set<T1>().FirstOrDefault(x => x.Id == id);
            return result;
        }

        public void Add(T1 entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(long id)
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
            var existingEntity = context.Set<T1>().Any(x => x.Id == entity.Id);
            if (!existingEntity)
            {
                logger.LogWarning("Update Entity ({ID}) not found", entity.Id); 
                return;
            }
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
