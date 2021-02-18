using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Business.QueryHandlers;
using AppFabric.Tests.Integration.Support;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AppFabric.Tests.Integration
{
    public class Projects_api_tests:IClassFixture<CustomWebApplicationFactory<AppFabric.API.Startup>>
    {
        private readonly CustomWebApplicationFactory<AppFabric.API.Startup> _factory;
        
        public Projects_api_tests(CustomWebApplicationFactory<AppFabric.API.Startup> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task Post_NewProject()
        {
            // Arrange
            var url = "/api/projects/save";
            var fixture = new Fixture();
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(0,long.MaxValue));
            
            var command = fixture.Build<AddProjectCommand>()
                .With(prj => prj.ClientId, Guid.Parse("232C32F1-5A69-43FF-8FFB-360E1EF6A08E"))
                .Create();
            
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

        [Theory]
        [InlineData("/api/projects/save/","7D74E1C4-3C35-47B9-B17B-7D5F9D9DFCF6")]
        public async Task Put_UpdateProjectDetails(string url, Guid id)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(0,long.MaxValue));
            
            var command = fixture.Build<UpdateProjectCommand>()
                .With(prj => prj.Id, id)
                .With(prj => prj.Owner, string.Format($"{fixture.Create<string>()}@teste.com"))
                .With(prj => prj.Status, 1)
                .Create();
            
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost/")
            });

            // Act
            var response = await client.PutAsJsonAsync(String.Concat(url,id), command);
            
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var result = await response.Content.ReadFromJsonAsync<ExecutionResult>();

            Assert.True(result?.IsSucceed);
        }
        
        [Fact]
        public async Task Get_Projects()
        {
            // Arrange
            var url = "/api/projects/list";
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
            var projects = await Task.Factory.StartNew(()=> JsonConvert.DeserializeObject<GetProjectsResponse>(data));
            Assert.True(projects?.IsSucceed);
        }
        
        [Theory]
        [InlineData("/api/projects/","DEF2A92E-A53E-4754-B811-2C0C7C7858FA")]
        [InlineData("/api/projects/","11263366-D54A-4E7A-B820-D8894B6C5362")]
        public async Task Get_Project_By_Id(string url, Guid id)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.GetAsync(String.Concat(url,id));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var data = await response.Content.ReadAsStringAsync();
            var found = await Task.Factory.StartNew(()=> JsonConvert.DeserializeObject<GetProjectResponse>(data));
            Assert.True(found?.IsSucceed);
            Assert.Equal(id,found?.Data.Id);
        }
        
        [Theory]
        [InlineData("/api/projects/{0}","65CC91A2-267F-4FFE-8CE0-796AECD6AB4D",1)]
        public async Task Delete_Project(string url, Guid id, int version)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("https://localhost")
            });

            // Act
            var response = await client.DeleteAsync(String.Format(url,id,version));

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var data = await response.Content.ReadAsStringAsync();
            var result = await Task.Factory.StartNew(()=> JsonConvert.DeserializeObject<ExecutionResult>(data));
            Assert.True(result?.IsSucceed);
        }
    }
}