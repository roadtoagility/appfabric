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
using AppFabric.Domain.ExtensionMethods;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.BusinessObjects
{
    public class Release : BaseEntity<EntityId>
    {
        private Release(EntityId id, EntityId clientId, VersionId version)
            : base(id, version)
        {
            ClientId = clientId;
            Activities = new List<Activity>();

            AppendValidationResult(ClientId.ValidationStatus.ToFailures());
        }

        public EntityId ClientId { get; }

        public List<Activity> Activities { get; }

        public override string ToString()
        {
            return $"[Release]:[ID: {Identity}]";
        }

        public static Release From(EntityId id, EntityId clientId, VersionId version)
        {
            var release = new Release(id, clientId, version);
            return release;
        }

        public static Release NewRequest(EntityId clientId)
        {
            return From(EntityId.GetNext(), clientId, VersionId.New());
        }

        public Release AddActivity(Activity activity)
        {
            Activities.Add(activity);
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
        }

        public Release RemoveActivity(Activity activity)
        {
            Activities.Remove(activity);
            return this;
        }
    }
}