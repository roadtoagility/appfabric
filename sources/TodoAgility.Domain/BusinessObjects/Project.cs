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
using TodoAgility.Domain.BusinessObjects.Validations;
using TodoAgility.Domain.Framework.Validation;

namespace TodoAgility.Domain.BusinessObjects
{
    public sealed class Project : ValidationStatus
    {
        private Project(ProjectName name, ProjectCode code, DateAndTime startDate)
        {
            Name = name;
            Code = code;
            StartDate = startDate;
        }

        public ProjectName Name { get; }
        public ProjectCode Code { get; }

        public DateAndTime StartDate { get; }
        
        public static Project From(ProjectName name, ProjectCode code, DateAndTime startDate)
        {
            var project = new Project(name, code, startDate);
            var validator = new ProjectValidator();
            project.SetValidationResult(validator.Validate(project));
            return project;        
        }
        
        public override string ToString()
        {
            return $"[PROJECT]:[Code:{Code}, Name: {Name}, Start date: {StartDate}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
            yield return Name;
            yield return StartDate;
        }
    }
}