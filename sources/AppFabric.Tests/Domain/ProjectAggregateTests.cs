using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationBilling;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationRelease;
using AppFabric.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
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
            var orderService = ServiceOrderNumber.From("", true);
            var projAgg = ProjectAggregationRoot.CreateFrom(null, orderService, null, null, null);
           
            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldNotCreateProjectWithServiceOrderNotApproved()
        {
            var orderService = ServiceOrderNumber.From("", false);
            var projAgg = ProjectAggregationRoot.CreateFrom(null, orderService, null, null, null);

            Assert.True(projAgg.ValidationResults.IsValid);
        }

        //O esforço não pode ser superior a 8 horas
        [Fact]
        public void ShouldNotAllowEffortBiggerThan8minutes()
        {
            var activityAgg = ActivityAggregationRoot.CreateFrom(projectId);
            var effort = Effort.From(10000, ActivityUnit.Hours);
            var activityId = Guid.NewGuid();
            activityAgg.CreateActivity("", effort, activityId);

            Assert.False(activityAgg.ValidationResults.IsValid);
        }

        //Não é possível concluir uma atividade com esforço pendente
        [Fact]
        public void ShouldNotAllowCloseActivityWithPendingEffort()
        {
            var activityAgg = ActivityAggregationRoot.CreateFrom(projectId);
            var activityId = Guid.NewGuid();
            var effort = Effort.From(9600, ActivityUnit.Hours);
            activityAgg.CreateActivity("", effort, activityId);
            activityAgg.UpdateRemaining(9000, activityId);
            activityAgg.Close();

            Assert.False(activityAgg.ValidationResults.IsValid);
        }

        //Só é possível associar atividades a membros do projeto
        [Fact]
        public void ShouldAsignActivityToMember()
        {
            var projectId = Guid.NewGuid();
            var activityAgg = ActivityAggregationRoot.CreateFrom(projectId);
            var activityId = Guid.NewGuid();
            var effort = Effort.From(9600, ActivityUnit.Hours);
            activityAgg.CreateActivity("", effort, activityId);
            
            var member = Member.From("", memberId, projectId, userId);
            activityAgg.Asign(member);

            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldNotAsignActivity()
        {
            var projectId = Guid.NewGuid();
            var activityAgg = ActivityAggregationRoot.CreateFrom(projectId);
            var activityId = Guid.NewGuid();
            var effort = Effort.From(9600, ActivityUnit.Hours);
            activityAgg.CreateActivity("", effort, activityId);

            var member = Member.From("", memberId, Guid.NewGuid(), userId);
            activityAgg.Asign(member);

            Assert.False(projAgg.ValidationResults.IsValid);
        }

        //Só é possível adicionar atividades concluídas
        [Fact]
        public void ShouldCreateRelease()
        {
            var projectId = Guid.NewGuid();
            var releaseAgg = ReleaseAggregationRoot.CreateFrom(projectId);

            releaseAgg.AddActivity(activityId);
            Assert.True(projAgg.ValidationResults.IsValid);
        }
    }

    //Todas as releases devem ser do mesmo cliente
    [Fact]
        public void ShouldCreateBilling()
        {
            var clientId = Guid.NewGuid();
            var billingAgg = BillingAggregationRoot.CreateFrom(clientId);

            billingAgg.AddRelease(releaseId);
            Assert.True(projAgg.ValidationResults.IsValid);
        }
    }
}
