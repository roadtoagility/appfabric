﻿using AppFabric.Domain.AggregationActivity;
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
    public class ActivityAggregateTests
    {
        //O esforço não pode ser superior a 8 horas
        [Fact]
        public void ShouldNotAllowEffortBiggerThan8hours()
        {
            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 9);

            Assert.False(activityAgg.ValidationResults.IsValid);
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x.GetType() == typeof(EffortIncreasedEvent));
        }

        //Não é possível concluir uma atividade com esforço pendente
        [Fact]
        public void ShouldNotAllowCloseActivityWithPendingEffort()
        {
            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 8);
            activityAgg.UpdateRemaining(7);
            activityAgg.Close();

            Assert.False(activityAgg.ValidationResults.IsValid);
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x.GetType() == typeof(EffortIncreasedEvent));
        }

        //Só é possível associar atividades a membros do projeto
        [Fact]
        public void ShouldAsignActivityToMember()
        {
            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 8);

            var memberId = EntityId.From(Guid.NewGuid());
            var member = Member.From(memberId, projectId, "Douglas");
            activityAgg.Asign(member);

            Assert.True(activityAgg.ValidationResults.IsValid);
            Assert.Contains(activityAgg.GetEvents(), x => x.GetType() == typeof(MemberAsignedEvent));
        }

        [Fact]
        public void ShouldNotAsignActivity()
        {
            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 8);

            var memberId = EntityId.From(Guid.NewGuid());

            projectId = EntityId.From(Guid.NewGuid());
            var member = Member.From(memberId, projectId, "Douglas");
            activityAgg.Asign(member);

            Assert.False(activityAgg.ValidationResults.IsValid);
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x.GetType() == typeof(MemberAsignedEvent));
        }
    }
}