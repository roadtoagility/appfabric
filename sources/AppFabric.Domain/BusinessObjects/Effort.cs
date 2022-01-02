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

using AppFabric.Domain.BusinessObjects.Validations;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.BusinessObjects
{
    public class Effort : ValueOf<int, Effort, EffortValidator>
    {
        private const int UnEstimatedEffort = -1;
        private const int NoEffort = 0;
        private const int MaximumEffort = 8;

        public static Effort UnEstimated()
        {
            return From(UnEstimatedEffort);
        }

        public static Effort Zero()
        {
            return From(NoEffort);
        }

        public static Effort MaxEffort()
        {
            return From(MaximumEffort);
        }


        public void Update(int hours)
        {
            Value = hours;
        }

        protected override void Validate()
        {
        }

        public static bool operator >=(Effort a, Effort b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(Effort a, Effort b)
        {
            return a.Value <= b.Value;
        }

        public static bool operator >(Effort a, Effort b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(Effort a, Effort b)
        {
            return a.Value < b.Value;
        }
    }
}