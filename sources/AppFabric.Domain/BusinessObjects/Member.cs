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
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Domain.Framework.Validation;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.BusinessObjects
{
    public class Member : BaseEntity<EntityId2>
    {
        public EntityId2 ProjectId { get; private set; }
        public string Name { get; private set; }

        private Member(EntityId2 id, EntityId2 projectId, string name, VersionId version)
            : base(id, version)
        {
            ProjectId = projectId;
            Name = name;
        }

        public static Member From(EntityId2 id, EntityId2 projectId, string name, VersionId version)
        {
            var member = new Member(id, projectId, name, version);
            var validator = new MemberValidator();

            //member.SetValidationResult(validator.Validate(member));

            return member;
        }

        public static Member Empty()
        {
            var member = new Member(EntityId2.From(Guid.Empty), EntityId2.From(Guid.Empty), string.Empty, VersionId.Empty());

            var validator = new MemberValidator();

            //member.SetValidationResult(validator.Validate(member));
            return member;
        }

        public void Update(Member member)
        {
            //this.Identity = member.Id;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        #region IEquatable

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
        }
        #endregion
    }
}
