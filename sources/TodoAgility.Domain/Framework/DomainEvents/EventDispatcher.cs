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

namespace TodoAgility.Domain.Framework.DomainEvents
{
    public sealed class DomainEventDispatcher : IEventDispatcher
    {
        private readonly IDictionary<string, IDictionary<string, IDomainEventHandler>> _eventRegistry =
            new SortedDictionary<string, IDictionary<string, IDomainEventHandler>>();

        public void Subscribe(String eventType, IDomainEventHandler handler)
        {
            if (!_eventRegistry.ContainsKey(eventType))
            {
                _eventRegistry.Add(eventType, new SortedDictionary<string, IDomainEventHandler>());
            }

            var handlers = _eventRegistry[eventType];
            if (!handlers.ContainsKey(handler.HandlerId))
            {
                handlers.Add(handler.HandlerId, handler);                
            }
        }
        
        public void Publish(IReadOnlyList<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Publish(@event);
            }
        }
        
        public void Publish(IDomainEvent @event)
        {
            var evt = @event.GetType().FullName;

            if (string.IsNullOrEmpty(evt) || !_eventRegistry.ContainsKey(evt))
            {
                return;
            }
            
            foreach (var handler in _eventRegistry[evt].Values)
            {
                handler.Handle(@event);
            }
        }
    }
}