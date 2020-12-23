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
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.AggregationProject;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using Xunit;
using Project = TodoAgility.Agile.Domain.AggregationActivity.Project;

namespace TodoAgility.Tests
{
    public class TestsTodoDomain
    {
        #region Description Business Object tests

        [Fact]
        public void Check_Description_Invalid_ValueNull()
        {
            var description = Description.From(null);
            Assert.False(description.ValidationResults.IsValid);
        }

        [Fact]
        public void Check_Description_Invalid_ValueEmpty()
        {
            var text = "";
            var description = Description.From(text);
            Assert.False(description.ValidationResults.IsValid);
        }

        [Fact]
        public void Check_Description_Invalid_ValueBlanks()
        {
            var text = "        ";
            var description = Description.From(text);
            Assert.False(description.ValidationResults.IsValid);
        }

        [Fact]
        public void Check_Description_Invalid_SizeLimit()
        {
            var text =
                "Teste excendo o limite do nome para o todo Teste excendo o limite do nome para o todo Teste excendo o limite do nome para o todo";
            var description = Description.From(text);
            Assert.False(description.ValidationResults.IsValid);
        }

        [Fact]
        public void Check_Description_Value_Exposing()
        {
            var givenName = "Given Description";
            var todoName = Description.From(givenName);
            IExposeValue<string> state = todoName;
            Assert.Equal(givenName, state.GetValue());
        }

        [Fact]
        public void Check_Descriptions_Are_Equal()
        {
            var givenName1 = "Given Description";
            var givenName2 = "Given Description";
            var name1 = Description.From(givenName1);
            var name2 = Description.From(givenName2);

            Assert.Equal(name1, name2);
        }

        #endregion

        #region Activity Business Object tests

        [Fact]
        public void Check_Task_Invalid_Description()
        {
            var activity = Activity.From(null, null, null, null);
            Assert.True(activity.ValidationResults.IsValid);
        }

        [Fact]
        public void Check_Task_valid_instance()
        {
            var name = Description.From("givenName");
            var entityId = EntityId.From(1u);

            var task = Activity.From(name, entityId, EntityId.From(1u), ActivityStatus.From(1));
            Assert.NotNull(task);
        }

        [Fact]
        public void Check_Task_GetValue()
        {
            var givenName = "givenName";
            var name = Description.From(givenName);
            var project = Project.From(EntityId.From(1u), Description.From(givenName));
            var entityId = EntityId.From(1u);

            var todo = Activity.From(name, entityId, EntityId.From(1u), ActivityStatus.From(1));
            IExposeValue<ActivityState> state = todo;
            var todoState = state.GetValue();

            Assert.Equal(todoState.Description, givenName);
        }

        [Fact]
        public void Check_TaskStatus_Invalid_Status()
        {
            var status = ActivityStatus.From(-1);
            Assert.False(status.ValidationResults.IsValid);
        }

        [Fact]
        public void Check_TaskStatus_valid_Status()
        {
            var statusStarted = ActivityStatus.From(2);
            Assert.Equal(2, statusStarted.Value);
        }

        #endregion

        #region Activity aggregate

        [Fact]
        public void Check_TaskAggregation_Create()
        {
            //given
            var descriptionText = "Given Description";
            var id = EntityId.From(1u);
            var projectId = EntityId.From(1u);

            var project = Project.From(projectId, Description.From(descriptionText));
            var task = Activity.From(Description.From(descriptionText), id, EntityId.From(1u), 
                ActivityStatus.From(1));

            //when
            var agg = ActivityAggregationRoot.CreateFrom(Description.From(descriptionText), id, project);
            var changes = agg.GetChange();

            //then
            Assert.Equal(changes, task);
        }

        [Fact]
        public void Check_TaskAggregation_UpdateTask()
        {
            //given
            var descriptionText = "Given Description";
            var descriptionNewText = "Given Description New One";
            var id = EntityId.From(1u);

            var oldState = Activity.From(Description.From(descriptionText), id, EntityId.From(1u),
                ActivityStatus.From(1));
            //when
            var agg = ActivityAggregationRoot.ReconstructFrom(oldState);
            agg.UpdateTask(Activity.Patch.FromDescription(Description.From(descriptionNewText)));
            var changes = agg.GetChange();

            //then
            Assert.NotEqual(changes, oldState);
        }

        [Fact]
        public void Check_TaskAggregation_UpdateTaskStatus()
        {
            //given
            var descriptionText = "Given Description";
            var id = EntityId.From(1u);
            var newStatus = 2;
            var oldState = Activity.From(Description.From(descriptionText), id, EntityId.From(1u),
                ActivityStatus.From(1));

            //when
            var agg = ActivityAggregationRoot.ReconstructFrom(oldState);
            agg.ChangeTaskStatus(ActivityStatus.From(newStatus));
            var changes = agg.GetChange();

            //then
            Assert.NotEqual(changes, oldState);
        }


        [Fact]
        public void Check_TaskAggregation_UpdateTaskStatus_invalid()
        {
            //given
            var descriptionText = "Given Description";
            var id = EntityId.From(1u);
            var newStatus = 6;

            var oldState = Activity.From(Description.From(descriptionText), id, EntityId.From(1u),
                ActivityStatus.From(1));

            //when
            var agg = ActivityAggregationRoot.ReconstructFrom(oldState);
            agg.ChangeTaskStatus(ActivityStatus.From(newStatus));
            
            //then
            Assert.False(agg.ValidationResults.IsValid);
        }

        #endregion
    }
}