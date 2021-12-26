using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class ProjectAggregateTests
    {
        //Um projeto só pode ser cadastrado com uma Ordem de Serviço aprovada
        [Fact]
        public void ShouldCreateProjectWithServiceOrder()
        {
            var orderService = ServiceOrder.From(("S20210209O125478593", true));
            var projectId = EntityId.From(Guid.NewGuid());
            var status = ProjectStatus.From("NotApproved");
            AggregateFactory factory = new AggregateFactory();
            var projAgg = factory.Create(new AddProjectCommand
            {
                ServiceOrderNumber = "S20210209O125478593",
                Owner = "doug.ramalho@gma.com",
                Code = "PojectFake",
                Name = "NameFake",
                ServiceOrderStatus = true
            });

            Assert.True(projAgg.IsValid);
            Assert.Contains(projAgg.GetEvents(), x => x is ProjectAddedEvent);
        }

        [Fact]
        public void ShouldNotCreateProjectWithServiceOrderNotApproved()
        {
            var orderService = ServiceOrder.From(("S20210209O125478593", false));
            var projectId = EntityId.From(Guid.NewGuid());
            var status = ProjectStatus.From("NotApproved");
            AggregateFactory factory = new AggregateFactory();

            var ex = Assert.Throws<Exception>(() =>
            {
                var projAgg = factory.Create(new AddProjectCommand
                {
                    ServiceOrderNumber = "S20210209O125478593",
                    Owner = "doug.ramalho@gma.com",
                    Code = "PojectFake",
                    Name = "NameFake",
                    Status = "NotApproved",
                    ServiceOrderStatus = false
                });
            });
        }
    }
}