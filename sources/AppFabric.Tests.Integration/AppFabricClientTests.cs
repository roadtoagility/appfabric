using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppFabric.Business.QueryHandlers;
using AppFabric.Persistence;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.ReadModel.Repositories;
using AppFabric.Tests.Integration.Support;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace AppFabric.Tests.Integration
{
    public class AppFabricClientTests:IClassFixture<CustomWebApplicationFactory<AppFabric.API.Startup>>
    {
        private readonly CustomWebApplicationFactory<AppFabric.API.Startup> _factory;
        
        public AppFabricClientTests(CustomWebApplicationFactory<AppFabric.API.Startup> factory)
        {
            _factory = factory;
        }
        
    
        [Theory]
        [InlineData("/api/Clients/list")]
        // [InlineData("/api/Clients/{id}")]
        public async Task Get_Clients(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var clients = await response.Content.ReadFromJsonAsync<GetClientsResponse>();
            Assert.True(clients?.IsSucceed);
        }    
    }
}