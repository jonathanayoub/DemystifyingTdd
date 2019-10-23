using AutoFixture;
using DemystifyingTdd.Api.Controllers;
using DemystifyingTdd.Api.Handlers;
using DemystifyingTdd.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DemystifyingTdd.Unit.Controllers
{
    public class GivenARequestForCustomersInTheSystem
    {
        private List<Customer> _dummyCustomers;
        private CustomersController _sut;

        [Fact]
        public void WhenTheRequestIsMade_ThenOkStatusIsReturned()
        {
            Setup();

            var result = _sut.GetCustomers();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void WhenTheRequestIsMade_ThenTheCustomerDataIsReturned()
        {
            Setup();

            var result = _sut.GetCustomers();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(_dummyCustomers);
        }

        private void Setup()
        {
            var fixture = new Fixture();
            _dummyCustomers = fixture.CreateMany<Customer>().ToList();
            _sut = BuildSut(_dummyCustomers);
        }

        private CustomersController BuildSut(IList<Customer> customers)
        {
            var stubCustomersHandler = new Mock<ICustomersHandler>();
            stubCustomersHandler.Setup(h => h.GetAll()).Returns(customers);
            return new CustomersController(stubCustomersHandler.Object);
        }
    }
}
