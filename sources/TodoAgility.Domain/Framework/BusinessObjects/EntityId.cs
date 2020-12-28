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
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Domain.Framework.Validation;

namespace TodoAgility.Domain.Framework.BusinessObjects
{
    public sealed class EntityId : ValidationStatus, IExposeValue<uint>
    {
        private readonly uint _id;

        public uint Value { get; }
        
        private EntityId(uint id)
        {
            _id = id;
            Value = _id;
        }

        uint IExposeValue<uint>.GetValue()
        {
            return _id;
        }

        public static EntityId From(uint id)
        {
            var entityId = new EntityId(id);
            var validator = new EntityIdValidator();

            entityId.SetValidationResult(validator.Validate(entityId));
            
            return entityId;
        }

        public static EntityId GetNext()
        {
            return From((uint)DateTime.UnixEpoch.Millisecond);
        }
        public override string ToString()
        {
            return $"{_id}";
        }

        #region IEquatable

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }

        #endregion

        #region IComparable

        public int CompareTo(EntityId other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return _id.CompareTo(other._id);
        }

        #endregion
    }
}