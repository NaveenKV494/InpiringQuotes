using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspiringQuotes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InspiringQuotes.Data.DBContext
{
    public class QuoteDbContext: DbContext
    {
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options) { }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>().HasQueryFilter(q => !q.IsDeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Quote>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    // Convert Delete operation to a soft delete
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
