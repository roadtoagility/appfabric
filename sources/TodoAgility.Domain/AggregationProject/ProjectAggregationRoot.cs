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

using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.Aggregates;
using TodoAgility.Domain.Framework.BusinessObjects;

namespace TodoAgility.Domain.AggregationProject
{
    public sealed class ProjectAggregationRoot : AggregationRoot<Project>
    {
        /// <summary>
        ///     load an aggregate from store
        /// </summary>
        /// <param name="currentActivity"></param>
        private ProjectAggregationRoot(Project current)
        :base(current)
        {
            
        }
        
        private ProjectAggregationRoot(ProjectName name, ProjectCode code, 
            Money budget, DateAndTime startDate, EntityId clientId )
            : this(Project.From(name,code,startDate,budget,clientId))
        {
            Change(_entityRoot);
        }

        #region Aggregation contruction

        
        public static ProjectAggregationRoot ReconstructFrom(Project currentState)
        {
            return new ProjectAggregationRoot(currentState);
        }

        
        public static ProjectAggregationRoot CreateFrom(ProjectName name, ProjectCode code, Money budget, DateAndTime startDate, EntityId clientId)
        {
            return new ProjectAggregationRoot(name,code,budget,startDate,clientId);
        }

        #endregion
    }
}