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

using FluentValidation;
using AppFabric.Domain.Framework.Validation;

namespace AppFabric.Domain.BusinessObjects.Validations
{
    public sealed class ProjectValidator: AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(project => project.Id).SetValidator(new EntityIdValidator());
            RuleFor(project => project.Name).SetValidator(new ProjectNameValidator());
            RuleFor(project => project.StartDate).SetValidator(new DateAndTimeValidator());
            RuleFor(project => project.ClientId).SetValidator(new EntityIdValidator());

            RuleFor(project => project.Code).SetValidator(new ProjectCodeValidator())
                .DependentRules(() =>
                {
                    RuleFor(current => current.Code).Custom((code, context) =>
                    {
                        if (context.ParentContext.RootContextData.ContainsKey("project"))
                        {
                            var changedProject = context.ParentContext.RootContextData["project"] as Project;

                            if (code.Equals(changedProject?.Code))
                            {
                                context.AddFailure("O código do novo projeto não pode ser igual ao atual.");
                            }
                        }
                    });
                });
            
            RuleFor(project => project.Budget).SetValidator(new MoneyValidator());
            RuleFor(project => project.Status).SetValidator(new ProjectStatusValidator())
                .When(status=> !status.Equals(ProjectStatus.Default()));
            
            RuleFor(project => project.Owner).SetValidator(new EmailValidator())
                .When(project => !project.Owner.Equals(Email.Empty()));
            
            RuleFor(project => project.OrderNumber).SetValidator(new ServiceOrderNumberValidator())
                .When(order => !order.OrderNumber.Equals(ServiceOrderNumber.Empty()));

        }
    }
}