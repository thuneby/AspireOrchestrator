using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspireOrchestrator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireOrchestrator.Domain.DataAccess
{
    public class DomainContext(DbContextOptions<DomainContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<ReceiptDetail> ReceiptDetail { get; set; }

    }
}
