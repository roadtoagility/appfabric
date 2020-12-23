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
using LiteDB;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Projections;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;

using ProjectReference = TodoAgility.Agile.Domain.AggregationActivity.Project;
using Activity = TodoAgility.Agile.Domain.AggregationActivity.Activity;
using Project = TodoAgility.Agile.Domain.AggregationProject.Project;

namespace TodoAgility.Tests
{
    public class TestsAgileDomainModelPersistence
    {
        #region Activity Persistence

        [Fact]
        public void Check_ActivityRespository_Create()
        {
            //given
            var descriptionText = "Given Description";
            var project = ProjectReference.From(EntityId.From(1u), Description.From(descriptionText));
            
            var id = EntityId.From(1u);

            var task = Activity.From(Description.From(descriptionText), id, 
                EntityId.From(1u),ActivityStatus.From(1));

            //when
            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_repo_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);

            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext, repTask);
            taskDbSession.Repository.Add(task);
            taskDbSession.SaveChanges();

            //then
            var taskSaved = taskDbSession.Repository.Get(id);
            Assert.Equal(taskSaved, task);
        }

        [Fact]
        public void Check_ActivityRespository_Update()
        {
            //given
            var descriptionText = "Given Description";
            var descriptionTextChanged = "Given Description Modificada";
            var project = ProjectReference.From(EntityId.From(1u), Description.From(descriptionText));

            var id = EntityId.From(1u);

            var task = Activity.From(Description.From(descriptionText), id, 
                EntityId.From(1u), ActivityStatus.From(1));

            //when
            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_repo_update_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);

            using var taskDbSession = new ActivityDbSession(taskDbContext, repTask);
            taskDbSession.Repository.Add(task);
            taskDbSession.SaveChanges();

            //then
            var taskSaved = taskDbSession.Repository.Get(id);
            var updatetask = Activity.CombineWithPatch(taskSaved,
                Activity.Patch.FromDescription(Description.From(descriptionTextChanged)));
            taskDbSession.Repository.Add(updatetask);
            taskDbSession.SaveChanges();

            var taskUpdated = taskDbSession.Repository.Get(id);
            Assert.NotEqual(taskUpdated, task);
        }

        [Fact]
        public void Check_ProjectActivityReferenceRespository_Update()
        {
            //given
            var descriptionText = "Given Description";
            var projectId = EntityId.From(1u);
            var id = EntityId.From(1u);
            var project = Project.From(EntityId.From(1u), Description.From(descriptionText));
            var projectReference = ProjectReference.From(projectId, Description.From(descriptionText));
            var task = Activity.From(Description.From(descriptionText), id, projectId, 
                ActivityStatus.From(1));

            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagility_project_update.db;");
            var projectDbContext = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject = new ProjectRepository(projectDbContext);

            using var projectDbSession = new ProjectDbSession(projectDbContext, repProject);
            projectDbSession.Repository.Add(project);
            projectDbSession.SaveChanges();

            //when
            var tasks = new List<EntityId> {task.Id};
            var projectWithTasks = Project.CombineProjectAndActivities(project, tasks);
            var projectDbContext2 = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject2 = new ProjectRepository(projectDbContext2);

            //then
            using var projectDbSession2 = new DbSession<IProjectRepository>(projectDbContext2, repProject2);
            projectDbSession2.Repository.Add(projectWithTasks);
            projectDbSession2.SaveChanges();

            var projectUpdated = projectDbSession2.Repository.Get(projectWithTasks.Id);
            Assert.True(projectUpdated.Activities.Count > 0);
        }

        [Fact]
        public void Check_ProjectRespository_Remove()
        {
            //given
            var descriptionText = "Given Description";
            var projectId = EntityId.From(1u);

            var project = Project.From(projectId, Description.From(descriptionText));

            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagility_project_remove.db;");

            var projectDbContext = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject = new ProjectRepository(projectDbContext);

            using var projectDbSession = new ProjectDbSession(projectDbContext, repProject);
            projectDbSession.Repository.Add(project);
            projectDbSession.SaveChanges();

            //when
            var projectToremove = projectDbSession.Repository.Get(projectId);
            projectDbSession.Repository.Remove(projectToremove);
            projectDbSession.SaveChanges();

            //then
            Assert.Throws<InvalidOperationException>(() =>
            {
                var repProject3 = new ProjectRepository(projectDbContext);
                using var dbs = new ProjectDbSession(projectDbContext, repProject3);
                return dbs.Repository.Get(projectId);
            });
        }

        #endregion
        
        [Fact]
        public void Check_ActivityProjection_Create()
        {
            //given
            var descriptionText = "Given Description";
            var projectId = EntityId.From(1u);

            var activity = new ActivityProjection("created", descriptionText, 1u, 1u);
            var connString = "Filename=todoagility_projection.db;Connection=shared";
            var activityDbContext = new ActivityProjectionDbContext(connString, BsonMapper.Global);
            var repActivity = new ActivityProjectionRepository(activityDbContext);

            using var pDbSession = new ProjectionDbSession<IActivityProjectionRepository>(activityDbContext, repActivity);
            pDbSession.Repository.Add(activity);
            pDbSession.SaveChanges();

            //when
            var found = pDbSession.Repository.Get(projectId);

            //then
            Assert.Equal(activity.ActivityId,found.ActivityId);
        }
    }
}