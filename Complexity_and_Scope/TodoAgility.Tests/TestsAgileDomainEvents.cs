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

using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.AggregationActivity.DomainEventHandlers;
using TodoAgility.Agile.Domain.AggregationActivity.Events;
using TodoAgility.Agile.Domain.AggregationProject;
using TodoAgility.Agile.Domain.AggregationProject.DomainEventHandlers;
using TodoAgility.Agile.Domain.AggregationProject.Events;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;

using Activity = TodoAgility.Agile.Domain.AggregationActivity.Activity;
using ProjectReference =  TodoAgility.Agile.Domain.AggregationActivity.Project;
using Project = TodoAgility.Agile.Domain.AggregationProject.Project;

namespace TodoAgility.Tests
{
    public class TestsAgileDomainEvents
    {
        #region Activity DomainEvents

        [Fact]
        public void Check_DomainEvents_ActivityAdded_Raise()
        {
            //given
            
            //existing project
            var project = Project.From(EntityId.From(1u), Description.From("descriptionText"));
            
            //a activity it is attached to it
            var activity = Activity.From(Description.From("activity to do"), EntityId.From(1u), 
                EntityId.From(1u), ActivityStatus.From(1));
            
            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagility_proj_activity_reference.db;");
            var projectDbContext = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject = new ProjectRepository(projectDbContext);
            using var projectDbSession = new DbSession<IProjectRepository>(projectDbContext, repProject);
            repProject.Add(project);
            projectDbSession.SaveChanges();
            var handlerActivityAdded = new ActivityAddedHandler(projectDbSession);
            var dispatcher = new DomainEventDispatcher();
            dispatcher.Subscribe(typeof(ActivityAddedEvent).FullName, handlerActivityAdded);

            //when
            dispatcher.Publish(ActivityAddedEvent.For(activity));

            //then
            var proj = repProject.Get(EntityId.From(1u));
            Assert.True(proj.Activities.Count > 0);
        }

        [Fact]
        public void Check_DomainEvents_ProjectAdded_Raise()
        {
            //given
            
            //existing project
            var project = Project.From(EntityId.From(1u), Description.From("descriptionText"));
            
            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_projectAdded_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);

            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext, repTask);

            var handlerActivityAdded = new ProjectAddedHandler(taskDbSession);
            var dispatcher = new DomainEventDispatcher();
            dispatcher.Subscribe(typeof(ProjectAddedEvent).FullName, handlerActivityAdded);

            //when
            dispatcher.Publish(ProjectAddedEvent.For(project));

            //then
            var projectId = EntityId.From(1u);
            var proj = repTask.GetProject(projectId);
            Assert.NotNull(proj);
        }
        #endregion
    }
}