﻿// Copyright (C) 2021  Road to Agility
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Library General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Library General Public License for more details.
//
// You should have received a copy of the GNU Library General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
// Boston, MA  02110-1301, USA.
//

using System;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationBilling;
using AppFabric.Domain.AggregationBilling.Specifications;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;

namespace AppFabric.Business.CommandHandlers.Factories
{
    public class BillingAggregateFactory :
        IAggregateFactory<BillingAggregationRoot, CreateBillingCommand>,
        IAggregateFactory<BillingAggregationRoot, Billing>
    {
        public BillingAggregationRoot Create(Billing source)
        {
            var billingSpec = new BillingCreationSpecification();

            if (billingSpec.IsSatisfiedBy(source) == false) throw new ArgumentException("Invalid Command");

            return new BillingAggregationRoot(source);
        }

        public BillingAggregationRoot Create(CreateBillingCommand source)
        {
            // TODO: cadê pelo menos uma release para faturar???
            var billing = Billing.NewRequest();

            var newBillingSpec = new BillingCreationSpecification();

            if (newBillingSpec.IsSatisfiedBy(billing) == false) throw new ArgumentException("Invalid Command");
            return new BillingAggregationRoot(billing);
        }
    }
}