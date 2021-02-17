using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Tests.Integration.Support;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
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
        
        [Fact]
        public async Task Post_NewClient()
        {
            // Arrange
            var url = "/api/clients/save";
            var fixture = new Fixture();
            var command = fixture.Build<AddUserCommand>()
                .With(usr => usr.CommercialEmail, string.Format($"{fixture.Create<string>()}@teste.com"))
                .With(usr => usr.Cnpj, fixture.Create<string>())
                .With(usr => usr.Name, fixture.Create<string>())
                .Create();
            
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost/")
            });

            // Act
            var response = await client.PostAsJsonAsync<AddUserCommand>(url, command);
            
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var result = await response.Content.ReadFromJsonAsync<CommandResult<Guid>>();

            Assert.True(result?.IsSucceed);
            Assert.False(result?.Id.Equals(Guid.Empty));
        }
    
        [Fact]
        public async Task Get_Clients()
        {
            // Arrange
            var url = "/api/clients/list";
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var data = await response.Content.ReadAsStringAsync();
            var clients = await response.Content.ReadFromJsonAsync<GetClientsResponse>();
            Assert.True(clients?.IsSucceed);
        }
        
        [Theory]
        [InlineData("/api/clients/117EB8AE-21EF-4049-B11C-36DB81D182E9")]
        [InlineData("/api/clients/77E69C95-E50C-410A-AC07-14D1F0D5CCA0")]
        [InlineData("/api/clients/81DC52BA-5D45-4E17-97EC-BEE71E459232")]
        [InlineData("/api/clients/E2528E3F-601F-4B67-92BA-D9E27462006F")]
        public async Task Get_Client_By_Id(string url)
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
            
            var data = await response.Content.ReadAsStringAsync();
            var clients = await response.Content.ReadFromJsonAsync<GetClientResponse>();
            Assert.True(clients?.IsSucceed);
        }
        
        [Theory]
        [InlineData("/api/clients/65CC91A2-267F-4FFE-8CE0-796AECD6AB4D/1")]
        [InlineData("/api/clients/1107FA5B-6DFC-45BB-9F58-5834F1F1A38C/1")]
        [InlineData("/api/clients/2E28CC8F-2127-4313-BA61-6063F8983585/1")]
        public async Task Delete_Client(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            // var data = await response.Content.ReadAsStringAsync();
            var clients = await response.Content.ReadFromJsonAsync<ExecutionResult>();
            Assert.True(clients?.IsSucceed);
        }

        
    }
}