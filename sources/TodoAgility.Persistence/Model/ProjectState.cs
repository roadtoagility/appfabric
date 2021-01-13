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
using TodoAgility.Persistence.Framework.Model;

namespace TodoAgility.Persistence.Model
{
    public class ProjectState : PersistentState
    {
        public ProjectState(long id, string name, string code, decimal budget, DateTime startDate, long clientId, string owner, string orderNumber, int status)
        :base(startDate)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Code = code;
            StartDate = startDate;
            Budget = budget;
            Owner = owner;
            Status = status;
            OrderNumber = orderNumber;
        }

        public long Id { get; }
        public long ClientId { get; }
        public string Name { get; }
        public string Code { get; }
        public DateTime StartDate { get; }
        public decimal Budget { get; }
        
        public string Owner { get; set; }
        
        public string OrderNumber { get; set; }
        
        public int Status { get; set; }
    }
}