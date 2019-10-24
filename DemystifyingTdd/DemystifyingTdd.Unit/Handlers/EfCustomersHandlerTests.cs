using AutoFixture;
using AutoMapper;
using DemystifyingTdd.Api.Data;
using DemystifyingTdd.Api.Handlers;
using DemystifyingTdd.Api.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DemystifyingTdd.Unit.Handlers
{
    public class GivenARequestForAllCustomers
    {
        [Fact]
        public void WhenTheRequestIsMade_ThenCustomersAreReturnedFromTheRepository()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var dummyCustomerEntities = fixture.CreateMany<CustomerEntity>().ToList();
            var options = new DbContextOptionsBuilder<TddContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var stubContext = new TddContext(options.Options);
            stubContext.Customers.AddRange(dummyCustomerEntities);
            stubContext.SaveChanges();

            var dummyApiCustomers = fixture.CreateMany<Customer>().ToList();
            var stubMapper = new Mock<IMapper>();
            stubMapper
                .Setup(m =>
                    m.Map<IList<Customer>>(It.IsAny<IEnumerable<CustomerEntity>>()))
                .Returns(dummyApiCustomers);

            var sut = new EfCustomersHandler(stubContext, stubMapper.Object);

            var result = sut.GetAll();
            result.Should().BeEquivalentTo(dummyApiCustomers);
        }
    }
}
