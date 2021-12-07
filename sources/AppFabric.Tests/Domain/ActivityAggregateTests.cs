using AppFabric.Business;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain;
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationBilling;
using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.AggregationRelease;
using AppFabric.Domain.AggregationRelease.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class ActivityAggregateTests
    {
        //O esforço não pode ser superior a 8 horas
        [Fact]
        public void ShouldNotAllowEffortBiggerThan8hours()
        {
            var projectId = EntityId.From(Guid.NewGuid());
            var aggFactory = new AggregateFactory();
            var activityAgg = aggFactory.Create(new CreateActivityCommand(projectId, 9));

            Assert.False(activityAgg.Failures.Any());
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x.GetType() == typeof(EffortIncreasedEvent));
        }

        //Não é possível concluir uma atividade com esforço pendente
        [Fact]
        public void ShouldNotAllowCloseActivityWithPendingEffort()
        {
            var projectId = EntityId.From(Guid.NewGuid());
            var aggFactory = new AggregateFactory();
            var activityAgg = aggFactory.Create(new CreateActivityCommand(projectId, 9));
            activityAgg.UpdateRemaining( Effort.From(7));
            activityAgg.Close();

            Assert.True(activityAgg.Failures.Any());
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x.GetType() == typeof(EffortIncreasedEvent));
        }

        //Só é possível associar atividades a membros do projeto
        [Fact]
        public void ShouldAsignActivityToMember()
        {
            var estimatedHours = 8;
            var projectId = EntityId.From(Guid.NewGuid());
            var aggFactory = new AggregateFactory();
            var activityAgg = aggFactory.Create(new CreateActivityCommand(projectId, estimatedHours));

            var memberId = EntityId.From(Guid.NewGuid());
            var member = Member.From(memberId, projectId,  Name.From("Douglas"), VersionId.Empty());
            activityAgg.Assign(member);

            Assert.False(activityAgg.Failures.Any());
            Assert.Contains(activityAgg.GetEvents(), x => x.GetType() == typeof(MemberAsignedEvent));
        }

        [Fact]
        public void ShouldNotAsignActivity()
        {
            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var aggFactory = new AggregateFactory();
            var activityAgg = aggFactory.Create(new CreateActivityCommand(projectId, 9));

            var memberId = EntityId.From(Guid.NewGuid());

            projectId = EntityId.From(Guid.NewGuid());
            var member = Member.From(memberId, projectId, Name.From("Douglas"), VersionId.Empty());
            activityAgg.Assign(member);

            Assert.True(activityAgg.Failures.Any());
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x.GetType() == typeof(MemberAsignedEvent));
        }
    }
}
