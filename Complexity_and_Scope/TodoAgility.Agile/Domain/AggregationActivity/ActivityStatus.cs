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
using TodoAgility.Agile.Domain.AggregationActivity.Validators;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.Validation;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.AggregationActivity
{
    public sealed class ActivityStatus :ValidationStatus, IExposeValue<int>
    {
        public enum Status
        {
            Created,
            Started,
            Completed
        }

        private readonly Status _status;

        public int Value { get; }
        
        private ActivityStatus(Status status)
        {
            _status = status;
            Value = (int) status;
        }

        int IExposeValue<int>.GetValue()
        {
            return (int) _status;
        }

        public static ActivityStatus From(int status)
        {
            var activity = new ActivityStatus((Status) status);
            var validator = new ActivityStatusValidator();

            activity.SetValidationResult(validator.Validate(activity));
            
            return activity;
        }

        public override string ToString()
        {
            return $"{_status}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _status;
        }

        #region IComparable

        public int CompareTo(ActivityStatus other)
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