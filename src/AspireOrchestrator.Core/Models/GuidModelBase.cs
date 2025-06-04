namespace AspireOrchestrator.Core.Models
{
    public abstract class GuidModelBase: Entity<Guid>
    {
        protected GuidModelBase()
        {
            Id = Guid.NewGuid();
            TenantId = 0;
        }

        protected GuidModelBase(long tenantÍd)
        {
            TenantId = tenantÍd;
            Id = Guid.NewGuid();
        }

        public long TenantId { get; set; }
        public virtual Tenant Tenant { get; set; } 

    }
}
