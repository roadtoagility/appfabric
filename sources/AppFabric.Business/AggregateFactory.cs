﻿using AppFabric.Business.CommandHandlers.Commands;
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
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Command;
using DFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFabric.Business
{
    public class AggregateFactory : IAggregateFactory<ActivityAggregationRoot, LoadActivityCommand>,
        IAggregateFactory<ActivityAggregationRoot, CreateActivityCommand>,
        IAggregateFactory<BillingAggregationRoot, CreateBillingCommand>,
        IAggregateFactory<BillingAggregationRoot, LoadBillingCommand>,
        IAggregateFactory<UserAggregationRoot, AddUserCommand>,
        IAggregateFactory<UserAggregationRoot, LoadUserCommand>,
        IAggregateFactory<ProjectAggregationRoot, AddProjectCommand>,
        IAggregateFactory<ProjectAggregationRoot, LoadProjectCommand>
    {
        public ActivityAggregationRoot Create(LoadActivityCommand source)
        {
            var activitySpec = new ActivitySpecification();

            if (activitySpec.IsSatisfiedBy(source.Activity))
            {
                return ActivityAggregationRoot.ReconstructFrom(source.Activity, activitySpec);
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
                return ActivityAggregationRoot.ReconstructFrom(activity, activitySpec);
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
                return BillingAggregationRoot.ReconstructFrom(billing, billingSpec);
            }
            throw new Exception("Invalid Command");
        }

        public BillingAggregationRoot Create(LoadBillingCommand source)
        {
            var billingSpec = new BillingSpecification();

            if (billingSpec.IsSatisfiedBy(source.Billing))
            {
                return BillingAggregationRoot.ReconstructFrom(source.Billing, billingSpec);
            }
            throw new Exception("Invalid Command");
        }

        public ReleaseAggregationRoot Create(CreateReleaseCommand source)
        {
            var release = Release.From(EntityId.GetNext(), EntityId.From(source.ClientId), VersionId.New());
            var newReleaseSpec = new ReleaseCreationSpecification();
            var releaseSpec = new ReleaseSpecification();

            if (newReleaseSpec.IsSatisfiedBy(release))
            {
                return ReleaseAggregationRoot.ReconstructFrom(release, releaseSpec);
            }
            throw new Exception("Invalid Command");
        }

        public ReleaseAggregationRoot Create(LoadReleaseCommand source)
        {
            var releaseSpec = new ReleaseSpecification();

            if (releaseSpec.IsSatisfiedBy(source.Release))
            {
                return ReleaseAggregationRoot.ReconstructFrom(source.Release, releaseSpec);
            }
            throw new Exception("Invalid Command");
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
                return UserAggregationRoot.ReconstructFrom(user, userSpec);
            }
            throw new Exception("Invalid Command");
        }

        public UserAggregationRoot Create(LoadUserCommand source)
        {
            var userSpec = new UserSpecification();

            if (userSpec.IsSatisfiedBy(source.User))
            {
                return UserAggregationRoot.ReconstructFrom(source.User, userSpec);
            }
            throw new Exception("Invalid Command");
        }

        public ProjectAggregationRoot Create(AddProjectCommand command)
        {
            var newProjectSpec = new ProjectCreationSpecification();
            var projectSpec = new ProjectSpecification();

            //TODO: update to get ServiceOrder and ProjectStatus from command
            var project = Project.NewRequest(EntityId.GetNext(), 
                ProjectName.From(command.Name),
                ServiceOrder.From(command.ServiceOrder, true),
                ProjectStatus.From(command.Status),
                ProjectCode.From(command.Code),
                DateAndTime.From(command.StartDate),
                Money.From(command.Budget),
                EntityId.From(command.ClientId));

            if (newProjectSpec.IsSatisfiedBy(project))
            {
                return ProjectAggregationRoot.ReconstructFrom(project, projectSpec);
            }
            throw new Exception("Invalid Command");
        }

        public ProjectAggregationRoot Create(LoadProjectCommand source)
        {
            var projectSpec = new ProjectSpecification();

            if (projectSpec.IsSatisfiedBy(source.Project))
            {
                return ProjectAggregationRoot.ReconstructFrom(source.Project, projectSpec);
            }
            throw new Exception("Invalid Command");
        }
    }

    

    
}