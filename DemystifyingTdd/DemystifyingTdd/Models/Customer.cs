using System;
using System.Collections.Generic;

namespace DemystifyingTdd.Api.Models
{
    public class Customer
    {
        public Customer()
        {
            Subscriptions = new List<Subscription>();
        }

        public Guid Id { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string FavoriteColor { get; set; }
    }
}
