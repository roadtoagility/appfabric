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
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.AggregationUser.Specifications;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.Factories
{
    public class UserAggregateFactory :
        IAggregateFactory<UserAggregationRoot, AddUserCommand>,
        IAggregateFactory<UserAggregationRoot, User>
    {
        public UserAggregationRoot Create(AddUserCommand source)
        {
            var newUserSpec = new UserCreationSpecification();
            var userSpec = new UserSpecification();
            
            var user = User.NewRequest(source.Name, source.Cnpj, source.CommercialEmail);

            if (newUserSpec.IsSatisfiedBy(user))
            {
                throw new ArgumentException("Invalid Command");
            }

            return new UserAggregationRoot(user);
        }

        public UserAggregationRoot Create(User source)
        {
            var userSpec = new UserSpecification();

            if (userSpec.IsSatisfiedBy(source))
            {
                throw new ArgumentException("Invalid Command");
            }
            
            return new UserAggregationRoot(source);
        }
    }
}