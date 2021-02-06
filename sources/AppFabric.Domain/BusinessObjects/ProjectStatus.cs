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
using AppFabric.Domain.Framework.Validation;

namespace AppFabric.Domain.BusinessObjects
{
    public sealed class ProjectStatus :ValidationStatus
    {
        public enum Status
        {
            ToAprove,
            Aproved,
            Finished
        }

        private readonly Status _status;

        public int Value { get; }
        
        private ProjectStatus(Status status)
        {
            _status = status;
            Value = (int) status;
        }

        public static ProjectStatus From(int status)
        {
            var ps = new ProjectStatus((Status) status);
            var validator = new ProjectStatusValidator();

            ps.SetValidationResult(validator.Validate(ps));
            
            return ps;
        }
        
        public static ProjectStatus Default()
        {
            return new ProjectStatus(Status.ToAprove);
        }

        public override string ToString()
        {
            return $"{_status.ToString()}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _status;
        }

        #region IComparable

        public int CompareTo(ProjectStatus other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return _status.CompareTo(other._status);
        }

        #endregion
    }
}