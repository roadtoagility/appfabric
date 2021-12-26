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
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Persistence.ExtensionMethods
{
    public static class BusinessObjectsExtensions
    {
        public static ProjectState ToProjectState(this Project project)
        {
            return new ProjectState(project.Identity.Value,
                project.Name.Value,
                project.Code.Value,
                project.Budget.Value,
                project.StartDate.Value,
                project.ClientId.Value,
                project.Owner.Value,
                project.OrderNumber.Value.Number,
                project.Status.Value,
                BitConverter.GetBytes(project.Version.Value));
        }

        public static Project ToProject(this ProjectState state)
        {
            return Project.From(
                EntityId.From(state.Id),
                ProjectName.From(state.Name),
                ServiceOrder.From((state.OrderNumber, true)),
                ProjectStatus.From(state.Status),
                ProjectCode.From(state.Code),
                DateAndTime.From(state.StartDate),
                Money.From(state.Budget),
                EntityId.From(state.ClientId),
                Email.From(state.Owner),
                VersionId.From(BitConverter.ToInt32(state.RowVersion)));
        }

        public static UserState ToUserState(this User user)
        {
            return new UserState(user.Id.Value,
                user.Name.Value,
                user.Cnpj.Value,
                user.CommercialEmail.Value,
                BitConverter.GetBytes(user.Version.Value));
        }

        public static User ToUser(this UserState state)
        {
            return User.NewRequest(
                EntityId.From(state.Id),
                Name.From(state.Name),
                SocialSecurityId.From(state.Cnpj),
                Email.From(state.CommercialEmail),
                VersionId.From(BitConverter.ToInt32(state.RowVersion)));
        }
    }
}