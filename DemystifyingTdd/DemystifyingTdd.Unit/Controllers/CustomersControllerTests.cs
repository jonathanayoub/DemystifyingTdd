using DemystifyingTdd.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DemystifyingTdd.Unit.Controllers
{
    public class CustomersControllerTests
    {
        [Fact]
        public void WhenTheRequestIsMade_ThenOkStatusIsReturned()
        {
            var sut = BuildSut();

            var result = sut.GetCustomers();

            result.Should().BeOfType<OkObjectResult>();
        }

        private CustomersController BuildSut()
        {
            return new CustomersController();
        }
    }
}
