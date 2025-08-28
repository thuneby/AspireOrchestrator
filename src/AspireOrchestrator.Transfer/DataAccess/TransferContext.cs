using AspireOrchestrator.Transfer.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireOrchestrator.Transfer.DataAccess
{
    public class TransferContext(DbContextOptions<TransferContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<TransferBase> Transfer { get; set; } 
    }
}
