using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Core.OrchestratorModels;
using Microsoft.EntityFrameworkCore;

namespace AspireOrchestrator.Orchestrator.DataAccess
{
    public class OrchestratorContext(DbContextOptions<OrchestratorContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<EventEntity> EventEntity { get; set; }
        public DbSet<Flow> Flow { get; set; }
    }
}
