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
using TodoAgility.Agile.CQRS.CommandHandlers;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.AggregationProject;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Hosting.CommandHandlers;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;
using Project = TodoAgility.Agile.Domain.AggregationActivity.Project;

namespace TodoAgility.Tests
{
    public class TestsAgileCommandHandlers
    {
        #region Activity Command Handlers

        [Fact]
        public void Check_AddActivityCommandHandler_Succeed()
        {
            var description = "Given Description";
            var projectId = 1u;
            var dispatcher = new DomainEventDispatcher();
            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_add_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext, repTask);
            taskDbSession.Repository.AddProject(Project.From(EntityId.From(projectId), Description.From(description)));
            taskDbSession.SaveChanges();

            var handler = new AddActivityCommandHandler(dispatcher, taskDbSession);
            var command = new AddActivityCommand(description, projectId);
            handler.Execute(command);

            var task = taskDbSession.Repository.Find(a => a.ProjectId == projectId);

            Assert.NotNull(task);
        }

        [Fact]
        public void Check_UpdateActivityCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var projectId = 1u;
            var dispatcher = new DomainEventDispatcher();
            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_cqrs_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext, repTask);

            var project = Project.From(EntityId.From(projectId), Description.From(description));
            var originalTask = Activity.From(Description.From(description), EntityId.From(id), 
                EntityId.From(projectId), ActivityStatus.From(1));
            taskDbSession.Repository.AddProject(project);
            taskDbSession.Repository.Add(originalTask);
            taskDbSession.SaveChanges();

            var descriptionNew = "Given Description Changed";
            var command = new UpdateActivityCommand(id, descriptionNew);

            var handler = new UpdateActivityCommandHandler(taskDbSession,dispatcher);
            handler.Execute(command);

            var task = taskDbSession.Repository.Get(EntityId.From(id));

            Assert.NotEqual(task, originalTask);
        }

        [Fact]
        public void Check_ChangeStatusActivityCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var status = 2;
            var projectId = 1u;
            var dispatcher = new DomainEventDispatcher();
            var optionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            optionsBuilder.UseSqlite("Data Source=todoagility_cqrs_changed_test.db;");
            var taskDbContext = new ActivityDbContext(optionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext, repTask);

            var originalTask = Activity.From(Description.From(description), EntityId.From(id), 
                EntityId.From(projectId),ActivityStatus.From(1));
            taskDbSession.Repository.Add(originalTask);
            taskDbSession.SaveChanges();

            var command = new ChangeActivityStatusCommand(id, status);

            var handler = new ChangeActivityStatusCommandHandler(taskDbSession,dispatcher);
            handler.Execute(command);

            var task = taskDbSession.Repository.Get(EntityId.From(id));

            Assert.NotEqual(task, originalTask);
        }
        
        [Fact]
        public void Check_ChangeStatusActivityCommandHandler_Failed()
        {
            var description = "Given Description";
            var id = 1u;
            var newStatus = 4;
            var projectId = 1u;
            var dispatcher = new DomainEventDispatcher();
            var optionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            optionsBuilder.UseSqlite("Data Source=todoagility_cqrs_changed_failed_test.db;");
            var taskDbContext = new ActivityDbContext(optionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext, repTask);

            var originalTask = Activity.From(Description.From(description), EntityId.From(id), 
                EntityId.From(projectId),ActivityStatus.From(1));
            taskDbSession.Repository.Add(originalTask);
            taskDbSession.SaveChanges();

            var command = new ChangeActivityStatusCommand(id, newStatus);

            var handler = new ChangeActivityStatusCommandHandler(taskDbSession,dispatcher);
            var result = handler.Execute(command);

            Assert.False(result.IsSucceed);
        }

        #endregion
    }
}