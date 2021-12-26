using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.BusinessObjects;
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