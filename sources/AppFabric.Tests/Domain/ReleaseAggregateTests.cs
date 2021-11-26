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
    public class ReleaseAggregateTests
    {
        //Só é possível adicionar atividades concluídas
        [Fact]
        public void ShouldCreateReleaseWithActivity()
        {
            var factory = new AggregateFactory();
            var releaseAgg = factory.Create(new CreateReleaseCommand(Guid.NewGuid()));

            var projectId = EntityId.From(Guid.NewGuid());
            
            var activity = factory.Create(new CreateActivityCommand(projectId, 3));
            
            var activityAgg = factory.Create(activity.GetChange());

            releaseAgg.AddActivity(activityAgg.GetChange());
            Assert.False(releaseAgg.Failures.Any());
            Assert.Contains(releaseAgg.GetEvents(), x => x.GetType() == typeof(ActivityAddedEvent));
        }
    }
}
