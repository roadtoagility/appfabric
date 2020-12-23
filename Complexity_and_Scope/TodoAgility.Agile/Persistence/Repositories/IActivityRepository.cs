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


using System.Collections.Generic;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Framework.Repositories;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories
{
    public interface IActivityRepository : IRepository<ActivityState, Activity>
    {
        void AddProject(IExposeValue<ProjectStateReference> project);
        
        void RemoveProject(IExposeValue<ProjectStateReference> projectId);

        Project GetProject(EntityId id);
    }
}