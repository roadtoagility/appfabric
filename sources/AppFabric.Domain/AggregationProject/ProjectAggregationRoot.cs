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
using System.Diagnostics;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.Events;
using DFlow.Domain.Specifications;
using DFlow.Domain.Validation;
using FluentValidation.Results;

namespace AppFabric.Domain.AggregationProject
{
    public sealed class ProjectAggregationRoot : ObjectBasedAggregationRootWithEvents<Project, EntityId>
    {
        public ProjectAggregationRoot(Project project)
        {
            Debug.Assert(project.IsValid);
            Apply(project);
            
            if (project.IsNew())
            {
                Raise(ProjectAddedEvent.For(project));
            }
        }

        public void UpdateDetail(Project.ProjectDetail detail, ISpecification<Project> specUpdateProject)
        {
            var projUpdated = Project.CombineWith(AggregateRootEntity, detail);

            if (specUpdateProject.IsSatisfiedBy(projUpdated) == false)
            {
                AppendValidationResult(projUpdated.Failures);
            }
            else
            {
                Apply(projUpdated);
                Raise(ProjectDetailUpdatedEvent.For(projUpdated));
            }
        }

        public void Remove(ISpecification<Project> specRemoveProject)
        {
            if (specRemoveProject.IsSatisfiedBy(AggregateRootEntity) == false)
            {
                Apply(AggregateRootEntity);
                Raise(ProjectRemovedEvent.For(AggregateRootEntity));
            }
            else
            {
                AppendValidationResult(Failure.For("Project", "Can´t be removed!"));
            }
        }

        public void AddMember(Member member, ISpecification<Project> spec)
        {
            AppendValidationResult(Failure.For("Member","Not implemented"));      
        }
        
        public void RemoveMember(Member member, ISpecification<Project> spec)
        {
            AppendValidationResult(Failure.For("Member","Not implemented"));
        }
        
        public void AddProject(User client, ISpecification<Project> spec)
        {
            if (spec.IsSatisfiedBy(AggregateRootEntity) == false)
            {
                Apply(AggregateRootEntity);
                Raise(ProjectAddedEvent.For(AggregateRootEntity));
            }
            else
            {
                AppendValidationResult(Failure.For("Project", "Can´t be added to client!"));
            }
        }
    }
}