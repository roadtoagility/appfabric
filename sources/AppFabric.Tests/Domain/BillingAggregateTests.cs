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
    public class BillingAggregateTests
    {
        //Todas as releases devem ser do mesmo cliente
        [Fact]
        public void ShouldCreateBillingWithRelease()
        {
            var aggFactory = new AggregateFactory();
            var clientId = Guid.NewGuid();
            
            //release 1
            var releaseAgg = aggFactory.Create(new CreateReleaseCommand(clientId));

            var projectId = EntityId.From(Guid.NewGuid());
            
            var activityAgg = aggFactory.Create(new CreateActivityCommand(projectId, 3));


            releaseAgg.AddActivity(activityAgg.GetChange());

            //release 2
            var release2Agg = aggFactory.Create(new CreateReleaseCommand(clientId));

            var project2Id = EntityId.From(Guid.NewGuid());

            var activity2Agg = aggFactory.Create(new CreateActivityCommand(project2Id, 8));

            release2Agg.AddActivity(activity2Agg.GetChange());

            // Add client?
            var billingAgg = aggFactory.Create(new CreateBillingCommand(Guid.NewGuid()));

            billingAgg.AddRelease(releaseAgg.GetChange());
            billingAgg.AddRelease(release2Agg.GetChange());
            Assert.False(billingAgg.Failures.Any());
            Assert.Contains(billingAgg.GetEvents(), x => x.GetType() == typeof(ReleaseAddedEvent));
        }

        [Fact]
        public void ShouldNotCreateBillingWithReleaseFromDifferentClient()
        {
            var aggFactory = new AggregateFactory();
            var clientId = Guid.NewGuid();

            //release 1
            var releaseAgg = aggFactory.Create(new CreateReleaseCommand(clientId));

            var projectId = EntityId.From(Guid.NewGuid());

            var activityAgg = aggFactory.Create(new CreateActivityCommand(projectId, 8));


            releaseAgg.AddActivity(activityAgg.GetChange());

            //release 2
            clientId = Guid.NewGuid();
            var release2Agg = aggFactory.Create(new CreateReleaseCommand(clientId));

            var project2Id = EntityId.From(Guid.NewGuid());
            
            var activity2Agg = aggFactory.Create(new CreateActivityCommand(project2Id, 8));

            release2Agg.AddActivity(activity2Agg.GetChange());

            // Add client?
            var billingAgg = aggFactory.Create(new CreateBillingCommand(Guid.NewGuid()));

            billingAgg.AddRelease(releaseAgg.GetChange());
            billingAgg.AddRelease(release2Agg.GetChange());
            Assert.True(billingAgg.Failures.Any());
        }
    }
}
