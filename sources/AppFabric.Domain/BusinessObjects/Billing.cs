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
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.BusinessObjects
{
    public class Billing : BaseEntity<EntityId>
    {
        public List<Release> Releases { get; private set; }

        public override string ToString()
        {
            return $"[Billing]:[ID: {Identity}]";
        }

        private Billing(EntityId id, IReadOnlyList<Release> release, VersionId version)
            : base(id, version)
        {
            Releases = new List<Release>();
            Releases.AddRange(release);
        }

        public static Billing From(EntityId id, IReadOnlyList<Release> releases, VersionId version)
        {
            var billing = new Billing(id, releases, version);
            return billing;
        }

        public static Billing NewRequest(IReadOnlyList<Release> releases)
        {
            return From(EntityId.GetNext(), releases, VersionId.New());
        }

        public Billing AddRelease(Release release)
        {
            Releases.Add(release);
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
        }
    }
}


