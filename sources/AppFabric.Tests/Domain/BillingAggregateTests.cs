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
            var clientId = EntityId.From(Guid.NewGuid());
            
            //release 1
            var releaseId = EntityId.From(Guid.NewGuid());
            var releaseAgg = ReleaseAggregationRoot.CreateFrom(releaseId, clientId);

            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 8);

            releaseAgg.AddActivity(activityAgg.GetChange());

            //release 2
            var release2Id = EntityId.From(Guid.NewGuid());
            var release2Agg = ReleaseAggregationRoot.CreateFrom(release2Id, clientId);

            var activity2Id = EntityId.From(Guid.NewGuid());
            var project2Id = EntityId.From(Guid.NewGuid());
            var activity2Agg = ActivityAggregationRoot.CreateFrom(activity2Id, project2Id, 8);

            release2Agg.AddActivity(activity2Agg.GetChange());

            // Add client?
            var billingId = EntityId.From(Guid.NewGuid());
            var billingAgg = BillingAggregationRoot.CreateFrom(billingId);

            billingAgg.AddRelease(releaseAgg.GetChange());
            billingAgg.AddRelease(release2Agg.GetChange());
            Assert.True(billingAgg.ValidationResults.IsValid);
            Assert.Contains(billingAgg.GetEvents(), x => x.GetType() == typeof(ReleaseAddedEvent));
        }

        [Fact]
        public void ShouldNotCreateBillingWithReleaseFromDifferentClient()
        {
            var clientId = EntityId.From(Guid.NewGuid());

            //release 1
            var releaseId = EntityId.From(Guid.NewGuid());
            var releaseAgg = ReleaseAggregationRoot.CreateFrom(releaseId, clientId);

            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 8);

            releaseAgg.AddActivity(activityAgg.GetChange());

            //release 2
            clientId = EntityId.From(Guid.NewGuid());
            var release2Id = EntityId.From(Guid.NewGuid());
            var release2Agg = ReleaseAggregationRoot.CreateFrom(release2Id, clientId);

            var activity2Id = EntityId.From(Guid.NewGuid());
            var project2Id = EntityId.From(Guid.NewGuid());
            var activity2Agg = ActivityAggregationRoot.CreateFrom(activity2Id, project2Id, 8);

            release2Agg.AddActivity(activity2Agg.GetChange());

            // Add client?
            var billingId = EntityId.From(Guid.NewGuid());
            var billingAgg = BillingAggregationRoot.CreateFrom(billingId);

            billingAgg.AddRelease(releaseAgg.GetChange());
            billingAgg.AddRelease(release2Agg.GetChange());
            Assert.False(billingAgg.ValidationResults.IsValid);
        }
    }
}
