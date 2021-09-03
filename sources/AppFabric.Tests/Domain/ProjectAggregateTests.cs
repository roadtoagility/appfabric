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
            var projAgg = ProjectAggregationRoot.CreateFrom(null, orderService, status, null, null, null, projectId);
           
            Assert.True(projAgg.ValidationResults.IsValid);
            Assert.Contains(projAgg.GetEvents(), x => x.GetType() == typeof(ProjectAddedEvent));
        }

        [Fact]
        public void ShouldNotCreateProjectWithServiceOrderNotApproved()
        {
            var orderService = ServiceOrder.From("S20210209O125478593", false);
            var projectId = EntityId.From(Guid.NewGuid());
            var status = ProjectStatus.From(1);
            var projAgg = ProjectAggregationRoot.CreateFrom(null, orderService, status, null, null, null, projectId);

            Assert.False(projAgg.ValidationResults.IsValid);
            Assert.DoesNotContain(projAgg.GetEvents(), x => x.GetType() == typeof(ProjectAddedEvent));
        }

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

        //Só é possível adicionar atividades concluídas
        [Fact]
        public void ShouldCreateReleaseWithActivity()
        {
            var releaseId = EntityId.From(Guid.NewGuid());
            var clientId = EntityId.From(Guid.NewGuid());
            var releaseAgg = ReleaseAggregationRoot.CreateFrom(releaseId, clientId);

            var activityId = EntityId.From(Guid.NewGuid());
            var projectId = EntityId.From(Guid.NewGuid());
            var activityAgg = ActivityAggregationRoot.CreateFrom(activityId, projectId, 8);

            releaseAgg.AddActivity(activityAgg.GetChange());
            Assert.True(releaseAgg.ValidationResults.IsValid);
            Assert.Contains(releaseAgg.GetEvents(), x => x.GetType() == typeof(ActivityAddedEvent));
        }

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
