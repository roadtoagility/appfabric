using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Specifications;
using AppFabric.Domain.AggregationBilling;
using AppFabric.Domain.AggregationBilling.Specifications;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Specifications;
using AppFabric.Domain.AggregationRelease;
using AppFabric.Domain.AggregationRelease.Specifications;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.AggregationUser.Specifications;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using System;
using System.Collections.Immutable;
using AppFabric.Domain.BusinessObjects.Validations.ActivityRules;

namespace AppFabric.Business
{
    public class AggregateFactory : IAggregateFactory<ActivityAggregationRoot, Activity>,
        IAggregateFactory<ActivityAggregationRoot, CreateActivityCommand>,
        IAggregateFactory<BillingAggregationRoot, CreateBillingCommand>,
        IAggregateFactory<BillingAggregationRoot, Billing>,
        IAggregateFactory<UserAggregationRoot, AddUserCommand>,
        IAggregateFactory<UserAggregationRoot, User>,
        IAggregateFactory<ProjectAggregationRoot, AddProjectCommand>,
        IAggregateFactory<ProjectAggregationRoot, Project>
    {
        public ActivityAggregationRoot Create(Activity source)
        {
            var activitySpec = new ActivitySpecification()
                .And(new ActivityClosedSpecification())
                .And(new ActivityCloseWithoutEffortSpecification())
                .And(new ActivityEffortSpecification())
                .And(new ActivityResponsibleSpecification());

            if (activitySpec.IsSatisfiedBy(source))
            {
                return new ActivityAggregationRoot(source);
            }
            throw new Exception("Invalid Command");
        }

        public ActivityAggregationRoot Create(CreateActivityCommand source)
        {
            var activity = Activity.New(source.ProjectId, source.EstimatedHours);
            var newActivitySpec = new ActivityCreationSpecification();

            if (newActivitySpec.IsSatisfiedBy(activity) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new ActivityAggregationRoot(activity);
        }

        public BillingAggregationRoot Create(CreateBillingCommand source)
        {
            // TODO: cadê pelo menos uma release para faturar???
            var billing = Billing.NewRequest(null);

            var newBillingSpec = new BillingCreationSpecification();

            if (newBillingSpec.IsSatisfiedBy(billing) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            return new BillingAggregationRoot(billing);
        }

        public BillingAggregationRoot Create(Billing source)
        {
            var billingSpec = new BillingCreationSpecification();

            if (billingSpec.IsSatisfiedBy(source) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            return new BillingAggregationRoot(source);
        }

        public ReleaseAggregationRoot Create(CreateReleaseCommand source)
        {
            var release = Release.NewRequest(EntityId.From(source.ClientId));
            var newReleaseSpec = new ReleaseCreationSpecification();

            if (newReleaseSpec.IsSatisfiedBy(release) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new ReleaseAggregationRoot(release);
        }

        public ReleaseAggregationRoot Create(Release source)
        {
            var releaseSpec = new ReleaseSpecification();

            if (releaseSpec.IsSatisfiedBy(source) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new ReleaseAggregationRoot(source);
        }

        public UserAggregationRoot Create(AddUserCommand source)
        {
            var newUserSpec = new UserCreationSpecification();
            var userSpec = new UserSpecification();

            var name = Name.From(source.Name);
            var cnpj = SocialSecurityId.From(source.Cnpj);
            var email = Email.From(source.CommercialEmail);
            var user = User.NewRequest(EntityId.GetNext(), name, cnpj, email, VersionId.New());

            if (newUserSpec.IsSatisfiedBy(user))
            {
                return new UserAggregationRoot(userSpec, user);
            }
            throw new Exception("Invalid Command");
        }

        public UserAggregationRoot Create(User source)
        {
            var userSpec = new UserSpecification();

            if (userSpec.IsSatisfiedBy(source))
            {
                return new UserAggregationRoot(userSpec, source);
            }
            throw new Exception("Invalid Command");
        }

        public ProjectAggregationRoot Create(AddProjectCommand command)
        {
            var newProjectSpec = new ProjectCreationSpecification();

            var project = Project.NewRequest(
                command.Name,
                command.ServiceOrderNumber,
                command.Status,
                command.Code,
                command.StartDate,
                command.Budget,
                command.ClientId,
                command.Owner);

            if (newProjectSpec.IsSatisfiedBy(project) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new ProjectAggregationRoot(project);
        }

        public ProjectAggregationRoot Create(Project source)
        {
            var projectSpec = new ProjectSpecification();

            if (projectSpec.IsSatisfiedBy(source) == false)
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new ProjectAggregationRoot(source);
        }
    }

    

    
}
