using DemystifyingTdd.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace DemystifyingTdd.Specs.AdditionStories
{
    [Binding]
    public class AdditionSteps
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private int _number1;
        private int _number2;
        private HttpClient _client;
        private HttpResponseMessage _response;
        private string _requestUrl;

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
            _number1 = number1;
            _number2 = number2;
        }
        
        [Given(@"an additions url of ""(.*)""")]
        public void GivenAnAdditionsUrlOf(string requestUrl)
        {
            _requestUrl = requestUrl;
        }


        [When(@"I calculate the addition result")]
        public void WhenICalculateTheAdditionResult()
        {
            var uri = new Uri(_requestUrl, UriKind.Relative);
            var postData = $"{{\"number1\": {_number1}, \"number2\":{_number2}}}";
            var content = new StringContent(postData, Encoding.UTF8, "application/json");
            _response = _client.PostAsync(uri, content).Result;
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expectedResult)
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = _response.Content.ReadAsAsync<int>().Result;
            result.Should().Be(expectedResult);
        }
    }
}
