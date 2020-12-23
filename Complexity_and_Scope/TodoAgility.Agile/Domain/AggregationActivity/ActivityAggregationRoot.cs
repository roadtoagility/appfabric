// Copyright (C) 2020  Road to Agility
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
using FluentValidation;
using TodoAgility.Agile.Domain.AggregationActivity.Events;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.Aggregates;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.Validation;

namespace TodoAgility.Agile.Domain.AggregationActivity
{
    public sealed class ActivityAggregationRoot : AggregationRoot<Activity>
    {
        /// <summary>
        ///     load an aggregate from store
        /// </summary>
        /// <param name="currentActivity"></param>
        private ActivityAggregationRoot(Activity currentActivity)
            : base(currentActivity)
        {
            if (currentActivity.ValidationResults.IsValid)
            {
                Change(currentActivity);
                Raise(ActivityAddedEvent.For(currentActivity));
            }

            ValidationResults = currentActivity.ValidationResults;
        }
    
        /// <summary>
        ///     to register new aggregate as change
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="entityId"></param>
        /// <param name="project"></param>
        /// <param name="status"></param>
        private ActivityAggregationRoot(Description descr, EntityId entityId, Project project, ActivityStatus status)
            : this(Activity.From(descr, entityId, project.Id,status))
        {
        }

        /// <summary>
        ///     update currentActivity value allowed from patch interface
        /// </summary>
        /// <param name="patchTask"></param>
        public void UpdateTask(Activity.Patch patchTask)
        {
            var change = Activity.CombineWithPatch(_entityRoot, patchTask);

            if (change.ValidationResults.IsValid)
            {
                var context = new ValidationContext<Activity>(_entityRoot);
                context.RootContextData["activity"] = change;
                
                
                Change(change);
                Raise(ActivityUpdatedEvent.For(change));                
            }
            
            ValidationResults = change.ValidationResults;
        }

        public void ChangeTaskStatus(ActivityStatus newStatus)
        {
            var change = Activity.CombineWithStatus(_entityRoot, newStatus);
            
            if (change.ValidationResults.IsValid)
            {
                Change(change);
                Raise(ActivityStatusChangedEvent.For(change));                
            }
            ValidationResults = change.ValidationResults;
        }

        #region Aggregation contruction

        /// <summary>
        ///     reconstructing aggregation from current state loaded from store
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public static ActivityAggregationRoot ReconstructFrom(Activity currentState)
        {
            return new ActivityAggregationRoot(currentState);
        }

        /// <summary>
        ///     creating new aggregation as design by business based on business concepts
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="project"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public static ActivityAggregationRoot CreateFrom(Description descr, EntityId entityId, Project project)
        {
            return new ActivityAggregationRoot(descr, entityId, project, ActivityStatus.From(1));
        }

        #endregion
    }
}