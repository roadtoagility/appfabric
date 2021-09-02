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

namespace AppFabric.Domain.BusinessObjects
{
    public class Billing : ValidationStatus
    {
        public EntityId Id { get; }

        public List<Release> Releases { get; }

        public Version Version { get; }

        public bool IsNew() => Version.Value == 1;

        public override string ToString()
        {
            return $"[Billing]:[ID: {Id}]";
        }

        private Billing(EntityId id, Version version)
        {
            this.Id = id;
            this.Version = version;
        }

        public static Billing From(EntityId id, Version version)
        {
            var billing = new Billing(id, version);
            var validator = new BillingValidator();
            billing.SetValidationResult(validator.Validate(billing));
            return billing;
        }

        public static Billing NewRequest(EntityId id)
        {
            return From(id, Version.New());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}


