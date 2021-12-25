// Copyright (C) 2021  Road to Agility
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
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Specifications;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations.ActivityRules;
using DFlow.Domain.Aggregates;

namespace AppFabric.Business.CommandHandlers.Factories
{
    public class ActivityAggregateFactory : 
        IAggregateFactory<ActivityAggregationRoot, Activity>,
        IAggregateFactory<ActivityAggregationRoot, CreateActivityCommand>
    {
        public ActivityAggregationRoot Create(Activity source)
        {
            var activitySpec = new ActivitySpecification()
                .And(new ActivityClosedSpecification())
                .And(new ActivityCloseWithoutEffortSpecification())
                .And(new ActivityEffortSpecification())
                .And(new ActivityResponsibleSpecification());

            if (activitySpec.IsSatisfiedBy(source))
            {
                return new ActivityAggregationRoot(source);
            }
            throw new Exception("Invalid Command");
        }

        public ActivityAggregationRoot Create(CreateActivityCommand source)
        {
            var activity = Activity.New(source.ProjectId, source.EstimatedHours);
            var newActivitySpec = new ActivityCreationSpecification();

            if (newActivitySpec.IsSatisfiedBy(activity) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new ActivityAggregationRoot(activity);
        }
        
        
    }
}
