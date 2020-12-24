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
using System.Collections.Immutable;
using FluentValidation.Results;
using TodoAgility.Domain.Framework.DomainEvents;

namespace TodoAgility.Agile.Domain.Framework.Aggregates
{
    public abstract class AggregationRoot<TChange> : IChangeSet<TChange>
    {
        protected TChange _entityRoot;
        private readonly IList<IDomainEvent> _domainEvents;
        
        protected AggregationRoot(TChange entityRoot)
        {
            _entityRoot = entityRoot;
            _domainEvents = new List<IDomainEvent>();
        } 
        
        protected void Change(TChange item)
        {
            _entityRoot = item;
        }
        
        protected void Raise(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }
        
        public TChange GetChange()
        {
            return _entityRoot;
        }

        public IReadOnlyList<IDomainEvent> GetEvents()
        {
            return _domainEvents.ToImmutableList();
        }
        
        public ValidationResult ValidationResults { get; protected set; }
    }
}