using AppFabric.Domain.AggregationProject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class ProjectAggregateTests
    {

        [Fact]
        public void ShouldCreateProjectWithServiceOrder()
        {
            var projAgg = ProjectAggregationRoot.CreateFrom(null, null, null, null, null);
           
            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldNotAllowEffortBiggerThan8minutes()
        {
            var projAgg = ProjectAggregationRoot.CreateFrom(null, null, null, null, null);

            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldNotAllowCloseActivityWithPendingEffort()
        {
            var projAgg = ProjectAggregationRoot.CreateFrom(null, null, null, null, null);

            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldAsignActivityToMember()
        {
            var projAgg = ProjectAggregationRoot.CreateFrom(null, null, null, null, null);

            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldNotAsignActivity()
        {
            var projAgg = ProjectAggregationRoot.CreateFrom(null, null, null, null, null);

            Assert.True(projAgg.ValidationResults.IsValid);
        }

        [Fact]
        public void ShouldCreateReleaseWithActivities()
        {
            var projAgg = ProjectAggregationRoot.CreateFrom(null, null, null, null, null);

            Assert.True(projAgg.ValidationResults.IsValid);
        }
    }
}
