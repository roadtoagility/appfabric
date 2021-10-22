using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Domain.AggregationProject.Specifications
{
    public class ProjectSpecification : CompositeSpecification<Project>
    {
        public override bool IsSatisfiedBy(Project candidate)
        {
            ////RuleFor(project => project.Id).SetValidator(new EntityIdValidator());
            //RuleFor(project => project.Name).SetValidator(new ProjectNameValidator());
            //RuleFor(project => project.StartDate).SetValidator(new DateAndTimeValidator());
            ////RuleFor(project => project.ClientId).SetValidator(new EntityIdValidator());

            //RuleFor(project => project.Code).SetValidator(new ProjectCodeValidator())
            //    .DependentRules(() =>
            //    {
            //        RuleFor(current => current.Code).Custom((code, context) =>
            //        {
            //            if (context.ParentContext.RootContextData.ContainsKey("project"))
            //            {
            //                var changedProject = context.ParentContext.RootContextData["project"] as Project;

            //                if (code.Equals(changedProject?.Code))
            //                {
            //                    context.AddFailure("O código do novo projeto não pode ser igual ao atual.");
            //                }
            //            }
            //        });
            //    });

            //RuleFor(project => project.Budget).SetValidator(new MoneyValidator());
            //RuleFor(project => project.Status).SetValidator(new ProjectStatusValidator())
            //    .When(status => !status.Equals(ProjectStatus.Default()));

            //RuleFor(project => project.Owner).SetValidator(new EmailValidator())
            //    .When(project => !project.Owner.Equals(Email.Empty()));

            //RuleFor(project => project.OrderNumber).SetValidator(new ServiceOrderNumberValidator())
            //    .DependentRules(() =>
            //    {
            //        RuleFor(current => current.OrderNumber).Custom((serviceOrder, context) =>
            //        {
            //            if (!serviceOrder.IsAproved)
            //            {
            //                context.AddFailure("A ordem de serviço precisa estar aprovada");
            //            }
            //        });
            //    });

            throw new NotImplementedException();
        }
    }
}
