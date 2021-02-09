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
using System.Globalization;
using System.Threading;
using AppFabric.Business.CommandHandlers;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Business.Framework;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Persistence.Framework;
using AppFabric.Persistence.Model.Repositories;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentMediator;
using FluentValidation.Results;
using NSubstitute;
using Xunit;
using Xunit.Gherkin.Quick;

namespace AppFabric.Tests.Domain
{
    public sealed class BusinessComponentsWithNSubstitute
    {
      
        
        [Fact]
        public void user_add_command()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization{ ConfigureMembers = true });
            
            fixture.Register<EntityId>(() => EntityId.From(fixture.Create<Guid>()));
            fixture.Register<Name>(() => Name.From(fixture.Create<string>()));
            fixture.Register<SocialSecurityId>(() => SocialSecurityId.From(fixture.Create<string>()));
            fixture.Register<Email>(() => Email.From(string.Format($"{fixture.Create<string>()}@teste.com")));
            fixture.Register<User>(() => User.From(fixture.Create<EntityId>(),
                fixture.Create<Name>(), fixture.Create<SocialSecurityId>(), fixture.Create<Email>()));

            fixture.Register<UserAggregationRoot>(
                ()=> UserAggregationRoot.ReconstructFrom(fixture.Create<User>()));
            
            var command = fixture.Build<AddUserCommand>()
                .With(user => user.CommercialEmail, string.Format($"{fixture.Create<string>()}@teste.com"))
                .Create();
            
            var mediator = Substitute.For<IMediator>();
            var repo = Substitute.For<IUserRepository>();
            var db = Substitute.For<IDbSession<IUserRepository>>();

            var handler = new AddUserCommandHandler(mediator,db);

            var result = handler.Execute(command);
            
            Assert.True(result.IsSucceed);
        }
        
        [Fact]
        public void money_create_zero()
        {
            var fixture = new Fixture();
            fixture.Register<Money>(() => Money.Zero());

            var money = fixture.Create<Money>();
            
            Assert.True(money.ValidationResults.IsValid);
        }

        [Fact]
        public void money_equality_valid_quantity_of()
        {
            var fixture = new Fixture();
            fixture.Register<Money>(() => Money.From(1));

            var money = fixture.Create<Money>();
            
            Assert.True(money.ToString().Equals("1"));
        }
        
        [Fact]
        public void money_tostring_valid_quantity_of()
        {
            var fixture = new Fixture();
            fixture.Register<Money>(() => Money.From(1));

            var money1 = fixture.Create<Money>();
            var money2 = fixture.Create<Money>();
            
            Assert.Equal(money1,money2);
        }
        
        [Fact]
        public void money_create_valid_quantity_of()
        {
            var fixture = new Fixture();
            fixture.Register<Money>(() => Money.From(fixture.Create<Decimal>()));

            var money = fixture.Create<Money>();
            
            Assert.True(money.ValidationResults.IsValid);
        }
        
        [Fact]
        public void money_create_negative_quantity_of()
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(
                new RandomNumericSequenceGenerator(long.MinValue,0));
            fixture.Register<Money>(() => Money.From(fixture.Create<Decimal>()));

            var money = fixture.Create<Money>();
            
            Assert.False(money.ValidationResults.IsValid);
        }
    }
}