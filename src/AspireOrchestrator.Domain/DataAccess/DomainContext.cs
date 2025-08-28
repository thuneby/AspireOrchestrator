using AspireOrchestrator.Core.Models;
using AspireOrchestrator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class DomainContext(DbContextOptions<DomainContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetail { get; set; }

        public DbSet<Deposit> Deposit { get; set; }

        public DbSet<PostingJournal> PostingJournal { get; set; }
        public DbSet<PostingEntry> PostingEntry { get; set; }
    }
}
