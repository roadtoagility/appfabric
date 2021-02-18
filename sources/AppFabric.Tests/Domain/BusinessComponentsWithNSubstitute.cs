﻿// Copyright (C) 2020  Road to Agility
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
using System.Globalization;
using System.Threading;
using AppFabric.Business.CommandHandlers;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.AggregationUser.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentMediator;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;
using Xunit.Gherkin.Quick;
using Version = AppFabric.Domain.BusinessObjects.Version;

namespace AppFabric.Tests.Domain
{
    public sealed class BusinessComponentsWithNSubstitute
    {
        [Fact]
        public void user_add_command_succed()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization{ ConfigureMembers = true });
            
            fixture.Register<EntityId>(() => EntityId.From(fixture.Create<Guid>()));
            fixture.Register<Name>(() => Name.From(fixture.Create<string>()));
            fixture.Register<Version>(() => Version.From(fixture.Create<int>()));
            fixture.Register<SocialSecurityId>(() => SocialSecurityId.From(fixture.Create<string>()));
            fixture.Register<Email>(() => Email.From(string.Format($"{fixture.Create<string>()}@teste.com")));
            fixture.Register<User>(() => User.From(fixture.Create<EntityId>(),
                fixture.Create<Name>(),fixture.Create<SocialSecurityId>(),
                fixture.Create<Email>(), fixture.Create<Version>()));

            var finalUser = fixture.Create<User>();

            var command = fixture.Build<AddUserCommand>()
                .With(user => user.CommercialEmail, finalUser.CommercialEmail.Value)
                .With(user => user.Cnpj, finalUser.Cnpj.Value)
                .With(user => user.Name, finalUser.Name.Value)
                .Create();

            var mediator = fixture.Create<IMediator>();
            var logger = fixture.Create<ILogger<AddUserCommandHandler>>();
            var db = fixture.Create<IDbSession<IUserRepository>>();
            var handler = new AddUserCommandHandler(logger,mediator,db);

            var result = handler.Execute(command);
            
            db.Received().Repository.Add(finalUser);
            db.Received().SaveChanges();
            mediator.Received(1).Publish(Arg.Any<UserAddedEvent>());
            
            Assert.True(result.IsSucceed);
        }
        
        [Fact]
        public void user_add_command_failed()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization{ ConfigureMembers = true });
            
            var finalUser = fixture.Create<User>();
            var command = fixture.Build<AddUserCommand>()
                .With(user => user.CommercialEmail, finalUser.CommercialEmail.Value)
                .With(user => user.Cnpj, finalUser.Cnpj.Value)
                .With(user => user.Name, finalUser.Name.Value)
                .Create();
            
            var mediator = fixture.Create<IMediator>();
            var logger = fixture.Create<ILogger<AddUserCommandHandler>>();
            var db = fixture.Create<IDbSession<IUserRepository>>();
            var handler = new AddUserCommandHandler(logger, mediator,db);


            var result = handler.Execute(command);
            db.DidNotReceive().Repository.Add(finalUser);
            db.DidNotReceive().SaveChanges();
            
            Assert.True(!result.IsSucceed && result.Violations.Count == 3);
        } 
        
        [Fact]
        public void project_add_command_succed()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization{ ConfigureMembers = true });
            
            fixture.Register<EntityId>(() => EntityId.From(fixture.Create<Guid>()));
            fixture.Register<ProjectName>(() => ProjectName.From(fixture.Create<string>()));
            fixture.Register<ProjectCode>(() => ProjectCode.From(fixture.Create<string>()));
            fixture.Register<DateAndTime>(() => DateAndTime.From(fixture.Create<DateTime>()));
            fixture.Register<ProjectStatus>(() => ProjectStatus.Default());
            fixture.Register<ServiceOrderNumber>(() => ServiceOrderNumber.Empty());
            fixture.Register<Email>(() => Email.From(string.Format($"{fixture.Create<string>()}@teste.com")));
            fixture.Register<Project>(() => Project.NewRequest(fixture.Create<EntityId>(),
                fixture.Create<ProjectName>(),fixture.Create<ProjectCode>(),
                fixture.Create<DateAndTime>(), fixture.Create<Money>(), fixture.Create<EntityId>()));
            fixture.Register<User>(() => User.From(fixture.Create<EntityId>(),
                fixture.Create<Name>(),fixture.Create<SocialSecurityId>(),
                fixture.Create<Email>(), fixture.Create<Version>()));
            
            var finalProject = fixture.Create<Project>();
            var finalClient = fixture.Create<User>();

            var command = fixture.Build<AddProjectCommand>()
                .With(project => project.Name, finalProject.Name.Value)
                .With(project => project.Code, finalProject.Code.Value)
                .With(project => project.Budget, finalProject.Budget.Value)
                .With(project => project.ClientId, finalProject.ClientId.Value)
                .With(project => project.StartDate, finalProject.StartDate.Value)
                .Create();

            var mediator = fixture.Create<IMediator>();
            var logger = fixture.Create<ILogger<AddProjectCommandHandler>>();
            var projectDb = fixture.Create<IDbSession<IProjectRepository>>();
            var userDb = fixture.Create<IDbSession<IUserRepository>>();
            var handler = new AddProjectCommandHandler(logger,mediator,projectDb,userDb);

            var result = handler.Execute(command);
            
            userDb.Received().Repository.Get(finalProject.ClientId).Returns(finalClient);
            projectDb.Received().Repository.Add(finalProject);
            projectDb.Received().SaveChanges();
            mediator.Received(1).Publish(Arg.Any<ProjectAddedEvent>());
            
            Assert.True(result.IsSucceed);
        }
        
        [Fact]
        public void user_project_command_failed()
        {
            //given
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization{ ConfigureMembers = true });
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(long.MinValue,-1));
            fixture.Register<Money>(() => Money.From(fixture.Create<Decimal>()));
            
            fixture.Register<EntityId>(() => EntityId.Empty());
            fixture.Register<ProjectName>(() => ProjectName.Empty());
            fixture.Register<ProjectCode>(() => ProjectCode.Empty());
            fixture.Register<DateAndTime>(() => DateAndTime.Empty());
            fixture.Register<Money>(() => fixture.Create<Money>());
            
            var command = new AddProjectCommand();
            
            var mediator = fixture.Create<IMediator>();
            var logger = fixture.Create<ILogger<AddProjectCommandHandler>>();
            var projectDb = fixture.Create<IDbSession<IProjectRepository>>();
            var userDb = fixture.Create<IDbSession<IUserRepository>>();

            // when 
            var handler = new AddProjectCommandHandler(logger,mediator,projectDb,userDb);

            var result = handler.Execute(command);

            // then
            Assert.True(!result.IsSucceed && result.Violations.Count == 4);
        }
    }
}