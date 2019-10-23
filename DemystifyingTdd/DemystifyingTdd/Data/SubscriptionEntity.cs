using System;

namespace DemystifyingTdd.Api.Data
{
    public class SubscriptionEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
    }
}
