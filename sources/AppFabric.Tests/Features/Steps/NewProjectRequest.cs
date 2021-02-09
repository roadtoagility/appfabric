﻿// Copyright (C) 2020  Road to Agility
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
using System.Globalization;
using System.Threading;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Domain.Framework.BusinessObjects;
using Xunit;
using Xunit.Gherkin.Quick;

namespace AppFabric.Tests.Features.Steps
{
    [FeatureFile("../Features/new_project_request_created.feature")]
    public sealed class NewProjectRequest:Feature
    {
        private ProjectName _projectName;
        private ProjectCode _projectCode;
        private DateAndTime _startDate;
        private Project _project;
        private Money _budget;
        private EntityId _clientId;
        
        public NewProjectRequest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [Given(@"The client ([a-fA-F0-9\-]+), requested a project named ([\w\s]+), code (\w+), budget ([\d\.\,]+), and start date ([\d\/]+)")]
        public void The_project_parameters_request(String clientId, string name, string code, decimal budget, DateTime date)
        {
            _projectName = ProjectName.From(name);
            _projectCode = ProjectCode.From(code);
            _startDate= DateAndTime.From(date);
            _budget = Money.From(budget);
            _clientId = EntityId.From(Guid.Parse(clientId));
        }
        
        [When(@"The client request a project")]
        public void The_client_request_a_project()
        {
            _project = Project.NewRequest(EntityId.GetNext(), _projectName, _projectCode, _startDate,_budget, _clientId);
        }

        [Then(@"The client see a project request created equals (\w+)")]
        public void The_client_see_a_project_request_created(bool created)
        {
            Assert.Equal(created, _project.ValidationResults.IsValid);
        }
    }
}