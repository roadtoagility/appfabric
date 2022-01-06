using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationActivity.Events;
using AppFabric.Domain.AggregationActivity.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Tests.Domain.Data;
using DFlow.Domain.BusinessObjects;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class ActivityAggregateTests
    {
        //O esforço não pode ser superior a 8 horas
        [Theory]
        [InlineData("d3a70308-b2cd-4b86-ae13-5d9e2f515177",9)]
        public void ShouldNotAllowEffortBiggerThan8Hours(Guid projectId, int effortHours)
        {
            var aggFactory = new ActivityCreateAggregateFactory();
            Assert.Throws<ArgumentException>(()=> aggFactory.Create(new CreateActivityCommand(projectId, effortHours)));
        }
        
        //Não é possível concluir uma atividade com esforço pendente
        [Theory]
        [ClassData(typeof(GenerateValidActivityTestingData))]
        public void ShouldNotAllowCloseActivityWithPendingEffort(Activity activity)
        {
            var aggFactory = new ActivityReconstructAggregateFactory();
            var activityAgg = aggFactory.Create(activity);
            activityAgg.Close(new ActivityCanBeClosed());
        
            Assert.False(activityAgg.IsValid);
        }
        
        //Só é possível associar atividades a membros do projeto
        [Theory]
        [ClassData(typeof(GenerateValidActivityAndMemberTestingData))]
        public void ShouldAssignActivityToMember(Activity activity, Member projectMember)
        {
            var aggFactory = new ActivityReconstructAggregateFactory();
            var activityAgg = aggFactory.Create(activity);
            activityAgg.Assign(projectMember, new ActivityResponsibleSpecification());
        
            Assert.True(activityAgg.IsValid);
            Assert.Contains(activityAgg.GetEvents(), x => x is MemberAssignedEvent);
        }
        //
        [Theory]
        [ClassData(typeof(GenerateInvalidActivityAndMemberTestingData))]
        public void ShouldNotAsignActivity(Activity activity, Member projectMember)
        {
            var aggFactory = new ActivityReconstructAggregateFactory();
            var activityAgg = aggFactory.Create(activity);
            activityAgg.Assign(projectMember, new ActivityResponsibleSpecification());
            
            Assert.False(activityAgg.IsValid);
            Assert.DoesNotContain(activityAgg.GetEvents(), x => x is MemberAssignedEvent);
        }
    }
}