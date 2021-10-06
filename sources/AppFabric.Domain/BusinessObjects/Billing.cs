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
    public class Billing : BaseEntity<EntityId2>
    {
        public List<Release> Releases { get; private set; }

        public override string ToString()
        {
            return $"[Billing]:[ID: {Identity}]";
        }

        private Billing(EntityId2 id, VersionId version)
            : base(id, version)
        {
            this.Releases = new List<Release>();
        }

        public static Billing From(EntityId2 id, VersionId version)
        {
            var billing = new Billing(id, version);
            //var validator = new BillingValidator();
            //billing.SetValidationResult(validator.Validate(billing));
            return billing;
        }

        public static Billing NewRequest(EntityId2 id)
        {
            return From(id, VersionId.New());
        }

        public Billing AddRelease(Release release)
        {
            Releases.Add(release);

            //var validator = new BillingValidator();
            //var result = validator.Validate(this);
            //this.ValidationResults = result;

            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
        }
    }
}


