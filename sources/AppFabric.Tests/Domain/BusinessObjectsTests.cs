using System;
using System.Globalization;
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
    public class BusinessObjectsTests
    {
        [Theory]
        [InlineData("S987987", true)]
        public void ShouldCreatedServiceOrder(string soNumber, bool soStatus)
        {
            ServiceOrder bo = ServiceOrder.From((soNumber, soStatus));
            Assert.True(bo.ValidationStatus.IsValid);
        }

        // [Fact]
        // public void ServiceOrderShouldNotBeCreate()
        // {
        //     var bo = ServiceOrder.From((null,false));
        //     Assert.False(bo.ValidationStatus.IsValid);
        // }
    }
}