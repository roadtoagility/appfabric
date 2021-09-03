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
    public class Activity : ValidationStatus
    {
        public EntityId Id { get; }
        public EntityId ProjectId { get; }

        public Effort Effort { get; private set; }

        public ActivityStatus ActivityStatus { get; private set; }
        public Member Responsible { get; private set; }

        public Version Version { get; }

        public bool IsNew() => Version.Value == 1;

        public override string ToString()
        {
            return $"[Activity]:[ID: {Id}]";
        }

        private Activity(EntityId id, EntityId projectId, Version version)
        {
            this.Id = id;
            this.Version = version;
            this.ProjectId = projectId;
            this.Effort = Effort.From(0);
            this.ActivityStatus = ActivityStatus.From(0);
            this.Responsible = Member.Empty();
        }

        public static Activity From(EntityId id, EntityId projectId, int hours, Version version)
        {
            var activity = new Activity(id, projectId, version);
            activity.UpdateEffort(hours);
            var validator = new ActivityValidator();
            activity.SetValidationResult(validator.Validate(activity));
            return activity;
        }

        public static Activity NewRequest(EntityId id, EntityId projectId, int hours)
        {
            return From(id, projectId, hours, Version.New());
        }

        public Activity AddMember(Member member)
        {
            Responsible = member;

            var validator = new ActivityValidator();
            var result = validator.Validate(this);
            this.ValidationResults = result;

            return this;
        }

        public Activity UpdateEffort(int hours)
        {
            Effort.Update(hours);

            var validator = new ActivityValidator();
            var result = validator.Validate(this);
            this.ValidationResults = result;

            return this;
        }

        public Activity Close()
        {
            ActivityStatus.Close();

            var validator = new ActivityValidator();
            var result = validator.Validate(this);
            this.ValidationResults = result;

            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
