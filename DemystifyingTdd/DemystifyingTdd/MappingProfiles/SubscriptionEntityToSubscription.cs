using AutoMapper;
using DemystifyingTdd.Api.Data;
using DemystifyingTdd.Api.Models;

namespace DemystifyingTdd.Api.MappingProfiles
{
    public class SubscriptionEntityToSubscription : Profile
    {
        public SubscriptionEntityToSubscription()
        {
            CreateMap<SubscriptionEntity, Subscription>();
        }
    }
}
