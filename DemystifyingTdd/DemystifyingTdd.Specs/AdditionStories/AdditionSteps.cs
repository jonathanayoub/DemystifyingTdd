using DemystifyingTdd.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DemystifyingTdd.Specs.AdditionStories
{
    [Binding]
    public class AdditionSteps
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _requestUrl;
        private IEnumerable<decimal> _numbers;

        public AdditionSteps(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Given(@"A web api client")]
        public void GivenAWebApiClient()
        {
            _client = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [Given(@"I want to add (.*) and (.*)")]
        public void GivenIWantToAddAnd(int number1, int number2)
        {
            _numbers = new List<decimal> { number1, number2 };
        }

        [Given(@"an additions url of ""(.*)""")]
        public void GivenAnAdditionsUrlOf(string requestUrl)
        {
            _requestUrl = requestUrl;
        }

        [Given(@"I have the following numbers")]
        public void GivenIHaveTheFollowingNumbers(Table table)
        {
            var numberData = table.CreateInstance<AdditionData>();
            _numbers = new List<decimal> { numberData.Number1, numberData.Number2,
                numberData.Number3, numberData.Number4,
                numberData.Number5, numberData.Number6 };
        }

        [When(@"I calculate the addition result")]
        public void WhenICalculateTheAdditionResult()
        {
            var uri = new Uri(_requestUrl, UriKind.Relative);
            var postData = $"{{\"numbers\": [{string.Join(",", _numbers)}]}}";
            var content = new StringContent(postData, Encoding.UTF8, "application/json");
            _response = _client.PostAsync(uri, content).Result;
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(decimal expectedResult)
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = _response.Content.ReadAsAsync<decimal>().Result;
            result.Should().Be(expectedResult);
        }


        [Then(@"the sum should be correct")]
        public void ThenTheSumShouldBeCorrect()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = _response.Content.ReadAsAsync<decimal>().Result;
            var expectedResult = _numbers.Sum();
            result.Should().Be(expectedResult);
        }

    }
}
