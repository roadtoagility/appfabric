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
using FluentValidation;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;

namespace TodoAgility.Agile.Domain.AggregationActivity.Validators
{
    public sealed class ActivityValidator: AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(activity => activity.Description).SetValidator(new DescriptionValidator())
                .DependentRules(() =>
                {
                    RuleFor(current => current.Description).Custom((description, context) => {
                        if(context.ParentContext.RootContextData.ContainsKey("activity"))
                        {
                            var changedActivity = context.ParentContext.RootContextData["activity"] as Activity;

                            if (changedActivity?.Description == description)
                            {
                                context.AddFailure("A nova descrição não pode ser igual a atual.");
                            }
                        }
                    });
                });
            
            RuleFor(activity => activity.Id).SetValidator(new EntityIdValidator());
            RuleFor(activity => activity.Status).SetValidator(new ActivityStatusValidator());
            RuleFor(activity => activity.ProjectId).SetValidator(new EntityIdValidator());
        }
    }
}