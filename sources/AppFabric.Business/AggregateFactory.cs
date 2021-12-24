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
                return new ActivityAggregationRoot(activitySpec, source);
            }
            throw new Exception("Invalid Command");
        }

        public ActivityAggregationRoot Create(CreateActivityCommand source)
        {
            var activity = Activity.From(EntityId.GetNext(), source.ProjectId, source.EstimatedHours, VersionId.New());
            var newActivitySpec = new ActivityCreationSpecification();
            var activitySpec = new ActivitySpecification();

            if (newActivitySpec.IsSatisfiedBy(activity))
            {
                return new ActivityAggregationRoot(activitySpec, activity);
            }
            throw new Exception("Invalid Command");
        }

        public BillingAggregationRoot Create(CreateBillingCommand source)
        {
            var billing = Billing.From(EntityId.GetNext(), VersionId.New());

            var newBillingSpec = new BillingCreationSpecification();
            var billingSpec = new BillingSpecification();

            if (newBillingSpec.IsSatisfiedBy(billing))
            {
                return new BillingAggregationRoot(billingSpec, billing);
            }
            throw new Exception("Invalid Command");
        }

        public BillingAggregationRoot Create(Billing source)
        {
            var billingSpec = new BillingSpecification();

            if (billingSpec.IsSatisfiedBy(source))
            {
                return new BillingAggregationRoot(billingSpec, source);
            }
            throw new Exception("Invalid Command");
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
