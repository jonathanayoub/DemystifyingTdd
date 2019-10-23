using DemystifyingTdd.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace DemystifyingTdd.Specs.Shared
{
    public class ApiFeatureBase
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        protected HttpClient HttpClient;

        public ApiFeatureBase(
            WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [BeforeScenario]
        public void InitializeWebClient()
        {
            HttpClient = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }
    }
}
