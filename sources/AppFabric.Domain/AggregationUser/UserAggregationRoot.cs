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

using AppFabric.Domain.AggregationProject;
using AppFabric.Domain.AggregationProject.Events;
using AppFabric.Domain.AggregationUser.Events;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.Aggregates;
using AppFabric.Domain.Framework.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.Specifications;

namespace AppFabric.Domain.AggregationUser
{
    public sealed class UserAggregationRoot : ObjectBasedAggregationRoot<User, EntityId2>
    {
        private CompositeSpecification<User> _spec;

        private UserAggregationRoot(CompositeSpecification<User> specification, User user)
        {
            _spec = specification;

            if (_spec.IsSatisfiedBy(user))
            {
                Apply(user);

                if (user.IsNew())
                {
                    Raise(UserAddedEvent.For(user));
                }
            }
             
            AppendValidationResult(user.Failures);
        }

        #region Aggregation contruction

        
        public static UserAggregationRoot ReconstructFrom(User user, CompositeSpecification<User> spec)
        {
            return new UserAggregationRoot(spec, user);

        }

        
        public static UserAggregationRoot CreateFrom(Name name, SocialSecurityId cnpj, Email commercialEmail, CompositeSpecification<User> spec)
        {
            var user = User.NewRequest(EntityId2.GetNext(), name, cnpj, commercialEmail, VersionId.New());
            return new UserAggregationRoot(spec, user);
        }

        #endregion

        public void Remove()
        {
            //TODO: definir deleção
            if (_spec.IsSatisfiedBy(GetChange()))
            {
                Raise(UserRemovedEvent.For(GetChange()));
            }
        }
    }
}