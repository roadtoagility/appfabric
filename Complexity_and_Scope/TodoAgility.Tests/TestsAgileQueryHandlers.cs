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

using System.Linq;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.CQRS.CommandHandlers;
using TodoAgility.Agile.CQRS.QueryHandlers;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.AggregationProject;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Projections;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;
using Project = TodoAgility.Agile.Domain.AggregationActivity.Project;

namespace TodoAgility.Tests
{
    public class TestsAgileQueryHandlers
    {
        #region Activity Query Handlers

        [Fact]
        public void Check_ProjectionGetActivitiesByProject()
        {
            //given
            var descriptionText = "Given Description";
            var projectId1 = 1u;
            var projectId2 = 2u;

            var activity1 = new ActivityProjection("created", string.Concat(descriptionText, " 01"), 1u, projectId1);
            var activity2 = new ActivityProjection("created", string.Concat(descriptionText, " 02"), 1u, projectId1);
            var activity3 = new ActivityProjection("created", descriptionText, 1u, projectId2);
            
            var connString = "Filename=:temp:;";
            var activityDbContext = new ActivityProjectionDbContext(connString, BsonMapper.Global);
            var repActivity = new ActivityProjectionRepository(activityDbContext);

            using var acDbSession = new ProjectionDbSession<IActivityProjectionRepository>(activityDbContext, repActivity);
            acDbSession.Repository.Add(activity1);
            acDbSession.Repository.Add(activity2);
            acDbSession.Repository.Add(activity3);
            acDbSession.SaveChanges();

            //when
            var handler = new GetActivitiesQueryHandler(acDbSession);
            var filter = GetActivitiesFilter.For(projectId2);
            var activities = handler.Execute(filter);
            
            //then
            Assert.True(activities.Items.AsQueryable().Count(i=> i.ProjectId == projectId2) == 1);
        }

        #endregion
    }
}