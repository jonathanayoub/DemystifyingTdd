using Microsoft.EntityFrameworkCore;

namespace DemystifyingTdd.Api.Data
{
    public class TddContext : DbContext
    {
        public TddContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscriptionEntity>()
                .HasOne(sub => sub.Customer)
                .WithMany(cust => cust.Subscriptions)
                .HasForeignKey(sub => sub.CustomerId);
        }
    }
}
