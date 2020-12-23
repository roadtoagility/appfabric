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
using System.Collections.Generic;
using TodoAgility.Agile.Domain.AggregationActivity.Validators;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.Validation;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.AggregationActivity
{
    public sealed class Activity : ValidationStatus, IExposeValue<ActivityState>
    {
        private static readonly int InitialStatus = 1;

        private Activity(ActivityStatus status, Description description, EntityId id,
            EntityId projectId)
        {
            Status = status;
            Description = description;
            Id = id;
            ProjectId = projectId;
        }

        public EntityId ProjectId { get; }

        public ActivityStatus Status { get; }

        public EntityId Id { get; }

        public Description Description { get; }

        ActivityState IExposeValue<ActivityState>.GetValue()
        {
            IExposeValue<int> stateStatus = Status;
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;
            IExposeValue<uint> project = ProjectId;
            var stateProject = project.GetValue();
            return new ActivityState(stateStatus.GetValue(), stateDescr.GetValue()
                , id.GetValue(), stateProject);
        }

        public static Activity From(Description description, EntityId entityId, EntityId projectId, ActivityStatus status)
        {
            var activity = new Activity(status, description,entityId, projectId);
            var validator = new ActivityValidator();
            activity.SetValidationResult(validator.Validate(activity));
            return activity;
        }

        /// <summary>
        ///     used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Activity FromState(ActivityState state)
        {
            return Activity.From( Description.From(state.Description), EntityId.From(state.ActivityId),
                EntityId.From(state.ProjectId),ActivityStatus.From(state.Status));
        }

        /// <summary>
        ///     used to update the aggregation
        /// </summary>
        /// <param name="current"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Activity CombineWithPatch(Activity current, Patch patch)
        {
            return Activity.From(patch.Description, current.Id, current.ProjectId,current.Status);
        }

        public static Activity CombineWithStatus(Activity current, ActivityStatus status)
        {
            return From(current.Description,current.Id,current.ProjectId,status);
        }

        public override string ToString()
        {
            return $"[TASK]:[Id:{Id}, description: {Description}: status: {Status}: Project: {ProjectId}]";
        }
        
        public class Patch
        {
            private Patch(Description description)
            {
                Description = description;
            }

            public Description Description { get; }

            public static Patch FromDescription(Description descr)
            {
                return new Patch(descr);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Description;
            yield return Status;
            yield return ProjectId;
        }
    }
}