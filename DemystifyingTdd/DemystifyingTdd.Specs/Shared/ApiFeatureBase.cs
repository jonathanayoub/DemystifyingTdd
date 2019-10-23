using DemystifyingTdd.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace DemystifyingTdd.Specs.Shared
{
    public class ApiFeatureBase
    {
        protected readonly WebApplicationFactory<Startup> WebApplicationFactory;
        protected HttpClient HttpClient;

        public ApiFeatureBase(
            WebApplicationFactory<Startup> webApplicationFactory)
        {
            WebApplicationFactory = webApplicationFactory;
        }

        [BeforeScenario(Order = 0)]
        public void InitializeWebClient()
        {
            HttpClient = WebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }
    }
}
