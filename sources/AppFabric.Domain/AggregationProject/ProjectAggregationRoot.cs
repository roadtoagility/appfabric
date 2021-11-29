﻿// Copyright (C) 2020  Road to Agility
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

using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationProject
{
    public sealed class ProjectAggregationRoot : ObjectBasedAggregationRoot<Project, EntityId>
    {
        private readonly ISpecification<Project> _spec;

        public ProjectAggregationRoot(ISpecification<Project> specification, Project project)
        {
            _spec = specification;
            if (_spec.IsSatisfiedBy(project))
            {
                Apply(project);

                if (project.IsNew())
                {
                    Raise(ProjectAddedEvent.For(project));
                }
            }

            AppendValidationResult(project.Failures);
        }

        public void UpdateDetail(Project.ProjectDetail detail)
        {
            var projUpdated = Project.CombineWith(AggregateRootEntity, detail);

            if (_spec.IsSatisfiedBy(projUpdated))
            {
                Apply(projUpdated);
                Raise(ProjectDetailUpdatedEvent.For(projUpdated));
            }

            AppendValidationResult(projUpdated.Failures);
        }
        
        public void Remove()
        {
            if (IsValid)
            {
                Raise(ProjectRemovedEvent.For(AggregateRootEntity));
            }
        }
    }
}