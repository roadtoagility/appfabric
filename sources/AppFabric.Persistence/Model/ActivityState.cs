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
using DFlow.Persistence.Model;

namespace AppFabric.Persistence.Model
{
    public class ActivityState : PersistentState
    {
        public ActivityState(int status, string description, uint activityId, uint projectId, byte[] rowVersion)
            : base(DateTime.Now, rowVersion)
        {
            ActivityId = activityId;
            Status = status;
            Description = description;
            ProjectId = projectId;
        }

        public uint ActivityId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }

        public uint ProjectId { get; set; }
    }
}