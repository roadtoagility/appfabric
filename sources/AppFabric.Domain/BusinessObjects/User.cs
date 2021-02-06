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

using System.Collections.Generic;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Domain.Framework.Validation;

namespace AppFabric.Domain.BusinessObjects
{
    public sealed class User : ValidationStatus
    {
        private User(EntityId clientId, Name name, SocialSecurityId cnpj, Email commercialEmail)
        {
            Id = clientId;
            Name = name;
            Cnpj = cnpj;
            CommercialEmail = commercialEmail;
        }
        public EntityId Id { get; }
        
        public Name Name { get; }
        public SocialSecurityId Cnpj { get; }
        
        public Email CommercialEmail { get; }
                
        public static User From(EntityId clientId, Name name, SocialSecurityId cnpj, Email commercialEmail)
        {
            var user = new User(clientId,name,cnpj,commercialEmail);
            var validator = new UserValidator();
            user.SetValidationResult(validator.Validate(user));
            return user;        
        }

        public static User Empty()
        {
            return From(EntityId.Empty(), Name.Empty(), SocialSecurityId.Empty(), Email.Empty());
        }
        
        public override string ToString()
        {
            return $"[PROJECT]:[ID: {Id} Name: {Name}, Social Security: {Cnpj} Commercial Email: {CommercialEmail}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return Cnpj;
            yield return CommercialEmail;
        }
    }
}