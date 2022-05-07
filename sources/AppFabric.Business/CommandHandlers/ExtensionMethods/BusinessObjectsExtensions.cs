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


using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Business.CommandHandlers.ExtensionMethods
{
    //TODO: update serviceOrder.From to get the value from UpdateProjectCommand
    public static class CommandExtensions
    {
        public static Project.ProjectDetail ToProjectDetail(this UpdateProjectCommand command)
        {
            return new Project.ProjectDetail(
                ProjectName.From(command.Name),
                Money.From(command.Budget),
                Email.From(command.Owner),
                ProjectStatus.From(command.Status),
                ServiceOrder.From((command.OrderNumber, true))
            );
        }
    }
}