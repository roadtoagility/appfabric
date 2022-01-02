using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Tests.Domain.Data;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class ProjectAggregateTests
    {
        //Um projeto só pode ser cadastrado com uma Ordem de Serviço aprovada
        [Theory]
        [ClassData(typeof(GenerateValidProjectTestingData))]
        public void ShouldCreateProjectWithServiceOrder(string name, string owner, string projectCode,
            DateTime createAt, int budget, Guid clientId, string serviceOrder, bool serviceOrderStatus, 
            string status)
        {
            var factory = new ProjectAggregateFactory();
            var projAgg = factory.Create(new AddProjectCommand(
                name,
                owner,
                projectCode,
                createAt,
                budget,
                clientId, 
                serviceOrder,                                         
                serviceOrderStatus,
                status
            ));
        
            Assert.True(projAgg.IsValid);
            Assert.Contains(projAgg.GetEvents(), x => x is ProjectAddedEvent);
        }
        
        [Theory]
        [ClassData(typeof(GenerateInvalidProjectTestingData))]
        public void ShouldNotCreateProjectWithServiceOrderNotApproved(string name, string owner, string projectCode,
            DateTime createAt, int budget, Guid clientId, string serviceOrder, bool serviceOrderStatus, 
            string status)
        {
            var factory = new ProjectAggregateFactory();
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var projAgg = factory.Create(new AddProjectCommand(
                    name,
                    owner,
                    projectCode,
                    createAt,
                    budget,
                    clientId, 
                    serviceOrder,                                         
                    serviceOrderStatus,
                    status
                ));
            });
        }
    }
}