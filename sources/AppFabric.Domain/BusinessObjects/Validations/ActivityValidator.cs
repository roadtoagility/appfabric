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
using AppFabric.Domain.Framework.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.BusinessObjects.Validations
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(activity => activity).Must(CloseOnlyWithoutEffort);
            RuleFor(activity => activity.Effort).Custom((effort, context) =>
            {
                if (effort.Value > 8)
                {
                    context.AddFailure("Uma atividade não pode ter esforço maior do que 8 horas");
                }
            });
            RuleFor(activity => activity).Custom((activity, context) =>
            {
                if (activity.ActivityStatus.Value == (int)ActivityStatus.Status.Closed && activity.Effort.Value > 0)
                {
                    context.AddFailure("Não é possível fechar uma atividade com esforço pendente");
                }

                if(activity.Responsible.ProjectId.Value != Guid.Empty && activity.ProjectId != activity.Responsible.ProjectId)
                {
                    context.AddFailure("Só é possível adicionar como responsável membros do projeto");
                }
            });
        }

        private bool CloseOnlyWithoutEffort(Activity activity)
        {
            if (activity.ActivityStatus == ActivityStatus.From(2) && activity.Effort.Value > 0)
                return false;
            return true;
        }
    }
}
