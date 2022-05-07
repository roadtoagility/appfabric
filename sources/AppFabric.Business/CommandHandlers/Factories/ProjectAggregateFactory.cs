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
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Specifications;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;

namespace AppFabric.Business.CommandHandlers.Factories
{
    public class ProjectAggregateFactory :
        IAggregateFactory<ProjectAggregationRoot, AddProjectCommand>,
        IAggregateFactory<ProjectAggregationRoot, Project>
    {
        public ProjectAggregationRoot Create(AddProjectCommand command)
        {
            var newProjectSpec = new ProjectCreationSpecification();

            var project = Project.NewRequest(
                command.Name,
                command.ServiceOrderNumber,
                command.Status,
                command.Code,
                command.StartDate,
                command.Budget,
                command.ClientId,
                command.Owner);

            if (newProjectSpec.IsSatisfiedBy(project) == false) throw new ArgumentException("Invalid Command");

            return new ProjectAggregationRoot(project);
        }

        public ProjectAggregationRoot Create(Project source)
        {
            var projectSpec = new ProjectSpecification();

            if (projectSpec.IsSatisfiedBy(source) == false) throw new ArgumentException("Invalid Command");

            return new ProjectAggregationRoot(source);
        }
    }
}