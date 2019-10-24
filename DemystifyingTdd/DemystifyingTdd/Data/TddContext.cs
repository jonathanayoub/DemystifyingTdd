using Microsoft.EntityFrameworkCore;

namespace DemystifyingTdd.Api.Data
{
    public class TddContext : DbContext
    {
        public TddContext(DbContextOptions<TddContext> options)
            : base(options)
        { }

        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<SubscriptionEntity> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscriptionEntity>()
                .HasOne(sub => sub.Customer)
                .WithMany(cust => cust.Subscriptions)
                .HasForeignKey(sub => sub.CustomerId);
        }
    }
}
