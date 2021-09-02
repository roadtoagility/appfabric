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

using System.ComponentModel.DataAnnotations;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;

namespace AppFabric.Domain.AggregationProject
{
    public sealed class ProjectAggregationRoot : AggregationRoot<Project>
    {

        private ProjectAggregationRoot(Project project)
        {
            if (project.ValidationResults.IsValid)
            {
                Apply(project);
                
                if (project.IsNew())
                {
                    Raise(ProjectAddedEvent.For(project));
                }
            }

            ValidationResults = project.ValidationResults;
        }
        
        private ProjectAggregationRoot(EntityId id, ProjectName name, ProjectCode code, 
            Money budget, DateAndTime startDate, EntityId clientId)
            : this(Project.NewRequest(id, name,code,startDate,budget,clientId))
        {
        }

        public void UpdateDetail(Project.ProjectDetail detail)
        {
            var change = Project.CombineWith(GetChange(), detail);
            if (change.ValidationResults.IsValid)
            {
                Apply(change);
                Raise(ProjectDetailUpdatedEvent.For(change));
            }

            ValidationResults = change.ValidationResults;
        }

        #region Aggregation contruction

        
        public static ProjectAggregationRoot ReconstructFrom(Project currentState)
        {
            return new ProjectAggregationRoot(Project.From(currentState.Id,
                            currentState.Name,
                            currentState.Code,
                            currentState.StartDate,
                            currentState.Budget,
                            currentState.ClientId,
                            currentState.Owner,
                            currentState.Status,
                            currentState.OrderNumber,
                            Version.Next(currentState.Version)));
        }

        
        public static ProjectAggregationRoot CreateFrom(ProjectName name, ProjectCode code, Money budget, DateAndTime startDate, EntityId clientId)
        {
            return new ProjectAggregationRoot(EntityId.GetNext(), name,code,budget,startDate,clientId);
        }

        public static ProjectAggregationRoot CreateFrom(ProjectName name, ServiceOrderNumber serviceOrder, ProjectStatus status, ProjectCode code, Money budget, DateAndTime startDate, EntityId clientId)
        {
            return new ProjectAggregationRoot(EntityId.GetNext(), name, code, budget, startDate, clientId);
        }

        #endregion

        public void Remove()
        {
            if (ValidationResults.IsValid)
            {
                Raise(ProjectRemovedEvent.For(this.GetChange()));
            }
        }
    }
}