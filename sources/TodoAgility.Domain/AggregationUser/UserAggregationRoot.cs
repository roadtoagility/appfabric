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

using TodoAgility.Domain.AggregationProject;
using TodoAgility.Domain.AggregationProject.Events;
using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.Aggregates;
using TodoAgility.Domain.Framework.BusinessObjects;

namespace TodoAgility.Domain.AggregationUser
{
    public sealed class UserAggregationRoot : AggregationRoot<User>
    {

        private UserAggregationRoot(User user)
        {
            if (user.ValidationResults.IsValid)
            {
                Apply(user);
            }

            ValidationResults = user.ValidationResults;
        }
        
        private UserAggregationRoot(EntityId id, Name name, SocialSecurityId cnpj, Email commercialEmail)
            : this(User.From(id, name,cnpj,commercialEmail))
        {
        }

        #region Aggregation contruction

        
        public static UserAggregationRoot ReconstructFrom(User currentState)
        {
            return new UserAggregationRoot(currentState);
        }

        
        public static UserAggregationRoot CreateFrom(EntityId id, Name name, SocialSecurityId cnpj, Email commercialEmail)
        {
            return new UserAggregationRoot(id, name,cnpj,commercialEmail);
        }

        #endregion
    }
}