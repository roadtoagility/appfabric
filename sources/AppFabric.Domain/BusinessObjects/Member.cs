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

namespace AppFabric.Domain.BusinessObjects
{
    public class Member : ValidationStatus
    {
        public EntityId2 Id { get; }
        public EntityId2 ProjectId { get; }
        public string Name { get; }

        private Member(EntityId2 id, EntityId2 projectId, string name)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
        }

        public static Member From(EntityId2 id, EntityId2 projectId, string name)
        {
            var member = new Member(id, projectId, name);
            var validator = new MemberValidator();

            member.SetValidationResult(validator.Validate(member));

            return member;
        }

        public static Member Empty()
        {
            var member = new Member(EntityId2.From(Guid.Empty), EntityId2.From(Guid.Empty), string.Empty);

            var validator = new MemberValidator();

            member.SetValidationResult(validator.Validate(member));
            return member;
        }

        public void Update(Member member)
        {
            this = member;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        #region IEquatable

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
        #endregion
    }
}
