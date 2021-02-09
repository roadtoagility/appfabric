﻿// Copyright (C) 2020  Road to Agility
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
    public sealed class ProjectCode : ValidationStatus
    {
        public string Value { get; }
        
        private ProjectCode(string code)
        {
            Value = code;
        }

        public static ProjectCode From(string code)
        {
            var projectCode = new ProjectCode(code);
            var validator = new ProjectCodeValidator();

            projectCode.SetValidationResult(validator.Validate(projectCode));
            
            return projectCode;
        }
        public static ProjectCode Empty()
        {
            return From(String.Empty);
        }
        
        public override string ToString()
        {
            return $"{Value}";
        }

        #region IEquatable

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #endregion
    }
}