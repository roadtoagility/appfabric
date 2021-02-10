using System;
using System.Threading.Tasks;
using AppFabric.Persistence;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.ReadModel.Repositories;
using AppFabric.Tests.Integration.Support;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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
        [InlineData("/api/Clients/list?name=u")]
        // [InlineData("/api/Clients/{id}")]
        public async Task Get_Clients(string url)
        {
            // Arrange
            var client = _factory
                // .WithWebHostBuilder(builder =>
                // {
                //     builder.ConfigureTestServices(services =>
                //     {
                //         services.AddScoped<IUserProjectionRepository, UserProjectionRepository>();
                //         services.AddScoped<IDbSession<IUserProjectionRepository>, DbSession<IUserProjectionRepository>>();
                //     });
                // })
                .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });
            // client.DefaultRequestHeaders.Add("content-type","application/json; charset=utf-8");
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            // Assert.Equal("text/html; charset=utf-8", 
            //     response.Content.Headers.ContentType.ToString());
        }    
    }
}