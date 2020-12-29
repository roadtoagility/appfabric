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
using TodoAgility.Agile.Persistence.Framework.Model;

namespace TodoAgility.Persistence.Model
{
    public class ProjectState : PersistentState
    {
        public ProjectState(string name, string code, decimal budget, DateTime startDate, uint clientId)
        :base(startDate)
        {
            ClientId = clientId;
            Name = name;
            Code = code;
            StartDate = startDate;
            Budget = budget;
        }

        public uint ClientId { get; }
        public string Name { get; }
        public string Code { get; }
        public DateTime StartDate { get; }
        public decimal Budget { get; }
    }
}