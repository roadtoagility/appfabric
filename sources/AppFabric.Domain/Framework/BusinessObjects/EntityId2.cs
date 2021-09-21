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
using AppFabric.Domain.Framework.Validation;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.Framework.BusinessObjects
{
    public sealed class EntityId2 : ValueOf<Guid, EntityId2, EntityIdValidator>
    {
        public static EntityId2 Empty()
        {
            return From(Guid.Empty);
        }

        public static EntityId2 GetNext()
        {
            return From(Guid.NewGuid());
        }
    }
}