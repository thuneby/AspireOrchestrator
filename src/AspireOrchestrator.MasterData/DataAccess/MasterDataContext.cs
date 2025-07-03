using Microsoft.EntityFrameworkCore;
using AspireOrchestrator.MasterData.Models;

namespace AspireOrchestrator.MasterData.DataAccess
{
    public class MasterDataContext: DbContext
    {
        public MasterDataContext(DbContextOptions<MasterDataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Agreement> Agreement { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<LaborAgreement> LaborAgreement { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Policy> Policy { get; set; }
    }
}
