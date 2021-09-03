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
using AppFabric.Domain.Framework.Validation;

namespace AppFabric.Domain.BusinessObjects
{
    public sealed class ServiceOrder : ValidationStatus
    {
        public string Number { get; }
        public bool IsAproved { get; }
        
        
        private ServiceOrder(string name, bool isAproved)
        {
            Number = name;
            IsAproved = isAproved;
        }

        public static ServiceOrder From(string name, bool isAproved)
        {
            var son = new ServiceOrder(name, isAproved);
            var validator = new ServiceOrderNumberValidator();

            son.SetValidationResult(validator.Validate(son));
            
            return son;
        }

        public static ServiceOrder Empty()
        {
            var son = new ServiceOrder(String.Empty, false);
            return son;
        }
        
        public override string ToString()
        {
            return $"{Number}";
        }

        #region IEquatable

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
        #endregion
    }
}