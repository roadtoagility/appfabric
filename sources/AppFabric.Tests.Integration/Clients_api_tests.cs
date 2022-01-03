using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppFabric.API;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Tests.Integration.Support;
using AutoFixture;
using DFlow.Business.Cqrs.CommandHandlers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AppFabric.Tests.Integration
{
    public class Clients_api_tests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Clients_api_tests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_NewClient()
        {
            // Arrange
            var url = "/api/clients/save";
            var command = new AddUserCommand("First user","245345345","my@mail.com");

            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost/")
            });

            // Act
            var response = await client.PostAsJsonAsync(url, command);

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
            var clients = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<GetClientsResponse>(data));
            Assert.True(clients?.IsSucceed);
        }

        [Theory]
        [InlineData("/api/clients/", "81DC52BA-5D45-4E17-97EC-BEE71E459232")]
        [InlineData("/api/clients/", "E2528E3F-601F-4B67-92BA-D9E27462006F")]
        public async Task Get_Client_By_Id(string url, Guid id)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.GetAsync(string.Concat(url, id));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var found = await response.Content.ReadFromJsonAsync<GetClientResponse>();
            Assert.True(found?.IsSucceed);
            Assert.Equal(id, found?.Data.Id);
        }

        [Theory]
        [InlineData("/api/clients/{0}", "65CC91A2-267F-4FFE-8CE0-796AECD6AB4D")]
        public async Task Delete_Client(string url, Guid id)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.DeleteAsync(string.Format(url, id));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var data = await response.Content.ReadAsStringAsync();
            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ExecutionResult>(data));
            Assert.True(result?.IsSucceed);
        }
    }
}