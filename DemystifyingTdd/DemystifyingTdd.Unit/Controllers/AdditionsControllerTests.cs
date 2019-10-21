﻿using DemystifyingTdd.Api.Controllers;
using DemystifyingTdd.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DemystifyingTdd.Unit.Controllers
{
    public class GivenARequestToAddTwoNumbers
    {
        private readonly Addition _dummyAddition = new Addition { Number1 = 7.5M, Number2 = 5.5M };

        [Fact]
        public void WhenTheRequestIsMade_ThenOkStatusIsReturned()
        {
            var sut = BuildSut();

            var result = sut.Add(_dummyAddition);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void WhenTheRequestIsMade_ThenTheCorrectCalculationIsReturned()
        {
            var sut = BuildSut();

            var result = sut.Add(_dummyAddition);

            result.As<OkObjectResult>().Value.Should().Be(13M);
        }

        private AdditionsController BuildSut()
        {
            return new AdditionsController();
        }
    }
}
