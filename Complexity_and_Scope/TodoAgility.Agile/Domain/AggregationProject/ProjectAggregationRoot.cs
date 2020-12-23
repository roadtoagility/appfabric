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

using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.AggregationProject.Events;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.Aggregates;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;

namespace TodoAgility.Agile.Domain.AggregationProject
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

        /// <summary>
        ///     to register new aggregate as change
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="entityId"></param>
        /// <param name="project"></param>
        private ProjectAggregationRoot(Description descr, EntityId entityId)
            : this(Project.From(entityId, descr))
        {
            Change(_entityRoot);
            Raise(ProjectAddedEvent.For(_entityRoot));
        }

        #region Aggregation contruction

        /// <summary>
        ///     reconstructing aggregation from current state loaded from store
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public static ProjectAggregationRoot ReconstructFrom(Project currentState)
        {
            return new ProjectAggregationRoot(currentState);
        }

        /// <summary>
        ///     creating new aggregation as design by business based on business concepts
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="project"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public static ProjectAggregationRoot CreateFrom(Description descr, EntityId entityId)
        {
            return new ProjectAggregationRoot(descr, entityId);
        }

        #endregion
    }
}