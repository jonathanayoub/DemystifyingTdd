using System;
using System.Collections.Generic;

namespace DemystifyingTdd.Api.Data
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string FavoriteColor { get; set; }
        public List<SubscriptionEntity> Subscriptions { get; set; }

        public CustomerEntity()
        {
            Subscriptions = new List<SubscriptionEntity>();
        }
    }
}
