using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationBilling.Events;
using AppFabric.Domain.AggregationBilling.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Tests.Domain.Data;
using Xunit;

namespace AppFabric.Tests.Domain
{
    public class BillingAggregateTests
    {
        //Todas as releases devem ser do mesmo cliente
        [Theory]
        [ClassData(typeof(GenerateReleaseValidTestingData))]
        public void ShouldCreateBillingWithRelease(EntityId clientId, Release releaseOne, Release releaseTwo)
        {
            var factory = new BillingAggregateFactory();
            var spec = new ReleaseCanBeBilled(clientId);
            // Add client?
            var billingAgg = factory.Create(new CreateBillingCommand(Guid.NewGuid()));
            billingAgg.AddRelease(releaseOne, spec);
            billingAgg.AddRelease(releaseTwo, spec);
            
            Assert.False(billingAgg.IsValid);
            Assert.Contains(billingAgg.GetEvents(), x => x is ReleaseAddedEvent);
        }

        [Theory]
        [ClassData(typeof(GenerateReleaseDifferentClientsTestingData))]
        public void ShouldNotCreateBillingWithReleaseFromDifferentClient(EntityId clientId, Release releaseOne, Release releaseTwo)
        {
            var factory = new BillingAggregateFactory();
            var spec = new ReleaseCanBeBilled(clientId);
            // Add client?
            var billingAgg = factory.Create(new CreateBillingCommand(Guid.NewGuid()));
            billingAgg.AddRelease(releaseOne, spec);
            billingAgg.AddRelease(releaseTwo, spec);
            
            Assert.False(billingAgg.IsValid);
        }
    }
}