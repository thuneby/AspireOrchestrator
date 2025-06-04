namespace AspireOrchestrator.Core.Models
{
    public abstract class TenantModelBase: LongEntityBase
    {
        /// <summary>
        /// Parameterless constructor for Entity Framework
        /// </summary>
        protected TenantModelBase() 
        { 
        }

        protected TenantModelBase(long tenantId)
        {
            TenantId = tenantId;
        }

        public long TenantId { get; set; } 

        //[ForeignKey("TenantId")]
        //public Tenant Tenant { get; set; }
    }
}
