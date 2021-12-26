// Copyright (C) 2021  Road to Agility
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
using AppFabric.Business.CommandHandlers;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.CommandHandlers.Factories;
using AppFabric.Domain.AggregationActivity;
using AppFabric.Domain.AggregationActivity.Specifications;
using AppFabric.Domain.AggregationBilling;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationRelease;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.BusinessObjects.Validations.ActivityRules;
using DFlow.Domain.Aggregates;
using Microsoft.Extensions.DependencyInjection;

namespace AppFabric.Business
{
    public static class BusinessServiceCollectionExtensions
    {
        public static void AddAggregationFactories(this IServiceCollection services)
        {
            //activity
            services
                .AddScoped<IAggregateFactory<ActivityAggregationRoot, CreateActivityCommand>, ActivityCreateAggregateFactory>();
            services
                .AddScoped<IAggregateFactory<ActivityAggregationRoot, Activity>, ActivityReconstructAggregateFactory>();
            
            //billing
            services
                .AddScoped<IAggregateFactory<BillingAggregationRoot, CreateBillingCommand>, BillingAggregateFactory>();
            services
                .AddScoped<IAggregateFactory<BillingAggregationRoot, Billing>, BillingAggregateFactory>();
        
            //project
            services
                .AddScoped<IAggregateFactory<ProjectAggregationRoot, AddProjectCommand>, ProjectAggregateFactory>();
            services
                .AddScoped<IAggregateFactory<ProjectAggregationRoot, Project>, ProjectAggregateFactory>();
            
            //release
            services
                .AddScoped<IAggregateFactory<ReleaseAggregationRoot, CreateReleaseCommand>, ReleaseAggregateFactory>();
            services
                .AddScoped<IAggregateFactory<ReleaseAggregationRoot, Release>, ReleaseAggregateFactory>();
            
            //user
            services
                .AddScoped<IAggregateFactory<UserAggregationRoot, AddUserCommand>, UserAggregateFactory>();
            services
                .AddScoped<IAggregateFactory<UserAggregationRoot, User>, UserAggregateFactory>();
        }

        public static void AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<CreateActivityCommandHandler>();
            services.AddScoped<CloseActivityCommandHandler>();
            services.AddScoped<AddActivityCommandHandler>();

            services.AddScoped<AssignResponsibleCommandHandler>();
            
            services.AddScoped<CreateBillingCommandHandler>();

            services.AddScoped<CreateProjectCommandHandler>();

            services.AddScoped<AddReleaseCommandHandler>();
            services.AddScoped<CreateReleaseCommandHandler>();
            
            services.AddScoped<AddUserCommandHandler>();
            services.AddScoped<RemoveUserCommandHandler>();

        }
    }
}
