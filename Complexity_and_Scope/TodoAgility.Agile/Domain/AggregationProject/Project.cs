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
using System.Collections.Immutable;
using System.Linq;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.AggregationProject
{
    public sealed class Project : ValueObject, IExposeValue<ProjectState>
    {
        private Project(Description description, EntityId id, IReadOnlyList<EntityId> tasks)
        {
            Description = description;
            Id = id;
            Activities = new List<EntityId>(tasks);
        }

        public EntityId Id { get; }
        public Description Description { get; }

        public IReadOnlyList<EntityId> Activities { get; }

        ProjectState IExposeValue<ProjectState>.GetValue()
        {
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;

            var tasks = Activities.Select(t =>
            {
                IExposeValue<uint> task = t;
                return new ActivityStateReference(task.GetValue(), id.GetValue());
            });

            return new ProjectState(stateDescr.GetValue(), id.GetValue(), tasks.ToList());
        }

        public static Project From(EntityId id, Description description)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            return new Project(description, id, ImmutableList<EntityId>.Empty);
        }

        public static Project CombineProjectAndActivities(Project project, IReadOnlyList<EntityId> activities)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (activities == null)
            {
                throw new ArgumentNullException(nameof(activities));
            }

            return new Project(project.Description, project.Id, activities);
        }

        //     
        /// <summary>
        ///     used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Project FromState(ProjectState state)
        {
            if (state == null)
            {
                throw new ArgumentException("Informe um projeto válido.", nameof(state));
            }

            var activities = state.Activities.Select(ac => { return EntityId.From(ac.ProjectId); }).ToList();

            return new Project(Description.From(state.Description), EntityId.From(state.ProjectId), activities);
        }

        public override string ToString()
        {
            return $"[PROJECT]:[Id:{Id}, description: {Description}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Description;

            foreach (var activity in Activities)
            {
                yield return activity;
            }
        }
    }
}