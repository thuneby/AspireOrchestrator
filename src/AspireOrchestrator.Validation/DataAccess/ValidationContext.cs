using Microsoft.EntityFrameworkCore;
using AspireOrchestrator.Validation.Models;

namespace AspireOrchestrator.Validation.DataAccess
{
    public class ValidationContext(DbContextOptions<ValidationContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<ValidationError> ValidationError { get; set; }
    }
}
