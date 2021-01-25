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

using System.Collections.Immutable;
using FluentMediator;
using TodoAgility.Business.CommandHandlers.Commands;
using TodoAgility.Business.Framework;
using TodoAgility.Domain.AggregationProject;
using TodoAgility.Domain.AggregationUser;
using TodoAgility.Domain.BusinessObjects;
using TodoAgility.Domain.Framework.BusinessObjects;
using TodoAgility.Persistence.Framework;
using TodoAgility.Persistence.Model.Repositories;

namespace TodoAgility.Business.CommandHandlers
{
    public sealed class AddUserCommandHandler : CommandHandler<AddUserCommand, CommandResult<long>>
    {
        private readonly IDbSession<IUserRepository> _dbSession;
        
        public AddUserCommandHandler(IMediator publisher, IDbSession<IUserRepository> dbSession)
            :base(publisher)
        {
            _dbSession = dbSession;
        }
        
        protected override CommandResult<long> ExecuteCommand(AddUserCommand command)
        {
            var agg = UserAggregationRoot.CreateFrom(
                Name.From(command.Name),
                SocialSecurityId.From(command.Cnpj),
                Email.From(command.Email));
            
            var isSucceed = false;
            var okId = -1L;
      
            if (agg.ValidationResults.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                _dbSession.SaveChanges();
                
                agg.GetEvents().ToImmutableList()
                    .ForEach( ev => Publisher.Publish(ev));
                
                isSucceed = true;
                okId = agg.GetChange().Id.Value;
            }
            
            return new CommandResult<long>(isSucceed, okId,agg.ValidationResults.Errors.ToImmutableList());
        }
    }
}