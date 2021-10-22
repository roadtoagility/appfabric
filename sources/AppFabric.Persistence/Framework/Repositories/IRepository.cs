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
using System.Linq.Expressions;
using AppFabric.Domain.Framework.BusinessObjects;
using Version = AppFabric.Domain.BusinessObjects.Version;

namespace AppFabric.Persistence.Framework.Repositories
{
    public interface IRepository<TState,TModel> where TModel : class
    {
        //TODO: remover o 2
        TModel Get(EntityId id);
        TModel Get(EntityId2 id);
        void Add(TModel entity);
        void Remove(TModel entity);
        
        IEnumerable<TModel> Find(Expression<Func<TState, bool>> predicate);
    }
}