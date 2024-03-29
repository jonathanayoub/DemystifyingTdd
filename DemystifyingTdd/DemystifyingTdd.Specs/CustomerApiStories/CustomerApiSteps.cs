﻿using DemystifyingTdd.Api;
using DemystifyingTdd.Api.Data;
using DemystifyingTdd.Api.Models;
using DemystifyingTdd.Specs.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemystifyingTdd.Specs.CustomerApiStories
{
    [Binding]
    public class CustomerApiSteps : ApiFeatureBase
    {
        private string _customersUrl;
        private HttpResponseMessage _response;
        private TddContext _dbContext;
        private IServiceScope _scope;
        private IEnumerable<CustomerEntity> _customers;
        private IEnumerable<SubscriptionEntity> _subscriptions;
        private IEnumerable<CustomerData> _customerDataTable;
        private IEnumerable<SubscriptionData> _subscriptionDataTable;

        public CustomerApiSteps(WebApplicationFactory<Startup> webApplicationFactory)
            : base(webApplicationFactory)
        { }

        [BeforeScenario]
        public void CreateTestDatabase()
        {
            _scope = WebApplicationFactory.Server.Host.Services.CreateScope();
            var scopedServices = _scope.ServiceProvider;
            _dbContext = scopedServices.GetRequiredService<TddContext>();
        }

        [Given(@"I have the following customer data")]
        public void GivenIHaveTheFollowingCustomerData(Table table)
        {
            _customerDataTable = table.CreateSet<CustomerData>();
            _customers = BuildCustomerData(_customerDataTable);
        }

        private IEnumerable<CustomerEntity> BuildCustomerData(
            IEnumerable<CustomerData> customerDataTable)
        {
            return customerDataTable.Select(customerData => new CustomerEntity
            {
                Id = Guid.NewGuid(),
                FirstName = customerData.FirstName,
                LastName = customerData.LastName,
                Address = customerData.Address,
                City = customerData.City,
                State = customerData.State,
                ZipCode = customerData.ZipCode,
                FavoriteColor = customerData.FavoriteColor
            }).ToList();
        }

        [Given(@"the following subscriptions")]
        public void GivenTheFollowingSubscriptions(Table table)
        {
            _subscriptionDataTable = table.CreateSet<SubscriptionData>();
            _subscriptions = BuildSubscriptionData(_subscriptionDataTable);

            AssociateCustomerSubscriptions();

            _dbContext.Customers.AddRange(_customers);
            _dbContext.SaveChanges();
        }

        private IEnumerable<SubscriptionEntity> BuildSubscriptionData(
            IEnumerable<SubscriptionData> subscriptionDataTable)
        {
            return subscriptionDataTable.Select(subscriptionData =>
            {
                var matchingCustomer =
                    _customers.Single(c =>
                        subscriptionData.Customer
                        == $"{c.FirstName} {c.LastName}");
                return new SubscriptionEntity
                {
                    Id = Guid.NewGuid(),
                    Title = subscriptionData.Title,
                    CustomerId = matchingCustomer.Id,
                    Customer = matchingCustomer
                };
            }).ToList();
        }

        private void AssociateCustomerSubscriptions()
        {
            foreach (var customerEntity in _customers)
            {
                var matchingSubscriptions = _subscriptions
                    .Where(s => s.CustomerId == customerEntity.Id);
                customerEntity.Subscriptions = matchingSubscriptions.ToList();
            }
        }

        [Given(@"a customers url of ""(.*)""")]
        public void GivenACustomersUrlOf(string customersUrl)
        {
            _customersUrl = customersUrl;
        }

        [When(@"I request the customer")]
        public void WhenIRequestTheCustomer()
        {
            var uri = new Uri(_customersUrl, UriKind.Relative);
            _response = HttpClient.GetAsync(uri).Result;
        }

        [Then(@"Ok status code is returned")]
        public void ThenOkStatusCodeIsReturned()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the customer data is returned")]
        public void ThenTheCustomerDataIsReturned()
        {
            var result = _response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            var expectedResult = BuildExpectedResult();
            result.Should().BeEquivalentTo(expectedResult);
        }

        private IList<Customer> BuildExpectedResult()
        {
            return _customers.Select(customer =>
            {
                var matchingSubscriptions = _subscriptions
                    .Where(sub => sub.CustomerId == customer.Id)
                    .Select(sd => new Subscription
                    {
                        Title = sd.Title
                    }
                    ).ToList();
                return new Customer
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    FavoriteColor = customer.FavoriteColor,
                    Subscriptions = matchingSubscriptions
                };
            }).ToList();
        }

        [AfterScenario]
        public void Teardown()
        {
            _scope.Dispose();
        }
    }
}
