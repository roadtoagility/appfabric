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
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Domain.Framework.Validation;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.BusinessObjects
{
    public class Release : BaseEntity<EntityId>
    {
        public EntityId ClientId { get; }

        public List<Activity> Activities { get; }

        public override string ToString()
        {
            return $"[Release]:[ID: {Identity}]";
        }

        private Release(EntityId id, EntityId clientId, VersionId version)
            : base(id, version)
        {
            this.ClientId = clientId;
        }

        public static Release From(EntityId id, EntityId clientId, VersionId version)
        {
            var release = new Release(id, clientId, version);
            //var validator = new ReleaseValidator();
            //release.SetValidationResult(validator.Validate(release));
            return release;
        }

        public static Release NewRequest(EntityId id, EntityId clientId)
        {
            return From(id, clientId, VersionId.New());
        }

        public Release AddActivity(Activity activity)
        {
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
        }
    }
}

