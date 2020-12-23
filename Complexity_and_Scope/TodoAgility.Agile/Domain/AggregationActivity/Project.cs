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
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.AggregationActivity
{
    public sealed class Project : ValueObject, IExposeValue<ProjectStateReference>
    {
        private Project(Description description, EntityId id)
        {
            Description = description;
            Id = id;
        }

        public EntityId Id { get; }
        public Description Description { get; }
        
        ProjectStateReference IExposeValue<ProjectStateReference>.GetValue()
        {
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;
            
            return new ProjectStateReference(stateDescr.GetValue(),id.GetValue());
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

            return new Project(description, id);
        }
        
        //     
        /// <summary>
        ///     used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Project FromState(ProjectStateReference state)
        {
            if (state == null)
            {
                throw new ArgumentException("Informe um projeto válido.", nameof(state));
            }
            
            return new Project(Description.From(state.Description), EntityId.From(state.ProjectId));
        }

        public override string ToString()
        {
            return $"[PROJECT]:[Id:{Id}, description: {Description}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Description;
        }
    }
}