using AppFabric.Business;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationBilling;
using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.AggregationRelease;
using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class ProjectAggregateTests
    {

        //Um projeto só pode ser cadastrado com uma Ordem de Serviço aprovada
        [Fact]
        public void ShouldCreateProjectWithServiceOrder()
        {
            var orderService = ServiceOrder.From("S20210209O125478593", true);
            var projectId = EntityId.From(Guid.NewGuid());
            var status = ProjectStatus.From(1);
            AggregateFactory factory = new AggregateFactory();
            var projAgg = factory.Create(new AddProjectCommand() { 
                ServiceOrderNumber = "S20210209O125478593", 
                Owner = "doug.ramalho@gma.com",
                Code = "PojectFake",
                Name = "NameFake",
                ServiceOrderStatus = true
            });


            Assert.True(projAgg.IsValid);
            Assert.Contains(projAgg.GetEvents(), x => x.GetType() == typeof(ProjectAddedEvent));
        }

        [Fact]
        public void ShouldNotCreateProjectWithServiceOrderNotApproved()
        {
            var orderService = ServiceOrder.From("S20210209O125478593", false);
            var projectId = EntityId.From(Guid.NewGuid());
            var status = ProjectStatus.From(1);
            AggregateFactory factory = new AggregateFactory();

            var ex = Assert.Throws<Exception>(() =>
            {
                var projAgg = factory.Create(new AddProjectCommand()
                {
                    ServiceOrderNumber = "S20210209O125478593",
                    Owner = "doug.ramalho@gma.com",
                    Code = "PojectFake",
                    Name = "NameFake",
                    Status = 1,
                    ServiceOrderStatus = false
                });
            });
        }
    }
}
